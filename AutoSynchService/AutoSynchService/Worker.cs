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
                MySettings settings = config.GetSection("MySettings").Get<MySettings>();
                string branchId=settings.BranchId;
                int _branchId = 0;
                if (!string.IsNullOrEmpty(branchId))
                {
                    if(int.TryParse(branchId, out _branchId))
                    {
                        Global.BranchId = _branchId;
                        

                        FtpCredentials ftpCredentials = config.GetSection("FtpCredentials").Get<FtpCredentials>();
                        if(ftpCredentials!=null && ftpCredentials.EnableFtpSynch == "true")
                        {
                            if (BusinessLogic.DownloadPublish(ftpCredentials))
                            {
                                _logger.LogInformation("Publish files downloaded successfully at: {time}", DateTimeOffset.Now);
                            }
                            else
                            {
                                _logger.LogInformation("Publish files downloading failed at: {time}", DateTimeOffset.Now);
                            }
                        }
                        int recordsToFetch = 1000;
                        int.TryParse(settings.RecordsToFetch, out recordsToFetch);
                        //BusinessLogic.GetAndReplaceSysTables();
                        if (settings.LocalDb.Equals(Constants.Sqlite))
                        {
                            if (BusinessLogic.GetAndReplaceSysTablesSqlite())
                            {
                                _logger.LogInformation("System Tables downloaded and replaced successfully at: {time}", DateTimeOffset.Now);
                            }
                            else
                            {
                                _logger.LogInformation("System Tables downloading/replacing failed at: {time}", DateTimeOffset.Now);
                            }
                        }
                        else if (settings.LocalDb.Equals(Constants.SqlServer))
                        {
                            
                            if (BusinessLogic.GetAndReplaceTablesSqlServer())
                            {
                                _logger.LogInformation("System Tables downloaded and replaced successfully at: {time}", DateTimeOffset.Now);
                                if (BusinessLogic.GetAndReplaceDataSqlServer(recordsToFetch))
                                {
                                    _logger.LogInformation("Data Downloaded successfully successfully at: {time}", DateTimeOffset.Now);
                                }

                            }
                            else
                            {
                                _logger.LogInformation("System Tables downloading/replacing failed at: {time}", DateTimeOffset.Now);
                            }
                        }
                        //if(!BusinessLogic.isFreshdb)
                        
                        Console.WriteLine("Preparing to upload data from server");
                        if (BusinessLogic.UploadInvSaleToServer(settings.LocalDb))
                        {
                            _logger.LogInformation("Data uploaded to server successfully at: {time}", DateTimeOffset.Now);
                        }
                        else
                        {
                            _logger.LogInformation("Data upload to server failed at: {time}", DateTimeOffset.Now);
                        }
                        if(BusinessLogic.GetProductsOnlySqlServer(recordsToFetch))
                        {

                            _logger.LogInformation("Some products downlaoded successfully only at: {time}", DateTimeOffset.Now);
                        }
                        else
                        {
                            _logger.LogInformation("Failed to download products only at: {time}", DateTimeOffset.Now);
                        }
                    }
                }
                
                
                
                await Task.Delay(Utility.CalculateBackoffTime(settings.BackoffTimerUnit,settings.BackoffTimer), stoppingToken);
            }
        }
        

    }
}