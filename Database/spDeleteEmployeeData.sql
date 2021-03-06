USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteEmployeeData]    Script Date: 8/5/2020 9:28:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spDeleteEmployeeData]
(
@Empid int
)
AS
BEGIN
DECLARE @status int
set @status=0
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;
	BEGIN TRY

    -- Insert statements for procedure here
			if EXISTS(select * from EmployeeTable where EmpId = @Empid)
			begin
				BEGIN TRANSACTION

						UPDATE EmployeeTable
						SET PresentState = 0	
						WHERE EmpId = @Empid;
				
				COMMIT 
						set @status = 1
						select @status
			end
			else
			begin
				BEGIN TRANSACTION

						UPDATE EmployeeTable
						SET PresentState = 0	
						WHERE EmpId = @Empid;
				
				Rollback
						select @status
			end
	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;

END
