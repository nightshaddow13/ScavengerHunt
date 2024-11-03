using ServiceStack;

[assembly: HostingStartup(typeof(ScavengerHuntApi.ConfigureServerEvents))]

namespace ScavengerHuntApi;

public class ConfigureServerEvents : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddPlugin(new ServerEventsFeature());
        });
}
