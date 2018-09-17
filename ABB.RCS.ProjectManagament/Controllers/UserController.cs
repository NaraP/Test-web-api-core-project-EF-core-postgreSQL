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
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository repository)
        {
            userRepository = repository;
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return userRepository.GetListUsers().ToList();
        }

        // POST: api/User
        [HttpPost]
        public int Post([FromBody]User user)
        {
            int SaveReturn = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaveReturn = userRepository.InsertUser(user);
                }
            }
            catch (Exception ex)
            {
            }
            return SaveReturn;
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]User user)
        {
            int UpdateReturn = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateReturn= userRepository.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
            }
            return UpdateReturn;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int Delete(int id,User user)
        {
            int DeleteReturn = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    DeleteReturn = userRepository.DeleteUser(user);
                }
            }
            catch (Exception ex)
            {
            }
            return DeleteReturn;
        }
    }
}
