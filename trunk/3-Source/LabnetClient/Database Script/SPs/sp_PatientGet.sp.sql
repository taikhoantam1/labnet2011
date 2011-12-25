/****** Object:  StoredProcedure [dbo].[sp_PatientsGet]    Script Date: 11/17/2011 18:37:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_PatientsGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_PatientsGet]
GO

CREATE PROCEDURE [dbo].[sp_PatientsGet] 
	-- Add the parameters for the stored procedure here
	@PatientId			int = null,
	@PatientName		nvarchar(100) = null,  
	@Phone				nvarchar(15) = null,
	@Email				nvarchar(50) = null,
	@IdentifierNumber	nvarchar(10) = null,
	@Address			nvarchar(100) = null,
	@PartnerId			int=null			,
	@OrderNumber		int=null			,
	@ReceivedDate		datetime=null			
AS
BEGIN
		Select	
				p.*
				l.OrderNumber,
				l.ReceivedDate
		from	Patient p								inner join
				LabExamination l on p.Id = l.PatientId
		where		(@PatientName is null or p.FirstName like '%'+@PatientName+'%')
				and	(@Phone is null or p.Phone like '%'+@Phone+'%')
				and	(@Email is null or p.Email like '%'+@Email+'%')
				and	(@IdentifierNumber is null or p.IndentifierNumber like '%'+@IdentifierNumber+'%')
				and	(@PartnerId is null or l.PartnerId = @PartnerId)
				and (@OrderNumber is null or l.OrderNumber = @OrderNumber)
				and (@ReceivedDate is null or convert(varchar,@ReceivedDate,101) =convert(varchar,l.ReceivedDate ,101))
				and (@PatientId is null or p.Id=@PatientId)
		
END

GO


