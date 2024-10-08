using ServiceStack;
using ScavengerHunt.ServiceModel;

namespace ScavengerHunt.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}