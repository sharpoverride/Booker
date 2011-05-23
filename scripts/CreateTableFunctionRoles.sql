if exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'FunctionRoles')
begin
	truncate table FunctionRoles
	drop table FunctionRoles
end
 
if not exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'FunctionRoles')
begin
	create table FunctionRoles(
		RoleId_FK uniqueidentifier not null,
		FunctionId_FK uniqueidentifier not null,

		constraint FunctionRoles_Roles_FK_Constraint foreign key  ( RoleId_FK ) references Roles(Id),
		constraint FunctionRoles_Functions_FK_Constraint foreign Key  ( FunctionId_FK ) references Functions(Id)
	)
end