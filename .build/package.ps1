. ".build\init.ps1"

$SolutionDir = Resolve-Path .

$ApiKey = Get-Content "$SolutionDir\api-key.txt"

# Check if the Api Key Exists


# ...


$BasePath = Resolve-Path "..\Enexure.Sql.Dynamic"
$ArtifactsPath = Resolve-Path ".\Artifacts"

$PackagePath = ""

$env:Path += ";$NuGetPath"

nuget pack "Enexure.Sql.Dynamic.csproj" -BasePath $BasePath -OutputDirectory $ArtifactsPath -Prop Configuration=Release

nuget push $PackagePath -ApiKey $ApiKey