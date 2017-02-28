using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace JsOidc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}