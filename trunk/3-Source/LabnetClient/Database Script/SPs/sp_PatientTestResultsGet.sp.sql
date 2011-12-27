/****** Object:  StoredProcedure [dbo].[sp_PatientTestResultsGet]    Script Date: 11/17/2011 18:37:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_PatientTestResultsGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_PatientTestResultsGet]
GO

CREATE PROCEDURE [dbo].[sp_PatientTestResultsGet] 
	-- Add the parameters for the stored procedure here
	@LabExaminationId	int
AS
BEGIN
	select 
			   ROW_NUMBER() OVER(ORDER BY t.Id DESC) AS 'STT',
			   t.Name ,
			   r.Value 'Result',
			   t.Range,
			   t.Unit,
			   a.Status,
			   a.Id 'AnalysisId',
			   t.Id 'TestId',
			   r.Id 'ResultId'
			    
		from	LabExamination l											inner join
				PatientItem    p on l.Id = p.LabExaminationId				inner join
				Analysis	   a on a.PatientItemId = p.Id					inner join
				Test		   t on a.TestId = t.Id							left join
				Result		   r on a.Id = r.AnalysisId				
		where l.Id=@LabExaminationId
		
END

GO


