# ILSport
## Build application
To build the application, run this line in the console:
### Win 64
```
 dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true -p:IncludeNativeLibrariesInSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true  -c Release -o publish
```
*You may need to install the .NET 5/7 SDK or/and Runtime.*
 
