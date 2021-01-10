param (
    $fileName = "Directory.Build.props",
    $version = "1.0.0.0"
)

$dateNow = [System.DateTime]::Now
$formattedDate = $dateNow.Ticks;
$xmlDocument = New-Object System.Xml.XmlDocument

$xmlDocument.Load($fileName)

$currentVersion = $xmlDocument.Project.PropertyGroup.Version
$assemblyVersion = $xmlDocument.Project.PropertyGroup.AssemblyVersion
$fileVersion = $xmlDocument.Project.PropertyGroup.FileVersion
$applicationVersion = $xmlDocument.Project.PropertyGroup.ApplicationVersion

Write-Output "Current Versions:"

Write-Output "Version: $currentVersion"
Write-Output "Assembly version: $assemblyVersion"
Write-Output "File version: $fileVersion"
Write-Output "Application version:" $applicationVersion

Write-Output "Backing up $fileName"

Write-Output "Updating version to $version"

$xmlDocument.Project.PropertyGroup.Version = $version
$xmlDocument.Project.PropertyGroup.AssemblyVersion = $version
$xmlDocument.Project.PropertyGroup.FileVersion = $version
$xmlDocument.Project.PropertyGroup.ApplicationVersion = $version

$xmlDocument.Save($fileName)