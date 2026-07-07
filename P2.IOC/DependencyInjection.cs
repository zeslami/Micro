using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P2.Infrastructure.Clients;
using P2.Infrastructure.Contracts.Clients;

namespace P2.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["P1ApiSettings:BaseUrl"]
            ?? throw new InvalidOperationException("P1ApiSettings:BaseUrl is not configured in appsettings.json.");

        services.AddHttpContextAccessor();
        services.AddTransient<AuthForwardingHandler>();

        services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        }).AddHttpMessageHandler<AuthForwardingHandler>();

        services.AddHttpClient<IUserApiClient, UserApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        }).AddHttpMessageHandler<AuthForwardingHandler>();

        services.AddHttpClient<IProductGroupApiClient, ProductGroupApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        }).AddHttpMessageHandler<AuthForwardingHandler>();

        services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        }).AddHttpMessageHandler<AuthForwardingHandler>();

        return services;
    }
}
