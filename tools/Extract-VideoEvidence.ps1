[CmdletBinding()]
param([string]$FfmpegPath = 'C:/Program Files/Virtual Desktop Streamer/ffmpeg.exe')
$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $FfmpegPath)) { throw 'Indiquer le chemin de ffmpeg avec -FfmpegPath.' }
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$videoDirectory = Join-Path $repositoryRoot 'docs/preuves/videos'
$captureDirectory = Join-Path $repositoryRoot 'docs/preuves/captures'
[IO.Directory]::CreateDirectory($captureDirectory) | Out-Null
$captures = @(
    @('pcvr',32.50,'pcvr_jus'), @('quest_autonome',17.50,'quest_jus'),
    @('pcvr',37.55,'pcvr_explosion'), @('quest_autonome',45.35,'quest_explosion'),
    @('pcvr',43.60,'pcvr_volume'), @('quest_autonome',5.50,'quest_volume'),
    @('pcvr',40.00,'pcvr_fin'), @('quest_autonome',48.00,'quest_fin'),
    @('pcvr',60.00,'pcvr_gameplay'), @('quest_autonome',37.50,'quest_gameplay')
)
foreach ($capture in $captures) {
    $time = ([double]$capture[1]).ToString('0.00',[Globalization.CultureInfo]::InvariantCulture)
    & $FfmpegPath -y -hide_banner -loglevel error -ss $time -i (Join-Path $videoDirectory ($capture[0] + '.mp4')) -frames:v 1 (Join-Path $captureDirectory ($capture[2] + '.jpg'))
    if ($LASTEXITCODE -ne 0) { throw ('Extraction echouee : ' + $capture[2]) }
}
$pairs = @(
    @('pcvr_jus','quest_jus','comparaison_jus'),
    @('pcvr_explosion','quest_explosion','comparaison_explosion'),
    @('pcvr_volume','quest_fin','volume_et_fin')
)
foreach ($pair in $pairs) {
    & $FfmpegPath -y -hide_banner -loglevel error -i (Join-Path $captureDirectory ($pair[0] + '.jpg')) -i (Join-Path $captureDirectory ($pair[1] + '.jpg')) -filter_complex '[0:v][1:v]hstack=inputs=2' -frames:v 1 (Join-Path $captureDirectory ($pair[2] + '.jpg'))
    if ($LASTEXITCODE -ne 0) { throw ('Juxtaposition echouee : ' + $pair[2]) }
}
Write-Output '10 captures et 3 comparaisons extraites, videos originales inchangees.'
