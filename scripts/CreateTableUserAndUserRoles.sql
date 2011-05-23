if exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'Users')
begin
	truncate table Users
	drop table Users
end
 
if exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'RoleUsers')
begin
	truncate table RoleUsers
	drop table RoleUsers
end
 
if not exists ( select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'Users')
begin
	create table Users(
		Id uniqueidentifier primary key not null,
		UserName varchar(250) not null
		)
end
if not exists ( select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'RoleUsers')
begin
	create table RoleUsers(
		UserId_FK uniqueidentifier not null,
		RoleId_FK uniqueidentifier not null,

		constraint RoleUsers_Users_FK_Constraint foreign key (UserId_FK) references Users(Id),
		constraint RoleUsers_Roles_FK_Constraint foreign key ( RoleId_FK ) references Roles(Id)
		)
end

select * from INFORMATION_SCHEMA.TABLES
