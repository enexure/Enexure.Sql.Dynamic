function ResolveMsBuildPath()
{
	$root = "C:\Windows\Microsoft.NET\"

	if (Test-Path "$root\Framework64") {
		$root = "$root\Framework64"
	} else {
		$root = "$root\Framework"
	}

	# TODO: Get latest folder starting with v
	
	#"v4.5\"
	#"v4.0.30319"
	
	return $root
}

function ResolveNuGetPath()
{
	return Resolve-Path ".nuget\"
}

$NuGetPath = ResolveNuGetPath
$MsBuildPath = ResolveMsBuildPath

Write-Host "NuGet: $NuGetPath"
Write-Host "MsBuild: $MsBuildPath"

$env:Path += ";$MsBuildPath"
$env:Path += ";$NuGetPath"

