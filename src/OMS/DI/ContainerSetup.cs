using OMS.Data.Access.DAL;
using System.Reflection;
using System.Runtime.CompilerServices;
using OMS.Queries.QueryProcessors;
using OMS.Maps;
using OMS.Queries.AppHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;

namespace OMS.DI
{
    public static class ContainerSetup
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        { 
            AddUow(services, configuration);
            AddQueries(services);
            ConfigureAutoMapper(services);
            AddCache(services, configuration);
        }
        private static void AddUow(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddEntityFrameworkSqlServer();

            //string connString = configuration["ConnectionStrings:SQLServer"];//Configuration.GetConnectionString("SQLServer");

            //services.AddDbContext<OMSDbContext>(options =>
            //options.UseSqlServer(connString));

            services.AddDbContext<OMSDbContext>(options =>
            options.UseSqlServer(
                       configuration.GetConnectionString("SQLServer"),
                       b => b.MigrationsAssembly(typeof(OMSDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork>(ctx => new EFUnitOfWork(ctx.GetRequiredService<OMSDbContext>()));
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            var mapperConfig = AutoMapperConfigurator.Configure();
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(x => mapper);
            services.AddTransient<IAutoMapper, AutoMapperAdapter>();
        }

        private static void AddQueries(IServiceCollection services)
        {
            var exampleProcessorType = typeof(CategoryQueryProcessor);
            var types = (from t in exampleProcessorType.GetTypeInfo().Assembly.GetTypes()
                         where t.Namespace == exampleProcessorType.Namespace
                               && t.GetTypeInfo().IsClass
                               && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                         select t).ToArray();

            foreach (var type in types)
            {
                var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }
        }

        private static void AddCache(IServiceCollection services, IConfiguration configuration)
        {
            // настройки конфигураций 
            services.Configure<CacheConfiguration>(configuration.GetSection("CacheConfiguration"));

            // для In-Memory Caching
            services.AddMemoryCache();
            services.AddTransient<MemoryCacheService>();

            // todo: добавить для Redis
            // для RedisCacheService

            services.AddTransient<Func<CacheTech, ICacheService>>(serviceProvider => key =>
            { // todo: по идее свич не нужен, так как используется только In-Memory Caching
                switch (key)
                {
                    case CacheTech.Memory:
                        return serviceProvider.GetService<MemoryCacheService>();
                    // место для case для Redis
                    default:
                        return serviceProvider.GetService<MemoryCacheService>();
                }
            });

            // строка подключения для хранения данных задания Hangfire
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("SQLServer")));
            services.AddHangfireServer();
        }
    }
}