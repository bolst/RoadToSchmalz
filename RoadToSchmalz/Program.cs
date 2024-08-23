using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

namespace RoadToSchmalz
{
    public class Program
    {
        public static async Task Main( string[] args )
        {
            var builder = WebAssemblyHostBuilder.CreateDefault( args );
            builder.RootComponents.Add<App>( "#app" );

            builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) } );

            AddBlazorise( builder.Services );

            await builder.Build().RunAsync();
        }

        public static void AddBlazorise( IServiceCollection services )
        {
            services
                .AddBlazorise();
            services
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();
        }
    }
}