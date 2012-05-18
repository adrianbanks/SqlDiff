CREATE TABLE [dbo].[Companies]
(
[Identity] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (255) NOT NULL,
)
GO

ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [PK_Companies_Identity] PRIMARY KEY CLUSTERED ([Identity])
GO

---------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Offices]
(
[Identity] [int] NOT NULL IDENTITY(1, 1),
[City] [nvarchar] (255) NOT NULL,
[CompanyIdentity] [int] NOT NULL
)
GO

ALTER TABLE [dbo].[Offices] ADD CONSTRAINT [PK_Offices_Identity] PRIMARY KEY CLUSTERED ([Identity])
GO

ALTER TABLE [dbo].[Offices] ADD CONSTRAINT [FK_Offices_Company] FOREIGN KEY ([CompantyIdentity]) 
	REFERENCES [dbo].[Companies] ([Identity])
GO

---------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Employees]
(
[Identity] [int] NOT NULL IDENTITY(1, 1),
[Guid] [uniqueidentifier] NOT NULL CONSTRAINT [DK_Employees_Guid] DEFAULT NEWID(),
[FirstName] [nvarchar] (255) COLLATE Latin1_General_BIN NOT NULL,
[MiddleName] [nvarchar] (50) NULL,
[LastName] [nvarchar] (255) NOT NULL,
[DOB] [date] NOT NULL,
[Age] [int] NULL,
[NumberOfYears] [int] NOT NULL CONSTRAINT [DK_Employees_NumberOfYears] DEFAULT (0),
[Salary] [decimal] (10,2) NULL,
[OfficeIdentity] [int] NOT NULL
)
GO

ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [PK_Employees_Identity] PRIMARY KEY CLUSTERED ([Identity])
GO

ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_Employees_Office] FOREIGN KEY ([OfficeIdentity]) 
	REFERENCES [dbo].[Offices] ([Identity])
GO

ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [UQ_Employees_Guid] UNIQUE ([Guid])
GO

CREATE NONCLUSTERED INDEX [IX_Employees_LastName] ON [dbo].[Employees] ([LastName])
GO

ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [CK_Employees_MinimunNameLength] CHECK
    (
        (LEN([FirstName]) + LEN([LastName])) > 4
    )
GO

---------------------------------------------------------------------------------------------------------------
CREATE VIEW [dbo].[EmployeesOverFifty]
AS
	SELECT * FROM [dbo].[Employees]	
	WHERE DATEDIFF(yy, [dbo].[Employees].[DOB], GETDATE()) -
			CASE WHEN [dbo].[Employees].[DOB] <= DATEADD(yy, DATEDIFF(yy, GETDATE(), [dbo].[Employees].[DOB]), GETDATE())
				THEN 0 
				ELSE 1 
			END > 50
GO

CREATE VIEW [dbo].[EmployeesWithNoMiddleName]
AS
	SELECT * FROM [dbo].[Employees]	
	WHERE [MiddleName] IS NULL
GO

CREATE VIEW [dbo].[OfficesAndTheirEmployees]
AS
	SELECT O.[City], E.[FirstName] + ' ' + E.[LastName] FROM [dbo].[Offices] AS O, [dbo].[Employees] AS E
	WHERE O.[Identity] = E.[OfficeIdentity]
GO

---------------------------------------------------------------------------------------------------------------
CREATE TRIGGER [EmployeeCalculateAge]
ON [dbo].[Employees] AFTER INSERT, UPDATE
AS 
BEGIN
	DECLARE @today datetime
	SELECT @today = GETDATE()

	UPDATE [dbo].[Employees]
	SET [Age] = DATEDIFF(yy, [dbo].[Employees].[DOB], @today) -
				CASE WHEN [dbo].[Employees].[DOB] <= DATEADD(yy, DATEDIFF(yy, @today, [dbo].[Employees].[DOB]), @today)
					THEN 0 
					ELSE 1 
				END
	WHERE [Identity] IN (SELECT [Identity] FROM INSERTED)
END 
GO

CREATE TRIGGER [EmployeeSalaryCannotBeBelowMinimum]
ON [dbo].[Employees] AFTER INSERT, UPDATE
AS 
BEGIN
    IF EXISTS 
    (
		-- the minimum allowed salary is $20,000
        SELECT 1 FROM [dbo].[Employees]
        WHERE [Salary] < 20000
    )
    BEGIN
        RAISERROR ('Salary cannot be below the minimum allowed', 16, 10)
        ROLLBACK TRANSACTION
    END 
END
GO

CREATE TRIGGER [EmployeeNumberOfYearsService]
ON [dbo].[Employees] AFTER INSERT, UPDATE
AS 
BEGIN
    IF EXISTS 
    (
		-- cannot have worked for less that 0 years
        SELECT 1 FROM [dbo].[Employees]
        WHERE [NumberOfYears] < 0
    )
    BEGIN
        RAISERROR ('NumberOfYears cannot be negative', 16, 10)
        ROLLBACK TRANSACTION
    END 
END
GO
