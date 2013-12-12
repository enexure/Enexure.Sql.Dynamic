$ApiKey = ""

$NuGetPath = Resolve-Path "..\.nuget\NuGet.exe"
$BasePath = Resolve-Path "..\Enexure.Sql.Dynamic"
$ArtifactsPath = Resolve-Path ".\Artifacts"

$PackagePath = ""

$env:Path += ";$NuGetPath"

nuget pack "Enexure.Sql.Dynamic.csproj" -BasePath $BasePath -OutputDirectory $ArtifactsPath -Prop Configuration=Release

nuget push $PackagePath -ApiKey $ApiKey