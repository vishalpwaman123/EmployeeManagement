USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spcheckemailId]    Script Date: 8/5/2020 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spcheckemailId] 
	(
	@EmailId varchar(30),
	@Flag int
	)
AS
BEGIN
	DECLARE @status int
	set @status=0
    -- Insert statements for procedure here
	
	BEGIN TRY

	IF (@Flag > 0)
			IF EXISTS (SELECT * FROM EmployeeTable WHERE [EmailId] = @EmailID)
			BEGIN
				set @status=1
				select @status
			END
			ELSE
		    BEGIN
				select @status
			END
    ELSE
            IF EXISTS (SELECT * FROM UserTable WHERE [EmailId] = @EmailID)
			BEGIN
				set @status=1
				select @status
			END
			ELSE
		    BEGIN
				select @status
			END

	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;

END
