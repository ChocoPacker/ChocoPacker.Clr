param($buildNumber = 1,
    [switch]
    $localDotNet)
    
# If specified we will prepend PATH with local dotnet CLI SDK
if ($localDotNet){
    . .\downloadDotNet.ps1
    Download-DotNet
}

& dotnet restore ChocoPacker.Common
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to restore dotnet dependencies for Common'
    exit 1
}

& dotnet pack ChocoPacker.Common -c Release --version-suffix=$buildNumber
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to create Common nuget'
    exit 1
}

& dotnet restore ChocoPacker.Loader
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to restore dotnet dependencies for Loader'
    exit 1
}

& dotnet pack ChocoPacker.Loader -c Release --version-suffix=$buildNumber
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to create Loader nuget'
    exit 1
}

& dotnet restore ChocoPacker.Loader.Tests
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to restore dotnet test dependencies'
    exit 1
}

& dotnet build ChocoPacker.Loader.Tests -c Release
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to build unit tests'
    exit 1
}

& dotnet test ChocoPacker.Loader.Tests\ChocoPacker.Loader.Tests.csproj
if ($LASTEXITCODE -ne 0){
    Write-Output 'Failed to run unit tests'
    exit 1
}

exit 0