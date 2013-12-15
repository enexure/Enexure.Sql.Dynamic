. ".build\init.ps1"

$SolutionDir = Resolve-Path .

msbuild "Enexure.Sql.Dynamic.sln"