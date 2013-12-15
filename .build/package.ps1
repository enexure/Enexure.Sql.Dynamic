. ".build\init.ps1"

$SolutionDir = Resolve-Path .

$KeyPath = "$SolutionDir\.build\api-key.txt"

$BasePath = "$SolutionDir\Enexure.Sql.Dynamic"

$ArtifactsPath = "$SolutionDir\.artifacts"

if (!(Test-Path $ArtifactsPath)) {
	New-Item $ArtifactsPath -Type Directory
}

msbuild "Enexure.Sql.Dynamic.sln" /property:Configuration=Release

#-BasePath $BasePath
$ProjectFile = "$BasePath\Enexure.Sql.Dynamic.csproj"

Write-Host "Packing: $ProjectFile"
$Output = nuget pack $ProjectFile -OutputDirectory $ArtifactsPath -Prop "Configuration=Release"
$LastLine = $Output[-1]

$EndIndex = $LastLine.LastIndexOf("\") + 1;

$FileName =  $LastLine.SubString($EndIndex, $LastLine.Length - $EndIndex - 2);

$PackagePath = "$ArtifactsPath\$FileName"

Write-Host $PackagePath

$PackagePath = Resolve-Path "$ArtifactsPath\$FileName"

if (Test-Path $KeyPath) {

	$ApiKey = Get-Content $KeyPath

	Write-Host (Test-Path $PackagePath)
	
	nuget push -ApiKey $ApiKey $PackagePath
	
} else {
	Write-Warning "You need an API Key to upload"
}

