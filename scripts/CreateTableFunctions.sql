
if exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'Functions')
begin
	truncate table Functions
	drop table Functions
end

if not exists( Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'Functions' )
begin

	create table [Functions](
		Id uniqueidentifier primary key not null,
		Name varchar(250) not null,
		)
end 
