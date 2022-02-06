using Microsoft.Extensions.DependencyInjection;

namespace Bank.Core.Utilities
{
    public static class MapperRegistirationExtensions
    {
        public static void AddApplicationMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}