$root = (split-path -parent $MyInvocation.MyCommand.Definition)
$lib = "$root\.package\lib\45\"
If (Test-Path $lib)
{
	Remove-Item $lib -recurse
}
new-item -Path $lib -ItemType directory
new-item -Path $root\.nupkg -ItemType directory -force
Copy-Item $root\CodeAnalysisRule\bin\Debug\* $lib

Write-Host "Setting .nuspec version tag to $env:GitVersion_NuGetVersion"

$content = (Get-Content $root\Tibre.CodeAnalysisRule.nuspec -Encoding UTF8) 
$content = $content -replace '\$version\$',$env:GitVersion_NuGetVersion

$content | Out-File $root\.package\Tibre.CodeAnalysisRule.compiled.nuspec -Encoding UTF8

& $root\.nuget\NuGet.exe pack $root\.package\Tibre.CodeAnalysisRule.compiled.nuspec -Version $env:GitVersion_NuGetVersion -OutputDirectory $root\.nupkg
