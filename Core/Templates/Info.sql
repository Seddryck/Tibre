CREATE TABLE [dwh].[CustomerInfo]
(
	[CustomerInfoId] Int IDENTITY(1,1)
	, [CustomerInfoKey] UniqueIdentifier
	, [Firstname] VarChar(50)
	, [Lastname] VarChar(50)
	, [BirthDate] Date
	CONSTRAINT [PK_CustomerInfo] PRIMARY KEY CLUSTERED (CustomerInfoId)
)

CREATE UNIQUE INDEX UX_CustomerInfo_CustomerInfoKey
	ON [dwh].[CustomerInfo]
	(
		[CustomerInfoKey]
	)	
	