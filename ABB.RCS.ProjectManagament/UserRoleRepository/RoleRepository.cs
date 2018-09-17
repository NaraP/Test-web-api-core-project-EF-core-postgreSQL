using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ABB.RCS.ProjectManagament.ErrorLogs;
using ABB.RCS.SystemManagament;
using ABB.RCS.SystemManagament.Entities;
using Npgsql;

namespace ABB.RCS.ProjectManagament.UserRoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        IErrorLogger ErrorLog = new ErrorLogger();

        string connectionString = GetConfigFileData.GetConfigurationData();

        /// <summary>
        /// GetListRoles this method is used to fetch the list of roles from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> GetListRoles()
        {
            List<Role> LstRoles = new List<Role>();

            Role role = null;
            NpgsqlDataReader rdr;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT roleid, rolename FROM public.roles;", con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        role = new Role();
                        role.RoleId = (int)rdr["roleid"];
                        role.RoleName = rdr["rolename"].ToString();
                        LstRoles.Add(role);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "GetListRoles", "Role Module");
            }
            finally
            {
                role = null;
                rdr = null;
            }
            return LstRoles;
        }

        /// <summary>
        /// InsertRole thi smethod is used to save the roles into database
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public int InsertRole(Role objRole)
        {
            int SavaRoles = 0;
            string SaveRole = string.Empty;

            try
            {
                SaveRole = "INSERT INTO public.roles(rolename) VALUES('" + objRole.RoleName + "');";

                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(SaveRole, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SavaRoles = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "InsertRole", "Role Module");
            }
            finally
            {
            }
            return SavaRoles;
        }

        /// <summary>
        /// UpdateRole this method is used to update the role names
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public int UpdateRole(Role objRole)
        {
            string UpdateRoleData = string.Empty;
            int UserUpdata = 0;

            try
            {
                UpdateRoleData = "UPDATE public.roles SET rolename='" + objRole.RoleName + "' WHERE userid='" + objRole.RoleId + "'";

                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(UpdateRoleData, con);
                    con.Open();
                    UserUpdata = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "UpdateRole", "Role Module");
            }
            finally
            {
            }
            return UserUpdata;
        }

        /// <summary>
        /// DeleteRole this method is used to delete role from the datbase
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public int DeleteRole(Role objRole)
        {
            int UserDelete = 0;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM public.roles WHERE roleid='" + objRole.RoleId + "'; ", con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    UserDelete = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "DeleteRole", "Role Module");
            }
            finally
            {
            }
            return UserDelete;
        }

        /// <summary>
        /// FindUser this method is used to find the roles in the datbase
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<Role> FindUser(int RoleId)
        {
            List<Role> LstRoles = new List<Role>();

            Role role = null;
            NpgsqlDataReader rdr;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT roleid, rolename FROM public.roles where roleid='" + RoleId + "';", con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        role = new Role();
                        role.RoleId = (int)rdr["roleid"];
                        role.RoleName = rdr["rolename"].ToString();
                        LstRoles.Add(role);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "FindUser", "Role Module");
            }
            finally
            {
                role = null;
                rdr = null;
            }
            return LstRoles;
        }
    }
}
