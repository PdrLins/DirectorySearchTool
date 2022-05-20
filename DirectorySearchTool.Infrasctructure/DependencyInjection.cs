using DirectorySearchTool.Infrasctructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace DirectorySearchTool.Infrasctructure
{
    public static class DependencyInjection
    {
        public static void RegisterInfraDI(this IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("DirectorySearchTool"));
            services.AddScoped<IApiContext, ApiContext>();
        }
    }
}
