IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FinantialManagement')
BEGIN
    CREATE DATABASE FinantialManagement;
END                           

create table categories(
    Id int not null primary key identity(1,1),
    Name varchar(200) not null unique,
    CreatedAt DateTime default GetDate()
)