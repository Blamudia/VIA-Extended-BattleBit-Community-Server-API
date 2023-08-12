using BBR.Community.API;
using BBR.Community.API.Other.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using EFCoreSecondLevelCacheInterceptor;
using MessagePack.Resolvers;
using MessagePack.Formatters;
using BBR.Community.API.Other.Cache;
using MessagePack;
using BBR.Community.API.Modules.GameModes.Infected.Instance;

var builder = InteractableServiceProvider.Builder;

builder.ConfigureServices((context, services) =>
{
    var config = InteractableServiceProvider.Configuration;

    services.AddDbContext<ApiContext>((serviceProvider, options) =>
    {
        options.UseLazyLoadingProxies()
        .UseNpgsql(config!.Context.ConnectionString);
    });

    if (config.Redis.Enabled)
    {
        services.AddEFSecondLevelCache(options =>
        {
            options.UseEasyCachingCoreProvider(config!.Redis.SerializerName)
                .UseCacheKeyPrefix("EF_");
            options.CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromDays(7));
        });

        services.AddEasyCaching(options =>
        {
            options.UseRedis(settings =>
            {
                settings.MaxRdSecond = config!.Redis.MaxRdSecond;
                settings.EnableLogging = config!.Redis.EnableLogging;
                settings.LockMs = config!.Redis.LockMs;
                settings.SleepMs = config!.Redis.SleepMs;
                settings.SerializerName = config!.Redis.SerializerName;
                settings.DBConfig = new EasyCaching.Redis.RedisDBOptions()
                {
                    Password = config!.Redis.DbConfig.Password,
                    IsSsl = config!.Redis.DbConfig.IsSsl,
                    SslHost = config!.Redis.DbConfig.SslHost,
                    SyncTimeout = config!.Redis.DbConfig.SyncTimeout,
                    AsyncTimeout = config!.Redis.DbConfig.AsyncTimeout,
                    ConnectionTimeout = config!.Redis.DbConfig.ConnectionTimeout,
                    AllowAdmin = config!.Redis.DbConfig.AllowAdmin,
                };

                config!.Redis.DbConfig.Endpoints.ForEach(endpoint =>
                    settings.DBConfig.Endpoints.Add(
                        new EasyCaching.Core.Configurations.ServerEndPoint(endpoint.Host, endpoint.Port))
                );
            }, config!.Redis.SerializerName)
                .WithMessagePack(options =>
                {
                    options.EnableCustomResolver = true;
                    options.CustomResolvers = CompositeResolver.Create(
                        new IMessagePackFormatter[]
                        {
                    DBNullFormatter.Instance, // This is necessary for the null values
                        },
                        new IFormatterResolver[]
                        {
                    NativeDateTimeResolver.Instance,
                    ContractlessStandardResolver.Instance,
                    StandardResolverAllowPrivate.Instance,
                        });
                }, config!.Redis.SerializerName);
        });
    }
});

var app = builder.Build();

if (InteractableServiceProvider.Configuration.EfCore.MigrateOnStartup)
{
    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetService<ApiContext>();
        context?.Database.Migrate();
    }
}

app.Run();