using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReckonMe.Api.Configs;
using ReckonMe.Api.Cryptography.Abstract;
using ReckonMe.Api.Cryptography.Concrete;
using ReckonMe.Api.Extensions;
using ReckonMe.Api.Managers.Abstract;
using ReckonMe.Api.Managers.Concrete;
using ReckonMe.Api.Options;
using ReckonMe.Api.Repositories.Abstract;
using ReckonMe.Api.Repositories.Concrete;
using ReckonMe.Api.Services.Abstract;
using ReckonMe.Api.Services.Concrete;

namespace ReckonMe.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddCors();

            services.AddSingleton<IMapper>(AutoMapperConfig.Initialize());
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.Configure<JwtIssuerOptions>(Configuration.GetSection(nameof(JwtIssuerOptions)));

            var mongoSettings = Configuration.GetSection("MongoDb");
            services.AddMongo(mongoSettings["ConnectionString"], mongoSettings["Database"]);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IIdentityService identityService)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());
            app.UseJwtBearerAuthentication(CreateJwtBearerOptions(identityService));
            app.UseStatusCodePages();
            app.UseMvc();

            MongoConfig.Initialize();
        }

        private static JwtBearerOptions CreateJwtBearerOptions(IIdentityService identityService)
        {
            return new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = identityService.GenerateTokenValidationParameters()
            };
        }
    }
}
