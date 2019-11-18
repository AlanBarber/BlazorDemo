using BlazorDemo.Client.Services;
using BlazorDemo.Client.Services.Authentication;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorDemo.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Browser Local Storage Interface
            // NuGet "Cloudcrate.AspNetCore.Blazor.Browser.Storage" https://github.com/cloudcrate/BlazorStorage
            services.AddStorage();
            // Global App State
            services.AddSingleton<AppState>();
            // Custom authentication provider for client side
            services.AddScoped<AuthenticationStateProvider, BlazorClientSideAuthenticationStateProvider>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
