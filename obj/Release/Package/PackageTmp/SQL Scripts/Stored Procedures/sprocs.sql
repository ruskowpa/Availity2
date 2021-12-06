USE [Availity]
GO


DROP PROCEDURE [dbo].[GetAllBusinessUsers]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllBusinessUsers] 

AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.Id
      ,u.FirstName
      ,u.LastName
      ,u.NPINumber
      ,u.BusinessAddress
      ,u.Phone
      ,u.IsReviewed
      ,u.Email
      ,u.UserName
	  ,r.Name AS RoleName
  FROM 
	dbo.AspNetUsers u WITH(NOLOCK)
  LEFT JOIN dbo.AspNetUserRoles ur WITH(NOLOCK)
	ON u.Id = ur.UserId
  LEFT JOIN dbo.AspNetRoles r WITH(NOLOCK)
	ON ur.RoleId = r.Id;

END
GO


USE [Availity]
GO

DROP PROCEDURE [dbo].[GetBusinessUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetBusinessUser] 
	@UserID nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.Id
      ,u.FirstName
      ,u.LastName
      ,u.NPINumber
      ,u.BusinessAddress
      ,u.Phone
      ,u.IsReviewed
      ,u.Email
      ,u.UserName
	  ,r.Name AS RoleName
  FROM 
	dbo.AspNetUsers u WITH(NOLOCK)
  LEFT JOIN dbo.AspNetUserRoles ur WITH(NOLOCK)
	ON u.Id = ur.UserId
  LEFT JOIN dbo.AspNetRoles r WITH(NOLOCK)
	ON ur.RoleId = r.Id
  WHERE u.Id = @UserID;

END
GO



