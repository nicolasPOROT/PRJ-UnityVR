[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$sourcePath = Join-Path $repositoryRoot 'docs/rapport.md'
$pdfPath = Join-Path $repositoryRoot 'docs/rapport.pdf'
$edgePath = 'C:/Program Files (x86)/Microsoft/Edge/Application/msedge.exe'
if (-not (Test-Path -LiteralPath $edgePath)) { throw 'Microsoft Edge absent : exporter le Markdown avec un autre outil local.' }
function Format-Inline([string]$value) {
    $encoded = [Net.WebUtility]::HtmlEncode($value)
    $encoded = $encoded -replace '\*\*(.+?)\*\*', '<strong>$1</strong>'
    $encoded = $encoded -replace '`([^`]+)`', '<code>$1</code>'
    $encoded = $encoded -replace '\[([^\]]+)\]\(([^)]+)\)', '<a href="$2">$1</a>'
    return $encoded
}
$html = [Collections.Generic.List[string]]::new()
$html.Add('<!doctype html><html lang="fr"><meta charset="utf-8"><title>Fruit Ninja VR - Rapport</title><style>body{font:11px Arial;line-height:1.45;color:#18202b}h1{font-size:24px}h2{font-size:17px;margin-top:22px;break-after:avoid}p{margin:7px 0}table{width:100%;border-collapse:collapse;font-size:8.5px;margin:12px 0}th,td{border:1px solid #ccd3da;padding:5px;text-align:left;vertical-align:top}th{background:#edf2f5}tr{break-inside:avoid}code{font-size:10px}a{color:#184f70} @page{size:A4;margin:15mm}</style><body>')
$inTable = $false
$inList = $false
foreach ($line in Get-Content -LiteralPath $sourcePath -Encoding UTF8) {
    if ($inTable -and -not $line.StartsWith('|')) { $html.Add('</tbody></table>'); $inTable = $false }
    if ($inList -and -not $line.StartsWith('- ')) { $html.Add('</ul>'); $inList = $false }
    if ($line.StartsWith('|')) {
        if ($line -match '^\|[\s:|-]+\|$') { continue }
        $cells = $line.Trim().Trim('|').Split('|')
        if (-not $inTable) {
            $html.Add('<table><thead><tr>' + (($cells | ForEach-Object { '<th>' + (Format-Inline $_.Trim()) + '</th>' }) -join '') + '</tr></thead><tbody>')
            $inTable = $true
        } else {
            $html.Add('<tr>' + (($cells | ForEach-Object { '<td>' + (Format-Inline $_.Trim()) + '</td>' }) -join '') + '</tr>')
        }
    } elseif ($line -match '^!\[([^\]]*)\]\(([^)]+)\)$') {
        $imageCaption = $Matches[1]
        $imagePath = [IO.Path]::GetFullPath((Join-Path (Split-Path -Parent $sourcePath) $Matches[2]))
        $html.Add('<figure style="margin:12px 0"><img style="width:100%" src="' + ([uri]$imagePath).AbsoluteUri + '"><figcaption>' + (Format-Inline $imageCaption) + '</figcaption></figure>')
    } elseif ($line.StartsWith('## ')) { $html.Add('<h2>' + (Format-Inline $line.Substring(3)) + '</h2>')
    } elseif ($line.StartsWith('# ')) { $html.Add('<h1>' + (Format-Inline $line.Substring(2)) + '</h1>')
    } elseif ($line.StartsWith('- ')) {
        if (-not $inList) { $html.Add('<ul>'); $inList = $true }
        $html.Add('<li>' + (Format-Inline $line.Substring(2)) + '</li>')
    } elseif ($line.Length -gt 0) { $html.Add('<p>' + (Format-Inline $line) + '</p>') }
}
if ($inTable) { $html.Add('</tbody></table>') }
if ($inList) { $html.Add('</ul>') }
$html.Add('</body></html>')
$htmlPath = Join-Path ([IO.Path]::GetTempPath()) ('FruitNinja-report-' + [guid]::NewGuid().ToString('N') + '.html')
$profilePath = Join-Path ([IO.Path]::GetTempPath()) ('FruitNinja-pdf-' + [guid]::NewGuid().ToString('N'))
[IO.File]::WriteAllLines($htmlPath, $html, [Text.UTF8Encoding]::new($false))
$startedAt = [DateTime]::UtcNow
$edgeArgs = @('--headless', '--disable-gpu', '--no-pdf-header-footer', ('--user-data-dir="' + $profilePath + '"'), ('--print-to-pdf="' + $pdfPath + '"'), ('"' + ([uri]$htmlPath).AbsoluteUri + '"'))
Start-Process -FilePath $edgePath -ArgumentList $edgeArgs -WindowStyle Hidden | Out-Null
$deadline = $startedAt.AddSeconds(30)
do {
    Start-Sleep -Milliseconds 200
    $pdfFile = Get-Item -LiteralPath $pdfPath -ErrorAction SilentlyContinue
    if ($pdfFile -and $pdfFile.LastWriteTimeUtc -ge $startedAt -and $pdfFile.Length -gt 0) { $pdfFile | Select-Object FullName, Length; return }
} while ([DateTime]::UtcNow -lt $deadline)
throw 'Export PDF non confirme dans les 30 secondes.'
