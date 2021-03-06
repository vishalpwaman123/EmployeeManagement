USE [EmployeeDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spGetAllUserData]    Script Date: 8/5/2020 9:29:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spGetAllUserData]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;
	BEGIN TRY
    -- Insert statements for procedure here
			SELECT EmpId ,
				   FirstName ,
				   LastName ,
				   EmailId ,
				   LocalAddress ,
				   MobileAddress ,
				   Gender ,
				   DayAndTime
			FROM UserTable
	END TRY
	BEGIN CATCH  
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;
END
