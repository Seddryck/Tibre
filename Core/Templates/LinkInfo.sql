CREATE TABLE [dwh].[CustomerLink]
(
	[CustomerId] Int
	, [CustomerInfoId] Int
	, [DateId] Int
	, [IsFirstDate] Bit
	, [IsLastDate] Bit
)

CREATE UNIQUE INDEX [UX_CustomerLink]
	ON [dwh].[CustomerLink]
	(
		[CustomerId]
		, [DateId]
	)	

CREATE INDEX [IX_CustomerLink_DateId]
	ON [dwh].[CustomerLink]
	(
		[DateId]
	)
	INCLUDE
	(
		[CustomerId]
		, [CustomerInfoId]
	)

CREATE INDEX [FIX_CustomerLink_IsFirstDate]
ON [dwh].[CustomerLink]
	(
		[CustomerId]
	)
	INCLUDE
	(
		[CustomerId]
		, [CustomerInfoId]
		, [DateId]
	)
	WHERE
		IsFirstDate = 1

CREATE INDEX [FIX_CustomerLink_IsLastDate]
ON [dwh].[CustomerLink]
	(
		[CustomerId]
	)
	INCLUDE
	(
		[CustomerId]
		, [CustomerInfoId]
		, [DateId]
	)
	WHERE
		IsLastDate = 1

