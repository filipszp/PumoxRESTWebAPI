delete from [Employee]
GO
delete from [Company]
GO
drop table [Employee]
GO
drop table [Company]
GO

CREATE TABLE [Company] (
  [Id] BIGINT IDENTITY (1,1)  NOT NULL,
  [CompanyName] nvarchar(255)  NOT NULL,
  [EstablishmentYear] int NOT NULL
);
GO
ALTER TABLE [Company] ADD CONSTRAINT [PK_CompanyId] PRIMARY KEY ([Id]);
GO


CREATE TABLE [Employee] (
  [Id] BIGINT IDENTITY (1,1)  NOT NULL,
  [FirstName] nvarchar(100)  NOT NULL,
  [LastName] nvarchar(100) NOT NULL,
  [DateOfBirth] datetime,
  [JobTitle] nvarchar(50) NOT NULL,
  [Company_Id] BIGINT NOT NULL
);
GO
ALTER TABLE [Employee] ADD CONSTRAINT [PK_EmployeeId] PRIMARY KEY ([Id]);
GO
ALTER TABLE [Employee]     
ADD CONSTRAINT FK_Employee_Company_Id FOREIGN KEY (Company_Id)     
    REFERENCES [Company] (Id);
GO



insert into [Company]([CompanyName],[EstablishmentYear]) values('Company 1',2010);
GO
insert into [Company]([CompanyName],[EstablishmentYear]) values('Company 2',2015);
GO
insert into [Employee]([FirstName],[LastName],[DateOfBirth],[JobTitle],[Company_Id]) values('Kowalski','Jan',CONVERT(DATETIME, '01-21-74', 10),'Manager',50);
GO
insert into [Employee]([FirstName],[LastName],[DateOfBirth],[JobTitle],[Company_Id]) values('Nowak','Jan',CONVERT(DATETIME, '03-10-79', 10),'Administrator',1);
GO
insert into [Employee]([FirstName],[LastName],[DateOfBirth],[JobTitle],[Company_Id]) values('Scott','Tiger',CONVERT(DATETIME, '01-08-80', 10),'Developer',2);
GO

