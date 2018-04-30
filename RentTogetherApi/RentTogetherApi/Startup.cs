using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentTogetherApi.Business.Services;
using RentTogetherApi.Common.Helpers;
using RentTogetherApi.Common.Mapper;
using RentTogetherApi.Dal;
using RentTogetherApi.Entities;
using RentTogetherApi.Interfaces.Business;
using RentTogetherApi.Interfaces.Dal;
using RentTogetherApi.Interfaces.Helpers;
using Swashbuckle.AspNetCore.Swagger;

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
            //EF
            services.AddDbContext<RentTogetherDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RentTogetherBdd")));

            //ssl
            //services.Configure<MvcOptions>(options => options.Filters.Add(new RequireHttpsAttribute();

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            //Dependency Injection
            services.AddTransient<IDal, SqlService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMapperHelper, Mapper>();
            services.AddTransient<ICustomEncoder, CustomEncoder>();
            services.AddTransient<IMessageService, MessageService>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            //JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecretKey"]))
                };
            });

            services.BuildServiceProvider().GetService<RentTogetherDbContext>().Database.Migrate();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            //ssl
            //var options = new RewriteOptions().AddRedirectToHttps();
            //app.UseRewriter(options);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentTogether API V1");
            });

              
            
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, DELETE, PUT");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
                await next();
            });

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
