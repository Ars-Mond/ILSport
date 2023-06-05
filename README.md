# ILSport
## Build application
To build the application, run this line in the console:
```
 dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true -p:IncludeNativeLibrariesInSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true  -c Build -o publish
```
*You may need to install the .NET 7 SDK or/and Runtime.*
 
