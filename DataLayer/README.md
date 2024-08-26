## What I have done

Created a .net project, class library, one project.

dotnet create solution -o sanddata.no.common
project name: datalayer

## Run the command
1. Added some project information to project file
2. Created a file nuget.config with url to project in git
3. nuget spec .csproj file created a file called  DataLayer.nuspec
3. dotnet pack
4. dotnet nuget push --source "MyGitHub" --api-key "PERSONAL ACCESS TOKEN" .\bin\debug\sanddata.no.common.datalayer.1.0.1.nupkg
2. 