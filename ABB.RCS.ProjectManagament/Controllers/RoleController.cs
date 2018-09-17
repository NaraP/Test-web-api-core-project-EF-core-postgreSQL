using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABB.RCS.ProjectManagament.UserRoleRepository;
using ABB.RCS.SystemManagament.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABB.RCS.ProjectManagament.Controllers
{
    [Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository userRepository;
        public RoleController(IRoleRepository repository)
        {
            userRepository = repository;
        }

        /// <summary>
        /// GetRoles this api method is used to get user roles from the database
        /// </summary>
        /// <returns></returns>
        // GET: api/Role
        [HttpGet]
        public IEnumerable<Role> GetRoles()
        {
            return userRepository.GetListRoles().ToList();
        }

        // POST: api/Role
        [HttpPost]
        public int Post([FromBody]Role role)
        {
            int SaveReturn = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaveReturn = userRepository.InsertRole(role);
                }
            }
            catch (Exception ex)
            {
            }
            return SaveReturn;
        }
        
        // PUT: api/Role/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]Role role)
        {
            int UpdateReturn = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateReturn = userRepository.UpdateRole(role);
                }
            }
            catch (Exception ex)
            {
            }
            return UpdateReturn;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int Delete(int id,Role role)
        {
            int DeleteReturn = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    DeleteReturn = userRepository.DeleteRole(role);
                }
            }
            catch (Exception ex)
            {
            }
            return DeleteReturn;
        }
    }
}
