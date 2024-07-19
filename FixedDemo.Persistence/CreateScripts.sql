CREATE TABLE demo_case.dbo.Users (
	Id uniqueidentifier PRIMARY KEY NONCLUSTERED DEFAULT NEWID(),
	CreatedAt datetime NULL,
	UpdatedAt datetime NULL,
	IsActive bit DEFAULT 1 NOT NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	Name nvarchar(100) NULL,
	Email nvarchar(100) NOT NULL,
	PhoneNumber nvarchar(20) NULL,
	PasswordHash varbinary(MAX) NOT NULL,
	PasswordSalt varbinary(MAX) NOT NULL,
);

CREATE TABLE demo_case.dbo.Assets (
	Id uniqueidentifier PRIMARY KEY NONCLUSTERED DEFAULT NEWID(),
	CreatedAt datetime NULL,
	UpdatedAt datetime NULL,
	IsActive bit DEFAULT 1 NOT NULL,
	IsDeleted bit DEFAULT 0 NOT NULL,
	Brand nvarchar(100) NULL,
	Model nvarchar(100) NULL,
	SerialNumber nvarchar(100) NULL,
);
