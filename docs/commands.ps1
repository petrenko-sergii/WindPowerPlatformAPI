
dotnet new web -f netcoreapp3.1 -n WindPowerPlatformAPI.App
dotnet new xunit -f netcoreapp3.1 -n WindPowerPlatformAPI.Tests

dotnet new web -f net6.0 -n WindPowerPlatformAPI.App
dotnet new xunit -f net6.0 -n WindPowerPlatformAPI.Tests
dotnet new xunit -f net6.0 -n WindPowerPlatformAPI.SystemTests

dotnet new sln --name WindPowerPlatformAPISolution

dotnet sln WindPowerPlatformAPISolution.sln add src/WindPowerPlatformAPI.App/WindPowerPlatformAPI.App.csproj test/WindPowerPlatformAPI.Tests/WindPowerPlatformAPI.Tests.csproj
dotnet sln WindPowerPlatformAPISolution.sln add test/WindPowerPlatformAPI.SystemTests/WindPowerPlatformAPI.SystemTests.csproj  --- from solution-folder
dotnet add test/WindPowerPlatformAPI.Tests/WindPowerPlatformAPI.Tests.csproj reference src/WindPowerPlatformAPI.App/WindPowerPlatformAPI.App.csproj

dotnet run
