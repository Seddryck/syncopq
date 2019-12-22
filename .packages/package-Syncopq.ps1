param(
    [parameter(Mandatory=$true)]
    [string]$version
)

$root = (split-path -parent $MyInvocation.MyCommand.Definition)

Write-Host "Calculating dependencies ..."

$dependencies = @{}
$solutionRoot = Join-Path ($root) ".."
$projects = Get-ChildItem $solutionRoot | ?{ $_.PSIsContainer -and $_.Name -like "Syncopq.*" -and $_.Name -notLike "*.Testing*"} | select Name, FullName
foreach($proj in $projects)
{
    $projName = $proj.name
    Write-Host "Looking for dependencies in project $projName ..."
    $path = Join-Path ($proj.FullName) "packages.config"
        
    if(Test-Path $path)
    {
        [xml]$packages = Get-Content $path
        foreach($package in $packages.FirstChild.NextSibling.ChildNodes)
        {
            if (!$dependencies.ContainsKey($package.id)) {$dependencies.add($package.id, "<dependency id=""$($package.id)"" version=""$(($package.allowedVersions, $package.version -ne $null)[0])"" />")}
        }
    }
}

Write-Host "Found $($dependencies.Count) dependencies ..."
$depList = $dependencies.Values -join [Environment]::NewLine + "`t`t"

$thisYear = get-date -Format yyyy
Write-Host "Setting copyright until $thisYear"

$nugetVersion = (((nuget help | select -First 1).Split(':')) | select -Last 1).Trim()
Write-Host "Nuget's version: $nugetVersion"
if ($nugetVersion -lt '5.3')
{
    $xpath = ('/package/metadata/icon')
}
else
{
    $xpath = ('/package/metadata/iconUrl')
}

#For Syncopq (dll)
Write-Host "Packaging Syncopq"
$lib = "$root\Temp\Syncopq\lib\net47\"
If (Test-Path $lib)
{
	Remove-Item $lib -recurse
}
new-item -Path $lib -ItemType directory
new-item -Path $root\.nupkg -ItemType directory -force
Copy-Item $root\..\Syncopq.Core\bin\Debug\Syncopq.Core.dll $lib

Write-Host "Setting .nuspec version tag to $version"

$content = (Get-Content $root\Syncopq.nuspec -Encoding UTF8) 
$content = $content -replace '\$version\$',$version
$content = $content -replace '\$thisYear\$',$thisYear
$content = $content -replace '\$depList\$',$depList

$xml = New-Object -TypeName System.Xml.XmlDocument
$xml.LoadXml($content)
$iconNode = $xml.SelectSingleNode($xpath)
$iconNode.ParentNode.RemoveChild($iconNode)

$xml.OuterXml | Out-File $root\Temp\Syncopq\Syncopq.compiled.nuspec -Encoding UTF8

& NuGet.exe pack $root\Temp\Syncopq\Syncopq.compiled.nuspec -Version $version -OutputDirectory $root\.nupkg
Write-Host "Package for Syncopq is ready"