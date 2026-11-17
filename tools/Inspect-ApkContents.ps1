[CmdletBinding()]
param([string]$ApkPath)
$ErrorActionPreference = 'Stop'
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
if (-not $ApkPath) { $ApkPath = Join-Path $repositoryRoot 'Builds/Android_Quest/FruitNinjaVR_Quest.apk' }
if (-not (Test-Path -LiteralPath $ApkPath -PathType Leaf)) { throw 'APK absent : produire le build avant analyse.' }
Add-Type -AssemblyName System.IO.Compression.FileSystem
$resolvedApk = (Resolve-Path -LiteralPath $ApkPath).Path
$hash = (Get-FileHash -LiteralPath $resolvedApk -Algorithm SHA256).Hash
'# Releve automatise du contenu ZIP de l''APK'
''
'Analyse de la structure ZIP uniquement ; Android Studio APK Analyzer et execution sur casque NON REALISES.'
''
'SHA256 : `' + $hash + '`'
''
'Mo = octets / 1 048 576. Les sommes ne comprennent pas les en-tetes et signatures ZIP.'
''
'| Entree / groupe | Fichiers | Mo compresses | Mo decomprimees |'
'|---|---:|---:|---:|'
$groups = @(
    @{ Name = 'assets/bin/Data'; Pattern = 'assets/bin/Data/*' },
    @{ Name = 'lib/arm64-v8a/libil2cpp.so'; Pattern = 'lib/arm64-v8a/libil2cpp.so' },
    @{ Name = 'lib/arm64-v8a/libunity.so'; Pattern = 'lib/arm64-v8a/libunity.so' },
    @{ Name = 'classes*.dex'; Pattern = 'classes*.dex' },
    @{ Name = 'resources.arsc'; Pattern = 'resources.arsc' }
)
$archive = [IO.Compression.ZipFile]::OpenRead($resolvedApk)
try {
    foreach ($group in $groups) {
        $entries = @($archive.Entries | Where-Object { $_.FullName -like $group.Pattern })
        if ($entries.Count -eq 0) { '| {0} | 0 | ABSENT | ABSENT |' -f $group.Name; continue }
        [long]$compressed = ($entries | Measure-Object -Property CompressedLength -Sum).Sum
        [long]$expanded = ($entries | Measure-Object -Property Length -Sum).Sum
        '| {0} | {1} | {2} | {3} |' -f $group.Name, $entries.Count, ($compressed/1MB).ToString('F2',[Globalization.CultureInfo]::InvariantCulture), ($expanded/1MB).ToString('F2',[Globalization.CultureInfo]::InvariantCulture)
    }
    ''
    '## Dix entrees les plus volumineuses (taille decompressee)'
    ''
    '| Entree | Octets compresses | Octets decomprimes |'
    '|---|---:|---:|'
    $archive.Entries | Sort-Object Length -Descending | Select-Object -First 10 | ForEach-Object {
        '| {0} | {1} | {2} |' -f $_.FullName.Replace('|','/'), $_.CompressedLength, $_.Length
    }
} finally { $archive.Dispose() }
