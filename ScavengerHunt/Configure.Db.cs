using Microsoft.EntityFrameworkCore;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ScavengerHunt.Data;

[assembly: HostingStartup(typeof(ScavengerHunt.ConfigureDb))]

namespace ScavengerHunt;

public class ConfigureDb : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => {
            var connectionString = context.Configuration.GetConnectionString("SQLAZURECONNSTR_DefaultConnection")
                ?? "Data Source=XAVIER-ASUS;Initial Catalog=SHData;Integrated Security=True;Trust Server Certificate=True";
            
            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                connectionString, SqlServer2022Dialect.Provider));

            // $ dotnet ef migrations add CreateIdentitySchema
            // $ dotnet ef database update
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(nameof(ScavengerHunt))));
            
            // Enable built-in Database Admin UI at /admin-ui/database
            services.AddPlugin(new AdminDatabaseFeature());
        });
}