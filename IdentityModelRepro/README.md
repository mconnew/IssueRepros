## IdentityModel dependency repro
This repro demonstrates the issue opened in https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/3112  

The solution by default uses version 8.3.0. I've created a build property called `IdentityModelVersion` that can be used to modify the version of the Microsoft.IdentityModel packages that are consumed. Running `dotnet restore` in the root repro folder will succeed as version 8.3.0 is being used. To repro the error, run:
```cmd
dotnet restore /p:IdentityModelVersion=8.3.1
```

The following error will be output when attempting to restore:
```
D:\git\IssueRepros\IdentityModelRepro>dotnet restore /p:IdentityModelVersion=8.3.1
    D:\git\IssueRepros\IdentityModelRepro\BuildErrorApp\BuildErrorApp.csproj : error NU1605:
      Warning As Error: Detected package downgrade: Microsoft.Extensions.Logging.Abstractions from 9.0.0 to 8.0.2. Refer
      ence the package directly from the project to select a different version.
       BuildErrorApp -> NetStandardLibrary -> Microsoft.IdentityModel.Tokens 8.3.1 -> Microsoft.Extensions.Logging.Abstr
      actions (>= 9.0.0)
       BuildErrorApp -> NetStandardLibrary -> Microsoft.Extensions.Logging.Abstractions (>= 8.0.2)

Restore failed with 1 error(s) in 1.9s
```