//
//Author : Déprez Rémi
//Version : 1.0
//

using System.Linq;
using System.Text;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using RentTogether.Api.Hubs;
using RentTogether.Business.Services;
using RentTogether.Common.Helpers;
using RentTogether.Common.Mapper;
using RentTogether.Dal;
using RentTogether.Entities;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace RentTogether
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

            //ODATA
			services.AddOData();
			services.AddTransient<RentTogetherModelBuilder>();

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

            //SignalR
            services.AddSignalR();

            //Dependency Injection
            services.AddTransient<IDal, SqlService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMapperHelper, Mapper>();
            services.AddTransient<ICustomEncoder, CustomEncoder>();
            services.AddTransient<IMessageService, MessageService>();
			services.AddTransient<IConversationService, ConversationService>();
			services.AddTransient<IParticipantService, ParticipantService>();
			services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IPersonalityService, PersonalityService>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<ITargetLocationService, TargetLocationService>();
            services.AddTransient<ISearchLocationService, SearchLocationService>();
            services.AddTransient<IBuildingService, BuildingService>();

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

            services.AddMvc();

			services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, RentTogetherModelBuilder rentTogetherModelBuilder,
                              ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

			app.UseStaticFiles();

            //Logger
            loggerFactory.AddFile("Logs/renttogetherapi.txt");

            //SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<PublicChatHub>("/chat");
            });

            //ssl
            //var options = new RewriteOptions().AddRedirectToHttps();
            //app.UseRewriter(options);

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            
            //Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
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


            app.UseAuthentication();

			//ODATA
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                routeBuilder.EnableDependencyInjection();
                routeBuilder.MapODataServiceRoute("ODataRoutes", "api", rentTogetherModelBuilder.GetEdmModel(app.ApplicationServices));
            });
        }
    }
}
