CREATE OR ALTER VIEW [dbo].[GameView] 
WITH SCHEMABINDING AS SELECT
  Game.Id,
  Game.[Name],
  Game.[Description],
  Game.[CategoryId],
  Game.[CompanyId],
  Category.[Name] as CategoryName,
  Category.[Description] as CategoryDescription,
  Company.[Name] as CompanyName,
  Company.[Description] as CompanyDescription
  FROM [dbo].[Games] Game
  INNER JOIN dbo.Categories Category ON Category.Id = Game.CategoryId
  INNER JOIN dbo.Companies Company ON Company.Id = Game.CompanyId
GO


CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex]
    ON [dbo].[GameView]([Id] ASC, [CategoryId] ASC, [CompanyId] ASC);
GO