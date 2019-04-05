param(
[string]$SolutionFolder
);

[string]$userTypeGeneratorProjectDir = "$SolutionFolder`SqlConstantsGenerator";

[xml]$xmlNuspec = Get-Content -Path $userTypeGeneratorProjectDir\SqlConstantsGenerator.nuspec
[string]$packageVersion = $xmlNuspec.package.metadata.version;

[string]$packageDir = "$SolutionFolder`\packages\SqlConstantsGenerator.$packageVersion";

Copy-Item "$userTypeGeneratorProjectDir`\bin\Debug\SqlConstantsGenerator.dll" "$packageDir\lib\net452\"

Copy-Item "$userTypeGeneratorProjectDir`\build\*.targets" "$packageDir\build\"
