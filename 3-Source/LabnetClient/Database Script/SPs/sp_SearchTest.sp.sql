/****** Object:  StoredProcedure [dbo].[sp_SearchTest]    Script Date: 11/17/2011 18:37:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SearchTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SearchTest]
GO

CREATE PROCEDURE [dbo].[sp_SearchTest] 
	-- Add the parameters for the stored procedure here
	@TestName nvarchar(100) = null,  
	@TestSectionName nvarchar(100) = null,
	@PanelName nvarchar(100) = null
AS
BEGIN
	BEGIN
		SELECT t.Id, t.Name as TestName, ts.Name as TestSectionName, t.Range, t.Unit, p.Name as PanelName
		FROM Test t
		INNER JOIN TestSection ts on t.TestSectionId = ts.Id
		INNER JOIN PanelItem pitem on pitem.TestId = t.Id
		INNER JOIN Panel p on pitem.PanelId = p.Id
		WHERE UPPER(t.Name) like '%' + UPPER(@TestName) + '%'
			AND t.IsActive = 1
			AND UPPER(ts.Name) LIKE CASE WHEN @TestSectionName IS NOT NULL THEN '%' + UPPER(@TestSectionName) + '%' ELSE '%%' END
		ORDER BY t.SortOrder
	END
END

GO


