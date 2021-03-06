USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateEmployeeData]    Script Date: 8/5/2020 9:30:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spUpdateEmployeeData]
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
		IF EXISTS (SELECT * FROM EmployeeTable WHERE [EmailId] = @EmailID AND PresentState = 1)
		BEGIN
			BEGIN TRANSACTION
    -- Insert statements for procedure here
					UPDATE  EmployeeTable
					SET			
							FirstName = @Firstname,
							LastName = @Lastname ,
							CurrentAddress = @CurrentAddress,
							MobileNumber = @MobileNumber ,
							Gender = @Gender ,
							ModificationDate = CURRENT_TIMESTAMP
					WHERE   EmailId = @EmailID;
			COMMIT 
			set @status = 1
				select @status
		END
		else
		Begin
				BEGIN TRANSACTION
						UPDATE  EmployeeTable
						SET			
							FirstName = @Firstname,
							LastName = @Lastname ,
							CurrentAddress = @CurrentAddress,
							MobileNumber = @MobileNumber ,
							Gender = @Gender ,
							ModificationDate = CURRENT_TIMESTAMP
						WHERE   EmailId = @EmailID;
				ROLLBACK

				select @status
		END
	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;

END
