using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Quickstart.UI;

namespace WebApplication4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //se o certificado for um arquivo, basta informá-lo com sua senha e passar como parametro em AddSigningCredential
            var cert = new X509Certificate2("idsrv4test.pfx", "idsrv3test");

            services.AddIdentityServer()
                    .AddSigningCredential(cert)
                    .AddTemporarySigningCredential()
                    .AddInMemoryClients(Config.Clients())
                    .AddInMemoryIdentityResources(Config.IdentityResources())
                    .AddInMemoryApiResources(Config.ApiResources())
                    .AddTestUsers(TestUsers.Users);

            //se o certificado estiver instalado na máquina (recomendado), use as informações dele para abri-lo
            //services.AddIdentityServer()
            //    .AddSigningCredential(name: "mycert", location: StoreLocation.LocalMachine, nameType: NameType.SubjectDistinguishedName)
            //    .AddTemporarySigningCredential()
            //    .AddInMemoryClients(Config.Clients())
            //    .AddInMemoryIdentityResources(Config.IdentityResources())
            //    .AddInMemoryApiResources(Config.ApiResources())
            //    .AddTestUsers(TestUsers.Users);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
