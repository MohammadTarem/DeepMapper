using Microsoft.Extensions.DependencyInjection;


namespace DeepMapper
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDeepMapping(this IServiceCollection services,Action<MappingConfigurations> action)
        {
            
            
            var configuration = new MappingConfigurations();
            action(configuration);
            
            services.AddSingleton<IConfigurationalMapper>(_ = new ConfigurationalMapper());
            services.AddSingleton<IConventionalMapper>(_ = new ConventionalMapper());

            services.AddSingleton<IDeepMapper, Mapper>(
                service =>
                {
                    var cfgMapper = service.GetService<IConfigurationalMapper>();
                    var cnvMapper = service.GetService<IConventionalMapper>();

                    return new Mapper(configuration, cnvMapper!, cfgMapper! );
                });    
        }
    }
}
