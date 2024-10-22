IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FinantialManagement')
BEGIN
    CREATE DATABASE FinantialManagement;
END                           

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'categories')
BEGIN
create table categories(
    Id int not null primary key identity(1,1),
    Name varchar(200) not null unique,
    CreatedAt DateTime default GetDate()
)
END