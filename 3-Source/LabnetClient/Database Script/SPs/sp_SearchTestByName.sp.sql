/****** Object:  StoredProcedure [dbo].[sp_SearchTestByName]    Script Date: 11/17/2011 18:39:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SearchTestByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SearchTestByName]
GO

CREATE PROCEDURE [dbo].[sp_SearchTestByName] 
	@FilterText nvarchar(100)=null,
	@SearchType	nvarchar(10)='CONTAINS'
as
Begin
	if(@SearchType ='WORD')
	begin
		select t.Id, t.Name, t.Cost 
		from Test t
		where(@FilterText is null or dbo.fuChuyenCoDauThanhKhongDau(t.Name) like @FilterText+'%')
	end
	else
	begin
		select t.Id, t.Name, t.Cost 
		from Test t
		where(@FilterText is null or dbo.fuChuyenCoDauThanhKhongDau(t.Name) like '%'+@FilterText+'%')
	end
End

GO


