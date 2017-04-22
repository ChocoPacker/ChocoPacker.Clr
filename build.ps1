param($buildNumber = 1,
    [switch]
    $localDotNet)

# I don't like Microsoft watching CI  
$env:DOTNET_CLI_TELEMETRY_OPTOUT=1

# Copy-Paste from stackoverflow (required for PS 4)
function ZipFiles($zipfilename, $sourcedir){
   Add-Type -Assembly System.IO.Compression.FileSystem
   $compressionLevel = [IO.Compression.CompressionLevel]::Optimal
   [IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)
}

# If specified we will prepend PATH with local dotnet CLI SDK
if ($localDotNet){
    . .\downloadDotNet.ps1
    Download-DotNet
}

& dotnet msbuild .\ChocoPacker.Clr.targets /t:BuildAndTest /p:VersionSuffix=$buildNumber
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to build projects'
    exit 1
}

& dotnet msbuild .\ChocoPacker.Clr.targets /t:PackAndPublish /p:VersionSuffix=$buildNumber
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to create nuget'
    exit 1
}

$publishDir = ".\src\ChocoPacker.Facade.Node\bin\Release\netcoreapp1.0\publish"
ZipFiles "ChocoPacker.Node.zip" $publishDir

exit 0