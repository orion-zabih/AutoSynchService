using AutoSynchService.ApiClient;
using AutoSynchService.Classes;
using AutoSynchService.DAOs;
using AutoSynchSqlite.DbManager;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AutoSynchService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BusinessLogic _businessLogic;


        //public Worker(ILogger<Worker> logger)
        //{
        //    _logger = logger;

        //}

        public Worker(
            BusinessLogic businessLogic,
            ILogger<Worker> logger) =>
            (_businessLogic, _logger) = (businessLogic, logger);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Synching Data from Central Database at: {time}", DateTimeOffset.Now);
                var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false);
                IConfiguration config = builder.Build();
                MySettings settings = config.GetSection("MySettings").Get<MySettings>();
                string branchId = settings.BranchId;
                int _branchId = 0;
                if (!string.IsNullOrEmpty(branchId))
                {
                    if (int.TryParse(branchId, out _branchId))
                    {
                        _logger.LogInformation("Branch ID is {_branchId}", _branchId);
                        Global.BranchId = _branchId;
                        //FtpCredentials ftpCredentials = config.GetSection("FtpCredentials").Get<FtpCredentials>();
                        //if (ftpCredentials != null && ftpCredentials.EnableFtpSynch == "true")
                        //{
                        //    if (BusinessLogic.DownloadPublish(ftpCredentials))
                        //    {
                        //        Logger.write("Publish files downloaded successfully at: {time}");
                        //    }
                        //    else
                        //    {
                        //        Logger.write("Publish files downloading failed at: {time}");
                        //    }
                        //}
                        int recordsToFetch = 1000;
                        int.TryParse(settings.RecordsToFetch, out recordsToFetch);
                        //Logger.write("Preparing to upload data from server");
                        ////Console.WriteLine("Preparing to upload data from server");
                        //try
                        //{
                        //    if (_businessLogic.UploadInvSaleToServer(settings.LocalDb))
                        //    {
                        //        Logger.write("Sale Data uploaded to server successfully at: {time}");
                        //    }
                        //    else
                        //    {
                        //        Logger.write("Sale Data upload to server failed at: {time}");
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Logger.write(ex.Message, true);
                        //}
                        //try
                        //{
                        //    if (_businessLogic.UploadInvPurchaseToServer(settings.LocalDb))
                        //    {
                        //        Logger.write("Purchase Data uploaded to server successfully at: {time}");
                        //    }
                        //    else
                        //    {
                        //        Logger.write("Purchase Data upload to server failed at: {time}");
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Logger.write(ex.Message, true);
                        //}
                        if (settings.LocalDb.Equals(Constants.SqlServer) && settings.SynchProduct.Equals("true"))
                        {
                            _logger.LogInformation("Started fetching newly added products from Central Database at: {time}");
                            if (_businessLogic.GetProductsOnlySqlServer(recordsToFetch, settings.UpdateExisting.Equals("true")))
                            {
                                _logger.LogInformation("Some products downlaoded successfully at: {time}");
                                Logger.write("Some products downlaoded successfully only at: {time}");
                            }
                            else
                            {
                                _logger.LogInformation("Failed to download products only at: {time}");
                                Logger.write("Failed to download products only at: {time}");
                            }
                            _logger.LogInformation("Started fetching recently added products from Central Database at: {time}");
                            if (_businessLogic.GetProductsRecentSqlServer(recordsToFetch, settings.UpdateExisting.Equals("true")))
                            {
                                _logger.LogInformation("Recently added products downlaoded successfully at: {time}");
                                Logger.write("Some products downlaoded successfully only at: {time}");
                            }
                            else
                            {
                                _logger.LogInformation("Failed to download Recently added products at: {time}");
                                Logger.write("Failed to download products only at: {time}");
                            }
                        }
                        if (settings.LocalDb.Equals(Constants.SqlServer) && settings.SynchVendor.Equals("true"))
                        {
                            _logger.LogInformation("Started fetching newly added vendors from Central Database at: {time}");
                            if (_businessLogic.GetVendorsOnlySqlServer(recordsToFetch, settings.UpdateExisting.Equals("true")))
                            {
                                _logger.LogInformation("Some vendors downlaoded successfully only at: {time}");
                                Logger.write("Some vendors downlaoded successfully only at: {time}");
                            }
                            else
                            {
                                _logger.LogInformation("Failed to download vendors only at: {time}");
                                Logger.write("Failed to download vendors only at: {time}");
                            }
                        }

                    }
                }



                await Task.Delay(Utility.CalculateBackoffTime(settings.BackoffTimerUnit,settings.BackoffTimer), stoppingToken);
            }
        }
        

    }
}