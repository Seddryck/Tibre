CREATE TABLE [dwh].[Customer]
(
	[CustomerId] Int IDENTITY(1,1)
	, [CustomerKey] VarChar(50)
	CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED (CustomerId)
)

CREATE UNIQUE INDEX [BK_Customer]
	ON [dwh].[Customer]
	(
		[CustomerKey]
	)	
	