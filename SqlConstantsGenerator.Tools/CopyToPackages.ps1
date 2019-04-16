param(
	[string] $SolutionDir
)
# ------------------------------
function Copy-MyFile($from, $to) {
	Write-Host "Copying '$from' to '$to'..."
	Copy-Item $from $to
}
# ------------------------------
$projectDir = "$SolutionDir\SqlConstantsGenerator"
[xml] $nuspec = Get-Content -Path "$projectDir\SqlConstantsGenerator.nuspec" -Encoding "UTF8"
$packageVer = $nuspec.package.metadata.version
$packageDir = "$SolutionDir\packages\SqlConstantsGenerator.$packageVer"
# ------------------------------
New-Item -Path "$packageDir\lib\net472\" -ItemType "Directory" -Force | Out-Null
New-Item -Path "$packageDir\build\" -ItemType "Directory" -Force | Out-Null
# ------------------------------
Copy-MyFile "$projectDir\bin\Debug\SqlConstantsGenerator.dll" "$packageDir\lib\net472\"
Copy-MyFile "$projectDir\build\*.targets" "$packageDir\build\"
# ------------------------------
