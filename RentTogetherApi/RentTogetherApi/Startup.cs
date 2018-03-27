using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RentTogetherApi.Business.Services;
using RentTogetherApi.Common.Mapper;
using RentTogetherApi.Dal;
using RentTogetherApi.Entities;
using RentTogetherApi.Interfaces.Business;
using RentTogetherApi.Interfaces.Dal;
using RentTogetherApi.Interfaces.Helpers;

namespace RentTogetherApi
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
            services.AddDbContext<RentTogetherDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RentTogetherBdd")));

            services.AddTransient<IDal, SqlService>();
            services.AddTransient<IAuthentificationService, AuthentificationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMapperHelper, Mapper>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
