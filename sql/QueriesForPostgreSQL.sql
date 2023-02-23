insert into "Commands" ("HowTo", "Platform", "CommandLine")
values ('Create an EF migration', 'Entity Framework Core Command Line','dotnet ef migrations add');

insert into "Commands" ("HowTo", "Platform", "CommandLine")
VALUES ('Apply Migrations to DB', 'Entity Framework Core Command Line','dotnet ef database update');

select * from "Commands";


insert into "Commands" ("HowTo", "Platform", "CommandLine")
VALUES ('Create an EF Migration', 'Entity Framework Package Manager Console','add-migration <name of
migration>');

insert into "Commands" ("HowTo", "Platform", "CommandLine")
VALUES ('Apply Migrations to DB', 'Entity Framework Package Manager Console','update database');
