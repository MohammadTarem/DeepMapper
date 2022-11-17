using Microsoft.Extensions.DependencyInjection;


namespace DeepMapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IDeepMapper? deepMapper = null;
        public static IDeepMapper? DeepMapper => deepMapper;
        public static void AddDeepMapping(this IServiceCollection services,Action<MappingConfigurations> action)
        {
            
            
            var configuration = new MappingConfigurations();
            action(configuration);
            
            services.AddSingleton<IConfigurationalMapper>(_ = new ConfigurationalMapper());
            services.AddSingleton<IConventionalMapper>(_ = new ConventionalMapper());
            services.BuildServiceProvider(); ;
            services.AddSingleton<IDeepMapper>(
                service =>
                {
                    var cfgMapper = service.GetService<IConfigurationalMapper>();
                    var cnvMapper = service.GetService<IConventionalMapper>();
                    deepMapper = new Mapper(configuration, cnvMapper!, cfgMapper!);

                    return deepMapper!;
                });    
        }
    }
}
