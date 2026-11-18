[CmdletBinding()]
param([string]$OutputZip)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$deliveryRoot = [IO.Path]::GetFullPath((Join-Path $repositoryRoot 'Livrable'))
if (-not $OutputZip) {
    $OutputZip = Join-Path $deliveryRoot 'Remise_FruitNinjaVR_Part1_Benjamin_JOANNARD.zip'
}
$OutputZip = [IO.Path]::GetFullPath($OutputZip)
if (-not $OutputZip.StartsWith($deliveryRoot + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase) -or
    [IO.Path]::GetExtension($OutputZip) -ne '.zip') {
    throw 'Le ZIP de remise doit se trouver dans Livrable.'
}
if (Test-Path -LiteralPath $OutputZip) { throw 'Ce ZIP existe deja ; choisir un autre -OutputZip pour conserver la version precedente.' }
$windowsRoot = Join-Path $deliveryRoot 'Windows_PCVR'
$apkPath = Join-Path $deliveryRoot 'Quest/FruitNinjaVR_Quest.apk'
$pdfPath = Join-Path $repositoryRoot 'docs/rapport_part1_benjamin_joannard.pdf'
$required = @(
    (Join-Path $windowsRoot 'FruitNinjaVR_PCVR.exe'),
    (Join-Path $windowsRoot 'UnityPlayer.dll'),
    (Join-Path $windowsRoot 'FruitNinjaVR_PCVR_Data'),
    (Join-Path $windowsRoot 'MonoBleedingEdge'),
    $apkPath, $pdfPath
)
foreach ($path in $required) {
    if (-not (Test-Path -LiteralPath $path)) { throw ('Livrable manquant : ' + $path) }
}
$sources = [ordered]@{}
foreach ($file in Get-ChildItem -LiteralPath $windowsRoot -Recurse -File -Force) {
    $relativePath = $file.FullName.Substring($windowsRoot.Length + 1).Replace('\','/')
    $sources.Add(('Windows_PCVR/' + $relativePath), $file.FullName)
}
$sources.Add('Quest/FruitNinjaVR_Quest.apk', $apkPath)
$sources.Add('Rapport_Benjamin_JOANNARD.pdf', $pdfPath)
$notice = @'
FRUIT NINJA VR - PART 1
Benjamin JOANNARD - ST2VAAS - 2026-2027

CONTENU
Windows_PCVR/ : build Windows complet, executable et dependances.
Quest/FruitNinjaVR_Quest.apk : application Meta Quest autonome.
Rapport_Benjamin_JOANNARD.pdf : rapport du mini-projet.

PROJET SOURCE GITHUB
https://github.com/nicolasPOROT/PRJ-UnityVR/tree/Mini-projet-Ben
Branche : Mini-projet-Ben
Unity : 6000.0.58f1
Scene de demarrage : Assets/Scenes/MainMenu.unity
Sources : Assets avec .meta, Packages, ProjectSettings.

LANCEMENT WINDOWS PCVR
Connecter le casque et activer un runtime OpenXR compatible.
Lancer Windows_PCVR/FruitNinjaVR_PCVR.exe.
Conserver tout le dossier Windows_PCVR avec l'executable.
Essais : Meta Quest 3 via Virtual Desktop.

LANCEMENT QUEST AUTONOME
Activer le mode developpeur et autoriser le debogage USB.
Depuis le dossier extrait :
adb devices
adb install -r "Quest/FruitNinjaVR_Quest.apk"
Lancer Fruit Ninja VR depuis les applications de sources inconnues.

CONTROLES
Deplacer les sabres avec les controleurs pour trancher les fruits.
Eviter les bombes. Dans le menu, trancher le fruit de la difficulte.
Utiliser le rayon et la gachette pour le volume et les boutons de fin.
'@
$zipStream = [IO.File]::Open($OutputZip, [IO.FileMode]::CreateNew, [IO.FileAccess]::ReadWrite)
try {
    $archive = [IO.Compression.ZipArchive]::new($zipStream, [IO.Compression.ZipArchiveMode]::Create, $true)
    try {
        foreach ($item in $sources.GetEnumerator()) {
            $null = [IO.Compression.ZipFileExtensions]::CreateEntryFromFile($archive, $item.Value, $item.Key, [IO.Compression.CompressionLevel]::Optimal)
        }
        $noticeEntry = $archive.CreateEntry('LISEZ_MOI.txt')
        $writer = [IO.StreamWriter]::new($noticeEntry.Open(), [Text.UTF8Encoding]::new($false))
        try { $writer.Write($notice) } finally { $writer.Dispose() }
    } finally { $archive.Dispose() }
} finally { $zipStream.Dispose() }

$verification = [IO.Compression.ZipFile]::OpenRead($OutputZip)
$sha256 = [Security.Cryptography.SHA256]::Create()
try {
    if ($verification.Entries.Count -ne $sources.Count + 1) { throw 'Nombre de fichiers incorrect dans le ZIP.' }
    foreach ($item in $sources.GetEnumerator()) {
        $entry = $verification.GetEntry($item.Key)
        if (-not $entry) { throw ('Fichier absent du ZIP : ' + $item.Key) }
        $sourceStream = [IO.File]::OpenRead($item.Value)
        $entryStream = $entry.Open()
        try {
            $expected = [BitConverter]::ToString($sha256.ComputeHash($sourceStream))
            $actual = [BitConverter]::ToString($sha256.ComputeHash($entryStream))
            if ($actual -cne $expected) { throw ('Contenu different dans le ZIP : ' + $item.Key) }
        } finally { $sourceStream.Dispose(); $entryStream.Dispose() }
    }
    $reader = [IO.StreamReader]::new($verification.GetEntry('LISEZ_MOI.txt').Open())
    try {
        if ($reader.ReadToEnd() -cne $notice) { throw 'Notice incorrecte dans le ZIP.' }
    } finally { $reader.Dispose() }
} finally { $verification.Dispose(); $sha256.Dispose() }
$result = Get-Item -LiteralPath $OutputZip
[pscustomobject]@{
    Archive = $result.FullName
    Files = $sources.Count + 1
    Bytes = $result.Length
    MiB = [Math]::Round($result.Length / 1MB, 2)
    SHA256 = (Get-FileHash -LiteralPath $OutputZip -Algorithm SHA256).Hash
    Verification = 'Tous les fichiers compares par SHA256, notice verifiee'
}
