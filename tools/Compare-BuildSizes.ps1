[CmdletBinding()]
param(
    [string]$ApkPath,
    [string]$WindowsZipPath,
    [string]$WindowsFolderPath
)

$ErrorActionPreference = 'Stop'
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
if (-not $ApkPath) { $ApkPath = Join-Path $repositoryRoot 'Builds/Android_Quest/FruitNinjaVR_Quest.apk' }
if (-not $WindowsZipPath) { $WindowsZipPath = Join-Path $repositoryRoot 'Builds/FruitNinjaVR_Windows.zip' }
if (-not $WindowsFolderPath) { $WindowsFolderPath = Join-Path $repositoryRoot 'Builds/Windows_PCVR' }
$invariant = [Globalization.CultureInfo]::InvariantCulture
$targets = @(
    @{ Label = 'APK Quest'; Path = $ApkPath; Directory = $false },
    @{ Label = 'ZIP Windows PCVR'; Path = $WindowsZipPath; Directory = $false },
    @{ Label = 'Dossier Windows PCVR'; Path = $WindowsFolderPath; Directory = $true }
)
'| Livrable | Taille (Mo, base 1024) | Octets | Statut |'
'|---|---:|---:|---|'
foreach ($target in $targets) {
    $pathType = if ($target.Directory) { 'Container' } else { 'Leaf' }
    if (-not (Test-Path -LiteralPath $target.Path -PathType $pathType)) {
        '| {0} | - | - | ABSENT |' -f $target.Label
        continue
    }
    [long]$bytes = 0
    if ($target.Directory) {
        Get-ChildItem -LiteralPath $target.Path -File -Recurse -Force | ForEach-Object { $bytes += $_.Length }
    } else {
        $bytes = (Get-Item -LiteralPath $target.Path).Length
    }
    $size = ($bytes / 1MB).ToString('F2', $invariant)
    '| {0} | {1} | {2} | PRESENT |' -f $target.Label, $size, $bytes
}
