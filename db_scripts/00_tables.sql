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




"Keyword": "<string>",
"EmployeeDateOfBirthFrom": "<DateTime?>",
"EmployeeDateOfBirthTo": "<DateTime?>",
"EmployeeJobTitles": [“<string(enum)>”, …]

select * from Company left outer join Employee on Company.Id=Employee.Company_Id
where (CompanyName like '%imie%'
   OR FirstName like '%imie%'
   OR LastName  like '%imie%');

SELECT this_.Id as id1_0_1_, this_.CompanyName as companyname2_0_1_, this_.EstablishmentYear as establishmentyear3_0_1_, e1_.Id as id1_1_0_, e1_.FirstName as firstname2_1_0_, e1_.LastName as lastname3_1_0_, e1_.DateOfBirth as dateofbirth4_1_0_, e1_.Company_Id as company5_1_0_, e1_.JobTitle as jobtitle6_1_0_ FROM [Company] this_ inner join [Employee] e1_ on this_.Id=e1_.Company_Id WHERE (this_.CompanyName like @p0 or e1_.FirstName like @p1 or e1_.LastName like @p2);@p0 = '%pier%' [Type: String (0:0:0)], @p1 = '%pier%' [Type: String (0:0:0)], @p2 = '%pier%' [Type: String (0:0:0)]




 select Company.Id ,Employee.Id, CompanyName,EstablishmentYear,FirstName,LastName,DateOfBirth , Company_Id , JobTitle from Company left outer join Employee  on Company.Id=Company_Id where CompanyName like '%imie%'
