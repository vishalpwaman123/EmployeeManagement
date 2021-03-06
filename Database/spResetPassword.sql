USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spResetPassword]    Script Date: 8/5/2020 9:29:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spResetPassword] 
	(
	@NewPassword varchar(30),
	@EmailId varchar(30)
	)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	BEGIN TRY

		if EXISTS(select * from UserTable where EmpId = @EmailId)
		begin
			BEGIN TRANSACTION

				UPDATE UserTable 
				SET UserPassword = @NewPassword 
				WHERE EmailId = @EmailId ;

			COMMIT 
				set @status = 1
				select @status
		end
		else
		begin
			BEGIN TRANSACTION
					UPDATE UserTable 
					SET UserPassword = @NewPassword 
					WHERE EmailId = @EmailId ;
			ROLLBACK
			   select @status
		end

	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;

END
