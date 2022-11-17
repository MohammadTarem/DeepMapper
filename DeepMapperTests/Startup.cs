using DeepMapper.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DeepMapperTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDeepMapping(c => 
            { 
                c.UseConventionalMapping();
            });
        }
    }
}
