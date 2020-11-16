using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using HeladacWeb.Data;
using HeladacWeb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using HeladacWeb.Services;

namespace HeladacWeb
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
            bool isDevEnvironment = true;
            string appName = "";
            string blobUri = "";
            string helmUserKeyvaultUri = "";
            if (isDevEnvironment)
            {
                appName = "heladackid";
                blobUri = "https://heladackid.blob.core.windows.net/";
                helmUserKeyvaultUri = "https://heladackid.vault.azure.net/keys/helmuser/d94a50057451408bb0b0880c5d436f96";
                services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"c:\dataprotection-persistkeys"))
                .AddKeyManagementOptions(options =>
                {
                    options.NewKeyLifetime = TimeSpan.FromTicks(long.MaxValue);
                    options.AutoGenerateKeys = true;
                });
                services
                    .AddDefaultIdentity<HeladacUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<HeladacDbContext>();
            }
            else
            {
                appName = "heladac";
                blobUri = "https://heladac.blob.core.windows.net/";
                helmUserKeyvaultUri = "https://heladac.vault.azure.net/keys/helmuser/d94a50057451408bb0b0880c5d436f96";
                services
                    .AddDefaultIdentity<HeladacUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<HeladacDbContext>();
            }
            
            var dataProtectionProvider = DataProtectionProvider.Create(appName);
            EncryptionService.initialProvider(dataProtectionProvider);



            services.AddDbContext<HeladacDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            

            services.AddIdentityServer()
                .AddApiAuthorization<HeladacUser, HeladacDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            //#region Allow-Orgin
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});
            //#endregion

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        //public static IServiceCollection AddCustomDataProtection(this IServiceCollection serviceCollection)
        //{
        //    bool isDevEnvironment = true;
        //    string appName = "";
        //    string blobUri = "";
        //    string helmUserKeyvaultUri = "";
        //    if (isDevEnvironment)
        //    {
        //        appName = "heladackid";
        //    }
        //    else
        //    {
        //        appName = "heladac";
        //    }

        //    var store = new X509Store(StoreLocation.CurrentUser);
        //    store.Open(OpenFlags.ReadOnly);
        //    var cert = store.Certificates.Find(X509FindType.FindByThumbprint, config["CertificateThumbprint"], false);

        //    var builder = serviceCollection
        //    .AddDataProtection()
        //    .SetApplicationName(appName);
        //    #if DEBUG
        //                builder
        //                    .PersistKeysToFileSystem(new DirectoryInfo(@"c:\dataprotection-persistkeys"))
        //                    .AddKeyManagementOptions(options =>
        //                    {
        //                        options.NewKeyLifetime = new TimeSpan(365, 0, 0, 0);
        //                        options.AutoGenerateKeys = true;
        //                    });
        //    #else
        //        serviceCollection
        //            .AddOptions<KeyManagementOptions>()
        //            .Configure<IConfiguration>((options, configuration) =>
        //            {
        //                configuration.GetSection("KeyManagement").Bind(options);
        //            })
        //            .Configure<MyAppOptions>((options, myAppOptions) => 
        //            {
        //                options.XmlRepository = new Microsoft.AspNetCore.DataProtection.StackExchangeRedis.RedisXmlRepository(
        //                    () => ConnectionMultiplexer.Connect(myAppOptions.RedisConfigurationString).GetDatabase(),
        //                    myAppOptions.DataProtectionRedisKeyForPersistKeys);
        //            });
        //        //.PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect("<URI>"), "DataProtection-Keys");
        //    #endif
        //    return serviceCollection;
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
