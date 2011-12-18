IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GenerateLabExaminationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GenerateLabExaminationNumber]
GO
Create PROCEDURE dbo.sp_GenerateLabExaminationNumber
(
    @Length int
)

AS
BEGIN
DECLARE @RandomID varchar(32)
	DECLARE @counter smallint
	DECLARE @RandomNumber float
	DECLARE @RandomNumberInt tinyint
	DECLARE @CurrentCharacter varchar(1)
	DECLARE @ValidCharacters varchar(255)
	DECLARE @False bit
	SET @ValidCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'
	DECLARE @ValidCharactersLength int
	SET @ValidCharactersLength = len(@ValidCharacters)
	SET @CurrentCharacter = ''
	SET @RandomNumber = 0
	SET @RandomNumberInt = 0
	SET @RandomID = ''
	SET NOCOUNT ON

	SET @counter = 1
	SET @False=1
	WHILE @False != 0
	BEGIN
			
		WHILE @counter < (@Length + 1)

		BEGIN

				SET @RandomNumber = Rand()
				SET @RandomNumberInt = Convert(tinyint, ((@ValidCharactersLength - 1) * @RandomNumber + 1))

				SELECT @CurrentCharacter = SUBSTRING(@ValidCharacters, @RandomNumberInt, 1)

				SET @counter = @counter + 1

				SET @RandomID = @RandomID + @CurrentCharacter
		END
		select @False = count(ExaminationNumber)
		from LabExamination
		where ExaminationNumber = @RandomID
	END
	select @RandomID
END
