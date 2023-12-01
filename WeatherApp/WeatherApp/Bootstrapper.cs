using System.Net.Http;
using Splat;
using WeatherApp.Services;

namespace WeatherApp;

public static class Bootstrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.Register(() => new HttpClient());
    }
}