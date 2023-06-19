
dotnet new webapi -f net6.0 -minimal -n MessageBroker
dotnet ef migrations add initialmigration
dotnet tool update --global dotnet-ef
dotnet ef database update
dotnet run

dotnet new console -f net6.0 -n Subcriber