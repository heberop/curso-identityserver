using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Quickstart.UI;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace HostDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = @"server=(localdb)\mssqllocaldb;database=IdentityServer4.EntityFramework;trusted_connection=yes";
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                    .AddTemporarySigningCredential()
                    .AddTestUsers(TestUsers.Users)
                    .AddConfigurationStore(builder =>
                        builder.UseSqlServer(connectionString, options =>
                            options.MigrationsAssembly(migrationAssembly)))
                    .AddOperationalStore(builder =>
                        builder.UseSqlServer(connectionString, options =>
                            options.MigrationsAssembly(migrationAssembly)));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.IdentityResources.Any())
                {
                    foreach (var identityResource in Config.IdentityResources())
                    {
                        context.IdentityResources.Add(identityResource.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.ApiResources.Any())
                {
                    foreach (var apiResource in Config.ApiResources())
                    {
                        context.ApiResources.Add(apiResource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
