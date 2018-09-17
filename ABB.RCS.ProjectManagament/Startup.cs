using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ABB.RCS.ProjectManagament.ErrorLogs;
using ABB.RCS.ProjectManagament.UserRoleRepository;
using ABB.RCS.SystemManagament;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace ABB.RCS.ProjectManagament
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = GetConfigFileData.GetConfigurationData();

            ////services.AddDbContext<ABBRCSContext>(options =>
         
            services.Configure<GetConfigFileData>(Configuration.GetSection("GetConfigFileData"));

            services.Add(new ServiceDescriptor(typeof(IErrorLogger), new ErrorLogger()));    // singleton
            services.Add(new ServiceDescriptor(typeof(IUserRepository), new UserRepository()));    // singleton
            services.Add(new ServiceDescriptor(typeof(IRoleRepository), new RoleRepository()));    // singleton

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            loggerFactory.AddDebug();
            loggerFactory.AddConsole();
            loggerFactory.AddEventSourceLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
