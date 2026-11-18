[CmdletBinding()]
param(
    [string]$PdfPath,
    [string]$OutputDirectory = '.utmp/report-pages'
)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Runtime.WindowsRuntime
$null = [Windows.Storage.StorageFile,Windows.Storage,ContentType=WindowsRuntime]
$null = [Windows.Data.Pdf.PdfDocument,Windows.Data.Pdf,ContentType=WindowsRuntime]
$null = [Windows.Storage.Streams.InMemoryRandomAccessStream,Windows.Storage.Streams,ContentType=WindowsRuntime]
$null = [Windows.Storage.Streams.DataReader,Windows.Storage.Streams,ContentType=WindowsRuntime]
$genericAsTask = [System.WindowsRuntimeSystemExtensions].GetMethods() | Where-Object {
    $_.Name -eq 'AsTask' -and $_.IsGenericMethodDefinition -and $_.GetGenericArguments().Count -eq 1 -and
    $_.GetParameters().Count -eq 1 -and $_.GetParameters()[0].ParameterType.Name -eq 'IAsyncOperation`1'
} | Select-Object -First 1
$actionAsTask = [System.WindowsRuntimeSystemExtensions].GetMethods() | Where-Object {
    $_.Name -eq 'AsTask' -and -not $_.IsGenericMethod -and $_.GetParameters().Count -eq 1 -and
    $_.GetParameters()[0].ParameterType.FullName -eq 'Windows.Foundation.IAsyncAction'
} | Select-Object -First 1
function Await-Result($Operation, [Type]$ResultType) {
    $task = $genericAsTask.MakeGenericMethod($ResultType).Invoke($null,@($Operation))
    $task.GetAwaiter().GetResult()
}
$file = Await-Result ([Windows.Storage.StorageFile]::GetFileFromPathAsync((Resolve-Path -LiteralPath $PdfPath).Path)) ([Windows.Storage.StorageFile])
$document = Await-Result ([Windows.Data.Pdf.PdfDocument]::LoadFromFileAsync($file)) ([Windows.Data.Pdf.PdfDocument])
[IO.Directory]::CreateDirectory([IO.Path]::GetFullPath($OutputDirectory)) | Out-Null
for ($i = 0; $i -lt $document.PageCount; $i++) {
    $page = $document.GetPage($i)
    $stream = [Windows.Storage.Streams.InMemoryRandomAccessStream]::new()
    try {
        $options = [Windows.Data.Pdf.PdfPageRenderOptions]::new()
        $options.DestinationWidth = 1190
        $options.DestinationHeight = [uint32][Math]::Round(1190 * $page.Size.Height / $page.Size.Width)
        $action = $page.RenderToStreamAsync($stream,$options)
        $task = $actionAsTask.Invoke($null,@($action))
        $null = $task.GetAwaiter().GetResult()
        $reader = [Windows.Storage.Streams.DataReader]::new($stream.GetInputStreamAt(0))
        try {
            $loaded = Await-Result ($reader.LoadAsync([uint32]$stream.Size)) ([uint32])
            $bytes = [byte[]]::new($loaded)
            $reader.ReadBytes($bytes)
            $path = Join-Path ([IO.Path]::GetFullPath($OutputDirectory)) ('page-' + ($i+1) + '.png')
            [IO.File]::WriteAllBytes($path,$bytes)
            Write-Output $path
        } finally { $reader.Dispose() }
    } finally { $stream.Dispose(); $page.Dispose() }
}
