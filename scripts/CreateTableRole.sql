
if exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'Roles')
begin
	truncate table Roles
	drop table Roles
end


if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'Roles')
begin

create table [Roles]
(
	Id uniqueidentifier primary key,
	Name varchar(250) NOT NULL,
	[Description] varchar(500) NULL
)
end