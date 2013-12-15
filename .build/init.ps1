function ResolveMsBuildPath()
{
	"C:\Windows\Microsoft.NET\Framework\v4.5\"
	"C:\Windows\Microsoft.NET\Framework64\v4.0.30319"
	
	return "TODO"
}

function ResolveNuGetPath()
{
	return Resolve-Path ".nuget\NuGet.exe"
}

$NuGetPath = ResolveNuGetPath
$MsBuildPath = ResolveMsBuildPath

$env:Path += ";$MsBuildPath"
$env:Path += ";$NuGetPath"

