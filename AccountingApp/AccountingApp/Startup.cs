using AccountingApp.Utils;
using Autofac;
using AutoMapper;
using BLL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AccountingApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public AuthentificationOptions AuthOptions { get; } = new AuthentificationOptions();
        public MapperConfiguration MapperConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Configuration
                .GetSection("Authentification")
                .Bind(AuthOptions);

            AuthOptions.GenerateKey();

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PLMappingProfile>();
                cfg.AddProfile<BLLMappingProfile>();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AccountingApp", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = AuthOptions.ValidateIssuer,
                        ValidateAudience = AuthOptions.ValidateAudience,
                        ValidateLifetime = AuthOptions.ValidateLifeTime,
                        ValidateIssuerSigningKey = AuthOptions.ValidateSigningKey,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidAudience = AuthOptions.Audience,
                        IssuerSigningKey = AuthOptions.SigningKey,
                    };
                });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connection = Configuration.GetConnectionString("Default");
            builder.RegisterModule(new PLDependencyInjection(connection));
            builder.Register(c => MapperConfiguration.CreateMapper())
                   .As<IMapper>()
                   .InstancePerDependency();

            builder.RegisterInstance(AuthOptions).SingleInstance();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccountingApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
