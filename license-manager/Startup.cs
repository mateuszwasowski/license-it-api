using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using licensemanager.Classes;
using licensemanager.Repositories;
using licensemanager.Repositories.Interfaces;
using licensemanager.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using MySQL.Data.Entity.Extensions;

namespace licensemanager
{
    public class Startup
    {
        private static readonly string secretKey = "SuperSecretSafetyKey!";

        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder =
                new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

            var configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<DataBaseContext>(options => options.UseMySQL(connectionString));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCors("AllowAnyOrigin");

            ConfigureAuth(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    "spa-fallback",
                    new {controller = "Home", action = "Index"});
            });

#if DEBUG
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>();

                DbInitializer.Initialize(context);
            }
#endif
        }

        private static void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = "Audience",
                Issuer = "Issuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = "Issuer",

                ValidateAudience = true,
                ValidAudience = "Audience",

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "token",
                LoginPath = "/login",
                TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters),
                Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async (context) => { context.HttpContext.Response.Redirect("/login"); }
                }
            });
        }

        private static Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            IUserRepository userRepo = new UserRepository(new DataBaseContext());
            var user = userRepo.GetActiveUserByEmailWithPass(email);

            if (user == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if (CryptoClass.CheckPassword(password, user.Password))
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), new Claim[] { }));

            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
