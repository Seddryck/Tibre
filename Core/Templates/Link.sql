CREATE TABLE [dwh].[CustomerWarehouseLink]
(
	[CustomerId] Int
	, [WarehouseId] Int
	, [DateId] Int
	, [IsFirstDate] Bit
	, [IsLastDate] Bit
)

CREATE INDEX [IX_CustomerWarehouseLink_DateId]
	ON [dwh].[CustomerWarehouseLink]
	(
		[DateId]
	)
	INCLUDE
	(
		[CustomerId]
		, [WarehouseId]
	)


CREATE INDEX [FIX_CustomerWarehouseLink_CustomerId_IsFirstDate]
ON [dwh].[CustomerWarehouseLink]
	(
		[CustomerId]
	)
	INCLUDE
	(
		[WarehouseId]
		, [DateId]
	)
	WHERE
		IsFirstDate = 1

CREATE INDEX [FIX_CustomerWarehouseLink_WarehouseId_IsFirstDate]
ON [dwh].[CustomerWarehouseLink]
	(
		[WarehouseId]
	)
	INCLUDE
	(
		[CustomerId]
		, [DateId]
	)
	WHERE
		IsFirstDate = 1


CREATE INDEX [FIX_CustomerWarehouseLink_CustomerId_IsLastDate]
ON [dwh].[CustomerWarehouseLink]
	(
		[CustomerId]
	)
	INCLUDE
	(
		[WarehouseId]
		, [DateId]
	)
	WHERE
		IsLastDate = 1

CREATE INDEX [FIX_CustomerWarehouseLink_WarehouseId_IsLastDate]
ON [dwh].[CustomerWarehouseLink]
	(
		[WarehouseId]
	)
	INCLUDE
	(
		[CustomerId]
		, [DateId]
	)
	WHERE
		IsLastDate = 1

