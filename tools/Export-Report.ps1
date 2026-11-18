[CmdletBinding()]
param([string]$PreviewHtmlPath, [string]$OutputPdfPath)
$ErrorActionPreference = 'Stop'
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$sourcePath = Join-Path $repositoryRoot 'docs/rapport.md'
$pdfPath = Join-Path $repositoryRoot 'docs/rapport_part1_benjamin_joannard.pdf'
if ($OutputPdfPath) { $pdfPath = [IO.Path]::GetFullPath($OutputPdfPath) }
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
$html.Add('<!doctype html><html lang="fr"><meta charset="utf-8"><title>Fruit Ninja VR - Benjamin JOANNARD</title><style>body{font:12.2px Arial;line-height:1.35;color:#17263a;margin:0}h1{font-size:25px;color:#163767;margin:10px 0 5px}h2{font-size:18px;color:#163767;margin:17px 0 9px;break-after:avoid}h3{font-size:13px;color:#163767;margin:13px 0 7px;break-after:avoid}p{margin:7px 0}table{width:100%;border-collapse:collapse;font-size:10.3px;line-height:1.26;margin:10px 0}th,td{border:1px solid #cbd5e1;padding:5px;text-align:left;vertical-align:top}th{background:#edf2f8;color:#163767}tr{break-inside:avoid}code{font:10.7px Consolas,monospace}a{color:#184f70;text-decoration:none}figure{margin:10px 0;break-inside:avoid}figure img{width:100%;height:auto}figcaption{font-size:9.6px;color:#48576a;margin-top:4px}.report-brand img{width:195px;height:auto}.page-break{break-before:page}ul{padding-left:19px;margin:7px 0}li{margin:5px 0}@page{size:A4;margin:13mm 15mm 15mm;@bottom-left{content:"Benjamin JOANNARD - ST2VAAS 2026-2027";font:8px Arial;color:#637083}@bottom-right{content:counter(page) " / " counter(pages);font:8px Arial;color:#637083}}@media screen{body{max-width:690px;margin:25px auto}.page-break{border-top:1px dashed #ccd3da;margin-top:30px;padding-top:20px}}</style><body>')
$inTable = $false
$inList = $false
foreach ($line in Get-Content -LiteralPath $sourcePath -Encoding UTF8) {
    if ($inTable -and -not $line.StartsWith('|')) { $html.Add('</tbody></table>'); $inTable = $false }
    if ($inList -and -not $line.StartsWith('- ')) { $html.Add('</ul>'); $inList = $false }
    if ($line -eq '<!-- PAGE BREAK -->') {
        $html.Add('<div class="page-break"></div>')
    } elseif ($line.StartsWith('|')) {
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
        if ([IO.Path]::GetFileName($imagePath) -eq 'efrei-logo.svg') {
            $html.Add('<div class="report-brand"><img alt="Efrei Paris - Paris Pantheon-Assas Universite" src="' + ([uri]$imagePath).AbsoluteUri + '"></div>')
        } else {
            $html.Add('<figure><img src="' + ([uri]$imagePath).AbsoluteUri + '"><figcaption>' + (Format-Inline $imageCaption) + '</figcaption></figure>')
        }
    } elseif ($line.StartsWith('### ')) { $html.Add('<h3>' + (Format-Inline $line.Substring(4)) + '</h3>')
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
if ($PreviewHtmlPath) {
    $previewTarget = [IO.Path]::GetFullPath($PreviewHtmlPath)
    [IO.Directory]::CreateDirectory([IO.Path]::GetDirectoryName($previewTarget)) | Out-Null
    [IO.File]::WriteAllLines($previewTarget, $html, [Text.UTF8Encoding]::new($false))
}
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
