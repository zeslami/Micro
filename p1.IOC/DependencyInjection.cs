using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P1.Application.Contracts.Services;
using P1.Application.Services;
using P1.Infrastructure.Contracts.Persistence;
using P1.Infrastructure.Contracts.Persistence.Command;
using P1.Infrastructure.Contracts.Persistence.Query;
using P1.Infrastructure.Persistence;
using P1.Infrastructure.Persistence.Repositories;

namespace P1.IoC;

public static class DependencyContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services , IConfiguration configuration)
    {
        return services;
    }
    public static void RegisterServices(IServiceCollection services)
    {
        // تراکنش‌ها و پایگاه داده
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // ثبت سرویس‌های دامنه کسب‌وکار (Domain Services)
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductGroupService, ProductGroupService>();
        services.AddScoped<IUserService, UserService>();

        // ثبت ریپازیتوری‌های گروه محصولات
        services.AddScoped<IProductGroupQueryRepository, ProductGroupRepository>();
        services.AddScoped<IProductGroupCommandRepository, ProductGroupRepository>();

        // ثبت ریپازیتوری‌های محصولات
        services.AddScoped<IProductQueryRepository, ProductRepository>();
        services.AddScoped<IProductCommandRepository, ProductRepository>();

        // ثبت ریپازیتوری‌های کاربران
        services.AddScoped<IUserQueryRepository, UserRepository>();
        services.AddScoped<IUserCommandRepository, UserRepository>();
    }
   
}
