
if exists(select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME=N'Bookmarks')
begin
	truncate table Bookmarks
	drop table Bookmarks
end


if not exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'Bookmarks')
begin

create table [Bookmarks]
(
	Id uniqueidentifier primary key,
	Url varchar(250) NOT NULL,
	[Notes] varchar(500) NULL
)
end

