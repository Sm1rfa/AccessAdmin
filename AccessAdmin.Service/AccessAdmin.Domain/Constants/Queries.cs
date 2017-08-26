namespace AccessAdmin.Domain.Constants
{
    public static class Queries
    {
        public const string ConnectionString = "Data Source=DESKTOP-UFAODH5;Initial Catalog=RequestBase;Integrated Security = True";

        #region System queries

        public const string GetAllSystemsQuery = "select * from Systems where IsDeleted = 0";
        public const string GetSingleSystemQuery = "select * from Systems where SystemId = @id and IsDeleted = 0";
        public const string CreateSystemQuery = "insert into Systems(SystemName, IsDeleted) values (@name, 0)";
        public const string UpdateSystemQuery = "update Systems set SystemName = @name where SystemId = @id";
        public const string DeleteSystemQuery = "update Systems set IsDeleted = 1 where SystemId = @id";

        #endregion

        #region Role queries

        public const string GetAllRoles = "select * from Roles";

        public const string GetAllRolesPerSystemQuery = @"select r.RoleId
	                                ,r.RoleName
                                    ,r.IsDeleted
                                from Roles r
                                left join SystemRoles sr on r.RoleId = sr.RoleId
                                where sr.SystemId = @id";

        public const string GetSingleRolePerSystemQuery = @"select r.RoleId
	                                  ,r.RoleName
                                      ,r.IsDeleted
                                from Roles r
                                left join SystemRoles sr on r.RoleId = sr.RoleId
                                where sr.SystemId = @sysId and r.RoleId = @roleId";

        public const string CreateRoleQuery = "insert into Roles(RoleName, IsDeleted) values(@name, 0)";
        public const string AssignRoleToSystemQuery = "insert into SystemRoles(SystemId, RoleId) values(@sysId, @roleId)";
        public const string UpdateRolePerSystemQuery = "update SystemRoles set RoleId = @roleId2 where SystemId = @sysId and RoleId = @roleId";
        public const string DeleteRolePerSystemQuery = "delete from SystemRoles where SystemId = @id and RoleId = @roleId";

        #endregion

        // I know it is very basic, but the purpouse of the tasks doesn't include encryption and login at all.
        // We can discouss more on the interview
        public const string AuthenticateUserQuery = @"select count(*)
                                    from Users
                                    where Email = @mail
                                    and UserPass = @pass";

        public const string IsUserAdminQuery = "select u.IsAdmin from Users u where u.Email = @mail";
    }
}
