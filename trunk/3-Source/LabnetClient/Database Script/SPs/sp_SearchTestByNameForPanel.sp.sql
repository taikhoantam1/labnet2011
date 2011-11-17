/****** Object:  StoredProcedure [dbo].[sp_SearchTestByNameForPanel]    Script Date: 11/17/2011 18:41:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SearchTestByNameForPanel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SearchTestByNameForPanel]
GO

CREATE PROCEDURE [dbo].[sp_SearchTestByNameForPanel]
	@FilterText nvarchar(100)=null,
	@SearchType	nvarchar(10)='CONTAINS'
as
Begin
	if(@SearchType ='WORD')
	begin
		select t.Id, t.Name, ts.Name AS TestSectionName
		from Test t INNER JOIN TestSection ts on t.TestSectionId = ts.Id
		where(@FilterText is null or dbo.fuChuyenCoDauThanhKhongDau(t.Name) like @FilterText+'%')
	end
	else
	begin
		select t.Id, t.Name, ts.Name AS TestSectionName
		from Test t INNER JOIN TestSection ts on t.TestSectionId = ts.Id
		where(@FilterText is null or dbo.fuChuyenCoDauThanhKhongDau(t.Name) like '%'+@FilterText+'%')
	end
End

GO


