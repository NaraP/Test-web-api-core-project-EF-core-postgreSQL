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
    public class UserRepository : IUserRepository
    {
        IErrorLogger ErrorLog = new ErrorLogger();

        string connectionString = GetConfigFileData.GetConfigurationData();

        //public UserRepository(IErrorLogger ErrorLogs)
        //{
        //    ErrorLog = ErrorLogs;
        //}

        /// <summary>
        /// GetListUsers this method is used to get the users data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetListUsers()
        {
            List<User> LstUser = new List<User>();

            User user = null;
            NpgsqlDataReader rdr;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT userid, username, password, email, createddate, lastlogindate FROM public.users;", con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                     rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user = new User();
                        user.UserId = (int)rdr["userid"];
                        user.Username = rdr["username"].ToString();
                        user.Email = rdr["email"].ToString();
                        //user.CreatedDate = (DateTime)rdr["createddate"].ToString());
                        LstUser.Add(user);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "GetListUsers", "User Module");
            }
            finally
            {
                user = null;
                rdr = null;
            }
            return LstUser;
        }

        /// <summary>
        /// InsertUser thismethod is used to save the users data
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public int InsertUser(User objUser)
        {
            int SavaUsers = 0;
            string SaveUser = string.Empty;

            try
            {
                SaveUser = "INSERT INTO public.users(username, password, email,createddate) VALUES('" + objUser.Username + "','" + objUser.Password + "',,'" + objUser.Email + "',,'" + objUser.CreatedDate + "');";

                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(SaveUser, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SavaUsers = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "InsertUser", "User Module");
            }
            finally
            {
            }
            return SavaUsers;
        }

        /// <summary>
        /// UpdateUser this method is used to update the user intormation
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public int UpdateUser(User objUser)
        {
            string UpdateUserData = string.Empty;
            int UserUpdata = 0;

            try
            {
                UpdateUserData = "UPDATE public.user SET username='" + objUser.Username + "', password='" + objUser.Password + "', email='" + objUser.Email + "', createddate='" + objUser.CreatedDate + "' WHERE userid='" + objUser.UserId + "'";

                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(UpdateUserData, con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    UserUpdata = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ExceptionHandler(ex, "UpdateUser", "User Module");
            }
            finally
            {
            }
            return UserUpdata;
        }

        /// <summary>
        /// DeleteUser this method is used to delete the users
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public int DeleteUser(User objUser)
        {
            int UserDelete = 0;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM public.users WHERE userid='" + objUser.UserId + "'; ", con);
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
                ErrorLog.ExceptionHandler(ex, "DeleteCustomerData", "Cusomer Module");
            }
            finally
            {
            }
            return UserDelete;
        }

        /// <summary>
        /// FindUser this method is used to find the users
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<User> FindUser(int UserId)
        {
            List<User> LstRoles = new List<User>();

            User user = null;
            NpgsqlDataReader rdr;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT UserId, Username,Email FROM public.users where UserId='" + UserId + "';", con);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user = new User();
                        user.UserId = (int)rdr["UserId"];
                        user.Username = rdr["Username"].ToString();
                        user.Email = rdr["Email"].ToString();

                        LstRoles.Add(user);
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
                user = null;
                rdr = null;
            }
            return LstRoles;
        }
    }
}
