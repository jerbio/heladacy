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
using Microsoft.AspNetCore.DataProtection;
using Azure.Storage.Blobs;
using Azure.Identity;

namespace HeladacWeb
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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

                string connectionString = "<connection_string>";
                string containerName = "my-key-container";
                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                //services
                //    .AddDataProtection()
                //    .PersistKeysToAzureBlobStorage(container, "keys.xml")
                //    .ProtectKeysWithAzureKeyVault(new Uri(helmUserKeyvaultUri), new DefaultAzureCredential());
                services
                    .AddDefaultIdentity<HeladacUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<HeladacDbContext>();
            }

            var dataProtectionProvider = DataProtectionProvider.Create(appName);
            EncryptionService.initialProvider(dataProtectionProvider);



            services.AddDbContext<HeladacDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(Configuration["ConnectionName"])
                    ));




            services.AddIdentityServer()
                .AddApiAuthorization<HeladacUser, HeladacDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                //app.UseCors(builder => {
                //    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                //});
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseCors("MyPolicy");
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
