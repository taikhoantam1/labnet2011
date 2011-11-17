/****** Object:  StoredProcedure [dbo].[sp_SearchTestSection]    Script Date: 11/17/2011 18:43:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SearchTestSection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SearchTestSection]
GO
CREATE  Procedure [dbo].[sp_SearchTestSection]
	@FilterText nvarchar(100)=null,
	@SearchType	nvarchar(10)='CONTAINS'
as
Begin
	if(@SearchType ='WORD')
	begin
		select ts.Id, ts.Name,ts.IsActive
		from TestSection ts
		where(@FilterText is null or dbo.fuChuyenCoDauThanhKhongDau(ts.Name) like @FilterText+'%')
	end
	else
	begin
		select ts.Id, ts.Name,ts.IsActive
		from TestSection ts
		where(@FilterText is null or dbo.fuChuyenCoDauThanhKhongDau(ts.Name) like '%'+@FilterText+'%')
	end
End

GO


