using DeepMapper;
using Microsoft.Extensions.DependencyInjection;
using SimpleMapperTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapperTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDeepMapping(c => 
            { 
                c.UseConfigurationalMapping();
            });
        }
    }
}
