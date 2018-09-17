using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABB.RCS.SystemManagament
{
    public class GetConfigFileData
    {
        public static IConfigurationRoot Configuration;

        public static String GetConfigurationData()
        {
            String SqlConn = string.Empty;

            try
            {
                var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");

                Configuration = builder.Build();

                SqlConn = ($"{Configuration["PostGreSqlConn:PostGreSqlConnection"]}");
            }
            catch (Exception ex)
            {
            }

            return SqlConn; ;
        }
    }
}
