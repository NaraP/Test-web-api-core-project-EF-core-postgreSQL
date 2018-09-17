using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.RCS.ProjectManagament.ErrorLogs
{
    public class ErrorLogger : IErrorLogger
    {
        public void ExceptionHandler(Exception ex, string MethodName, string ModuleName)
        {
            try
            {
                if (ex != null && ex.Message!=null)
                {
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.Append("The following Error occured at ");
                    sbMessage.Append(DateTime.Now).Append("in ").Append(MethodName).Append("in ").Append(ModuleName);

                    //System.Web.HttpContext.Current.Session["ErrorMessage"] = sbMessage.ToString();

                    if (ex.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        sbMessage.Append(" - While calling Data access layer.");
                    }
                    else if (ex.GetType() == typeof(System.ArgumentException))
                    {
                        sbMessage.Append(" - There is some wrong Argument passed to the methods.");
                    }
                    else if (ex.GetType() == typeof(System.IO.FileNotFoundException))
                    {
                        sbMessage.Append(" - File not found in the specified directory. ");
                    }
                    else if (ex.GetType() == typeof(System.IO.DirectoryNotFoundException))
                    {
                        sbMessage.Append(" - Directory not found.");
                    }
                    else if (ex.GetType() == typeof(System.NullReferenceException))
                    {
                        sbMessage.Append(" - The method or variable has the Null value.");
                    }
                    else
                    {
                        sbMessage.Append(" - The general exception occured.");
                    }

                    sbMessage.Append(Environment.NewLine);
                    sbMessage.Append("System Error :").Append(Environment.NewLine).Append(ex.Message);
                    sbMessage.Append(Environment.NewLine);
                    sbMessage.Append("Stack Trace Details:--").Append(Environment.NewLine);
                    sbMessage.Append(ex.StackTrace).Append(Environment.NewLine);

                    if (ex.InnerException != null)
                    {
                        sbMessage.Append("Inner Exception:").Append(Environment.NewLine);
                        sbMessage.Append(ex.InnerException.Message);
                    }
                    //Logging error in to the physical file
                    LogErrorIntoFile(sbMessage.ToString());
                }
            }
            catch (Exception exc)
            {
            }
        }

        public void ExceptionWriteIntoTextFile(Exception ex, string MethodName, string StrLayer, string ModuleName)
        {
            try
            {
                if (ex != null && ex.Message!=null)
                {
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.Append("The following Error occured at ");
                    sbMessage.Append(DateTime.Now).Append("in ").Append(MethodName).Append("in ").Append(ModuleName); ;

                    if (ex.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        sbMessage.Append(" - While calling Data access layer.");
                    }
                    else
                    {
                        sbMessage.Append(" - The general exception occured.");
                    }

                    sbMessage.Append("\nSystem Error :\n").Append(ex.Message);
                    sbMessage.Append("\nStack Trace Details:--\n");
                    sbMessage.Append(ex.StackTrace);

                    if (ex.InnerException != null)
                    {
                        sbMessage.Append("\nInner Exception:\n");
                        sbMessage.Append(ex.InnerException.Message);
                    }

                    sbMessage.Append("Query:\n");
                    sbMessage.Append(StrLayer);

                    //sbMessage.Append("\n\n User: " + System.Web.HttpContext.Current.Session["EMPNO"]);
                    //sbMessage.Append("\n Page: " + System.Web.HttpContext.Current.Request.ServerVariables["URL"]);

                    //Logging error in to the physical file
                    LogErrorIntoFile(sbMessage.ToString());

                    //SendMail(sbMessage.ToString());

                    //System.Web.HttpContext.Current.Response.Redirect("ErrorPage.aspx");
                }
            }
            catch (Exception excep)
            {
            }
        }

        private void LogErrorIntoFile(string errorMessage)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string filePath = "";// System.Configuration.ConfigurationSettings.AppSettings["ErrorFilePath"];

            DateTime dt = DateTime.Now;
            string date = null;// dt.ToString("yyyy/MM/dd").Replace("/", string.Empty);
            filePath = filePath + "_" + date + ".txt";
            try
            {
                using (fs = File.Open(filePath,
                            FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (sw = new StreamWriter(fs))
                    {
                        sw.Write(errorMessage);
                        sw.Write(Environment.NewLine);
                        sw.Write("==========================================================================================================");
                        sw.Write(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                fs.Close();
                fs.Dispose();
                fs = null;
                sw.Dispose();
                sw = null;
            }
        }
    }
}
