using ABB.RCS.SystemManagament.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABB.RCS.ProjectManagament.UserRoleRepository
{
    public interface IRoleRepository
    {
        int InsertRole(Role objRole);
        int UpdateRole(Role objRole);
        int DeleteRole(Role objRole);
        IEnumerable<Role> GetListRoles();
        List<Role> FindUser(int RoleId);
    }
}
