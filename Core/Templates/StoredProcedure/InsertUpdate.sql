CREATE PROCEDURE [dwh].[Customer_InsUpd]
(
	@CustomerKey VarChar(50)
	, @CustomerInfoKey UniqueIdentifier
	, @Firstname VarChar(50)
	, @Lastname VarChar(50)
	, @BirthDate Date
	, @FromDate Int
	, @ToDate Int
)
AS
BEGIN
	SET NOCOUNT ON;

	/*Manage Anchor*/

	declare @CustomerId Int;

	select
		@CustomerId = [CustomerId]
	from
		[dwh].[Customer] 
	where 
		[CustomerKey] = @CustomerKey
	;

	if
	(	
		@CustomerId is null
	)
	begin
		insert into
			[dwh].[Customer]
			(
				[CustomerKey]
			)
		values
		(
			@CustomerKey
		);

		select @CustomerId = SCOPE_IDENTITY;
	end

	/*Manage Info*/

	declare @CustomerInfoId Int;

	select
		@CustomerInfoId = [CustomerInfoId]
	from
		[dwh].[CustomerInfo] 
	where 
		[CustomerInfoKey] = @CustomerInfoKey;

	if
	(	
		@CustomerInfoId is null
	)
	begin
		insert into
			[dwh].[CustomerInfo]
			(
				[CustomerInfoKey]
				, [Firstname]
				, [Lastname]
				, [BirthDate]
			)
		values
		(
			@CustomerInfoKey
			, @Firstname
			, @Lastname
			, @BirthDate
		);

		select @CustomerInfoId = SCOPE_IDENTITY;
	end
	else
	begin
		update
			[dwh].[CustomerInfo]
		set
			[Firstname] = @Firstname
			, [Lastname] = @Lastname
			, [BirthDate] = @BirthDate
		where
			[CustomerInfoId] = @CustomerInfoId;
	end

	/*Manage LinkInfo*/

	declare @PreviousFromDate int;
	declare @PreviousToDate int;

	select
		@PreviousFromDate = [DateId]
	from
		[dwh].[CustomerLink]
	where
		[IsFirstDate]=1
		and [CustomerInfoId] = @CustomerInfoId

	if (@PreviousFromDate>@FromDate)
	begin
		update
			[dwh].[CustomerLink]
		set
			[IsFirstDate] = 0
		where
			[IsFirstDate]=1
			and [CustomerInfoId] = @CustomerInfoId;

		insert into
			[dwh].[CustomerLink]
			(
				[CustomerId]
				, [CustomerInfoId]
				, [DateId]
				, [IsFirstDate]
				, [IsLastDate]
			)
		select
			@CustomerId
			, @CustomerInfoId
			, DateId
			, case when DateId = @FromDate then 1 else 0 end
			, 0
		from
			[dwh].[DimDate]
		where
			DimDate between @FromDate and @PreviousFromDate;
	end

	if (@PreviousFromDate<@FromDate)
	begin
		
		delete from
			[dwh].[CustomerLink]
		where
			DateId<@FromDate
			and [CustomerInfoId] = @CustomerInfoId;

		update
			[dwh].[CustomerLink]
		set
			[IsFirstDate] = 1
		where
			[DateId] = @FromDate
			and [CustomerInfoId] = @CustomerInfoId;
	end

	select
		@PreviousToDate = [DateId]
	from
		[dwh].[CustomerLink]
	where
		[IsLastDate]=1
		and [CustomerInfoId] = @CustomerInfoId

	if (@PreviousToDate<@ToDate)
	begin
		update
			[dwh].[CustomerLink]
		set
			[IsLastDate] = 0
		where
			[IsLastDate]=1
			and [CustomerInfoId] = @CustomerInfoId;

		insert into
			[dwh].[CustomerLink]
			(
				[CustomerId]
				, [CustomerInfoId]
				, [DateId]
				, [IsFirstDate]
				, [IsLastDate]
			)
		select
			@CustomerId
			, @CustomerInfoId
			, DateId
			, case when DateId = @ToDate then 1 else 0 end
			, 0
		from
			[dwh].[DimDate]
		where
			DimDate between @PreviousToDate and @ToFromDate;
	end

	if (@PreviousToDate>@ToDate)
	begin
		
		delete from
			[dwh].[CustomerLink]
		where
			DateId>@ToDate
			and [CustomerInfoId] = @CustomerInfoId;

		update
			[dwh].[CustomerLink]
		set
			[IsLastDate] = 1
		where
			[DateId] = @ToDate
			and [CustomerInfoId] = @CustomerInfoId;
	end
END;
