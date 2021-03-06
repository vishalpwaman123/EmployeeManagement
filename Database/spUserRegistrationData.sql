USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spUserRegistrationData]    Script Date: 8/5/2020 9:30:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spUserRegistrationData]
(
    @Firstname varchar(30),  
    @Lastname varchar(30),
    @EmailID varchar(50),
    @UserPassword varchar(200),
    @CurrentAddress varchar(150),
	@MobileNumber varchar(10),
	@Gender varchar(6)
	/*@Date varchar(30)*/
)
AS
BEGIN
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF NOT EXISTS (SELECT * FROM UserTable WHERE [EmailId] = @EmailID)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
					INSERT INTO UserTable (
								FirstName , LastName , EmailId , Userpassword , LocalAddress , MobileAddress , Gender , DayAndTime
								) values 
								( 
										 @Firstname,  
										 @Lastname,  
										 @EmailID,  
										 @UserPassword,
										 @CurrentAddress,
										 @MobileNumber,
										 @Gender,
										 CURRENT_TIMESTAMP
								);
				COMMIT 
				set @status = 1
					select @status
			END
			else
			BEGIN
			BEGIN TRANSACTION
					INSERT INTO UserTable (
								FirstName , LastName , EmailId , Userpassword , LocalAddress , MobileAddress , Gender , DayAndTime
								) values 
								( 
										 @Firstname,  
										 @Lastname,  
										 @EmailID,  
										 @UserPassword,
										 @CurrentAddress,
										 @MobileNumber,
										 @Gender,
										 CURRENT_TIMESTAMP
								);
					ROLLBACK

				select @status
			END
	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;

    -- select statements for procedure here
    ---Select  EmpId
	---from UserTable
	---where FirstName = @Firstname ;
    

END
