using ServiceStack;
using ScavengerHuntApi.ServiceModel;

namespace ScavengerHuntApi.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}