using AutoSynchClientEngine;
using AutoSynchService;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<BusinessLogic>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
