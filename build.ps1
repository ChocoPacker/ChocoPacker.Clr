param($buildNumber = 1,
    [switch]
    $localDotNet)
    
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

exit 0