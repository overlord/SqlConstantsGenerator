param(
	[string] $SolutionDir
)
# ------------------------------
$projectDir = "$SolutionDir\SqlConstantsGenerator"
[xml] $nuspec = Get-Content -Path "$projectDir\SqlConstantsGenerator.nuspec" -Encoding "UTF8"
$packageVer = $nuspec.package.metadata.version
$packageDir = "$SolutionDir\packages\SqlConstantsGenerator.$packageVer"
# ------------------------------
New-Item -Path "$packageDir\lib\net472\" -ItemType "Directory" -Force | Out-Null
New-Item -Path "$packageDir\build\" -ItemType "Directory" -Force | Out-Null
# ------------------------------
Copy-Item "$projectDir\bin\Debug\SqlConstantsGenerator.dll" "$packageDir\lib\net472\"
Copy-Item "$projectDir\build\*.targets" "$packageDir\build\"
# ------------------------------
