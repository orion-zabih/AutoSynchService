using AutoSynchClientEngine;
using AutoSynchClientEngine.ApiClient;
using AutoSynchClientEngine.Classes;
using AutoSynchClientEngine.DAOs;
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
                        
                        int recordsToFetch = 1000;
                        int.TryParse(settings.RecordsToFetch, out recordsToFetch);
                        
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