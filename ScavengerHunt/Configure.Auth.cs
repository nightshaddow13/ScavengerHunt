using ServiceStack.Auth;
using ScavengerHunt.Data;

[assembly: HostingStartup(typeof(ScavengerHunt.ConfigureAuth))]

namespace ScavengerHunt;

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