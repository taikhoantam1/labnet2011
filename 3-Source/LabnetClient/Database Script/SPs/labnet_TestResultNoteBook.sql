IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[labnet_TestResultNoteBook]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[labnet_TestResultNoteBook]
GO
/****** Object:  StoredProcedure [dbo].[labnet_TestResultNoteBook]    Script Date: 03/02/2012 20:15:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[labnet_TestResultNoteBook]
		@StartDate	datetime
	,	@EndDate	datetime
AS
BEGIN
	set @EndDate =DATEADD(dd, 1, @EndDate)
	SELECT	distinct	
		p.FirstName, p.Gender, p.Age, p.Phone, 
		lx.PatientId, 
		lx.ExaminationNumber, 
		lx.Id, 
		lx.ReceivedDate, 
		lx.CreatedBy, 
		a.PatientItemId, 
		a.TestId, 
		t.Name, 
		r.Value, 
		pn.Name 'LabName'
	FROM	
					LabExamination lx 
		left JOIN	[Partner] pn		ON lx.PartnerId = pn.Id
		INNER JOIN  Patient p		ON lx.PatientId = p.Id 
		INNER JOIN  PatientItem	pai	ON lx.Id = pai.LabExaminationId 
		INNER JOIN	Analysis a		ON a.PatientItemId = pai.Id 
		INNER JOIN	Test t			ON t.Id = a.TestId
		INNER JOIN	Result r		ON r.AnalysisId = a.Id
	WHERE 
		lx.ReceivedDate between @StartDate and @EndDate
		and a.Status > 1
	order by pn.Name , lx.ReceivedDate
END
GO
