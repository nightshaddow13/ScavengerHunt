using ServiceStack.Auth;
using ScavengerHuntApi.Data;

[assembly: HostingStartup(typeof(ScavengerHuntApi.ConfigureAuth))]

namespace ScavengerHuntApi;

public class ConfigureAuth : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddPlugin(new AuthFeature(IdentityAuth.For<ApplicationUser>(options => {
                options.SessionFactory = () => new CustomUserSession();
                options.CredentialsAuth();
                options.AdminUsersFeature();
            })));
        });
}