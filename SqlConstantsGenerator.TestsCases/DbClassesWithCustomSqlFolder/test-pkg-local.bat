rem nuget update packages.config -id SqlConstantsGenerator -source %1
msbuild DbClassesWithCustomSqlFolder.csproj /t:rebuild
