using AutoSynchService.ApiClient;
using AutoSynchService.Classes;
using AutoSynchService.DAOs;
using AutoSynchService.Models;
using AutoSynchSqlite.DbManager;
using System.Data;

namespace AutoSynchService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                FtpCredentials ftpCredentials = config.GetSection("FtpCredentials").Get<FtpCredentials>();

                
                
                if(BusinessLogic.UploadInvSaleToServer())
                {
                    _logger.LogInformation("Data uploaded to server successfully at: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogInformation("Data upload to server failed at: {time}", DateTimeOffset.Now);
                }
                if (BusinessLogic.DownloadPublish(ftpCredentials))
                {
                    _logger.LogInformation("Publish files downloaded successfully at: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogInformation("Publish files downloading failed at: {time}", DateTimeOffset.Now);
                }
                if (BusinessLogic.GetAndReplaceSysTables())
                {
                    _logger.LogInformation("System Tables downloaded and replaced successfully at: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogInformation("System Tables downloading/replacing failed at: {time}", DateTimeOffset.Now);
                }
                
                
                MySettings settings = config.GetSection("MySettings").Get<MySettings>();
                
                await Task.Delay(Utility.CalculateBackoffTime(settings.BackoffTimerUnit,settings.BackoffTimer), stoppingToken);
            }
        }
        

    }
}