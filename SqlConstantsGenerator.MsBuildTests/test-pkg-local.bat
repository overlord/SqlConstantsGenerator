rem nuget update packages.config -id SqlConstantsGenerator -source %1
msbuild DbClasses.csproj /t:rebuild
