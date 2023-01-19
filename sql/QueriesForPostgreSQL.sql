insert into "Commands" ("HowTo", "Platform", "CommandLine")
values ('Create an EF migration', 'Entity Framework Core Command Line','dotnet ef migrations add');

insert into "Commands" ("HowTo", "Platform", "CommandLine")
VALUES ('Apply Migrations to DB', 'Entity Framework Core Command Line','dotnet ef database update');

select * from "Commands";