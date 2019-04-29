CREATE OR ALTER PROCEDURE [dbo].[GetGames]
	@pageNumber int = 1, 
	@pageSize int = 25,
	@term nvarchar(500) = ''
AS
BEGIN
	DECLARE @total INT = 0;

	DECLARE @selectedColumns NVARCHAR(max) = 'Entity.*'

	DECLARE @condition NVARCHAR(max) = IIF(@term IS NULL OR @term = '', '','WHERE Entity.[Name] LIKE ''%'+ @term +'%'' OR Entity.[Description] LIKE ''%'+ @term +'%''')

	--Fill total count
	DECLARE  @result nvarchar(max) = N'Select @count = Count(*) From [dbo].[GameView] as Entity with (noexpand) ' + @condition;
	EXECUTE sp_executesql @result, N'@count int OUTPUT', @count=@total OUTPUT

	IF (@total > 0 AND @total / @pageSize < @pageNumber)
		SET @pageNumber = Ceiling(CAST(@total AS float) / CAST (@pageSize AS float));

	select @total;
	
	DECLARE @startRow int = (@pageNumber - 1) * @pageSize;	
		
	DECLARE @Query NVARCHAR(max);
	SET @Query= 
	N'
	WITH Entities AS (
		Select ' + @selectedColumns + N'
		From [dbo].[GameView] as Entity with (noexpand) '
		+ @condition
		+ N' ORDER BY Entity.Id desc OFFSET ' + CAST(@startRow as nvarchar(max)) + ' ROWS
		FETCH NEXT ' + CAST(@pageSize as nvarchar(max))  + N' ROWS ONLY	
	)
	
	Select '+ @selectedColumns +
	' From Entities as Entity
	order by Entity.Id'	

	--PRINT len(@Query)
	--SELECT @Query; -- for testing	

	EXECUTE sp_executesql @Query
END