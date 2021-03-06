USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spAddEmployeeData]    Script Date: 8/5/2020 9:19:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spAddEmployeeData] 
(
    @Firstname varchar(30),  
    @Lastname varchar(30),
    @EmailID varchar(50),
    @CurrentAddress varchar(150),
	@MobileNumber bigint ,
	@Gender varchar(6)
)
AS
BEGIN
   DECLARE @status int
	set @status=0

	BEGIN TRY

		IF NOT EXISTS (SELECT * FROM EmployeeTable WHERE [EmailId] = @EmailID)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
				INSERT INTO EmployeeTable (
							FirstName , LastName , EmailId , CurrentAddress , MobileNumber , Gender , CreatedDate , ModificationDate
							) values 
							( 
									 @Firstname,  
									 @Lastname,  
									 @EmailID,  
									 @CurrentAddress,
									 @MobileNumber,
									 @Gender,
									 CURRENT_TIMESTAMP,
									 CURRENT_TIMESTAMP
							);
			COMMIT 
			set @status = 1
				select @status
		
		END
		else
		Begin
				BEGIN TRANSACTION
						INSERT INTO EmployeeTable (
							FirstName , LastName , EmailId , CurrentAddress , MobileNumber , Gender , CreatedDate , ModificationDate
							) values 
							( 
									 @Firstname,  
									 @Lastname,  
									 @EmailID,  
									 @CurrentAddress,
									 @MobileNumber,
									 @Gender,
									 CURRENT_TIMESTAMP,
									 CURRENT_TIMESTAMP
							);
				ROLLBACK

				select @status

		END
	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;      
END
