using AutoSynchPosService.Classes;

namespace AutoSynchPoSService
{
    //public class Worker : BackgroundService
    //{
    //    private readonly ILogger<Worker> _logger;

    //    public Worker(ILogger<Worker> logger)
    //    {
    //        _logger = logger;
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    //            await Task.Delay(1000, stoppingToken);
    //        }
    //    }
    //}

    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly BusinessLogic _businessLogic;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            BusinessLogic businessLogic,
            ILogger<WindowsBackgroundService> logger) =>
            (_businessLogic, _logger) = (businessLogic, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
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
                            Global.BranchId = _branchId;


                            FtpCredentials ftpCredentials = config.GetSection("FtpCredentials").Get<FtpCredentials>();
                            if (ftpCredentials != null && ftpCredentials.EnableFtpSynch == "true")
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
                                if (_businessLogic.GetAndReplaceSysTablesSqlite())
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

                                if (_businessLogic.GetAndReplaceTablesSqlServer())
                                {
                                    _logger.LogInformation("System Tables downloaded and replaced successfully at: {time}", DateTimeOffset.Now);
                                    if (_businessLogic.GetAndReplaceDataSqlServer(recordsToFetch))
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
                            _logger.LogInformation("Preparing to upload data from server");
                            //Console.WriteLine("Preparing to upload data from server");
                            if (_businessLogic.UploadInvSaleToServer(settings.LocalDb))
                            {
                                _logger.LogInformation("Data uploaded to server successfully at: {time}", DateTimeOffset.Now);
                            }
                            else
                            {
                                _logger.LogInformation("Data upload to server failed at: {time}", DateTimeOffset.Now);
                            }
                            if (_businessLogic.GetProductsOnlySqlServer(recordsToFetch))
                            {

                                _logger.LogInformation("Some products downlaoded successfully only at: {time}", DateTimeOffset.Now);
                            }
                            else
                            {
                                _logger.LogInformation("Failed to download products only at: {time}", DateTimeOffset.Now);
                            }
                        }
                    }



                    await Task.Delay(Utility.CalculateBackoffTime(settings.BackoffTimerUnit, settings.BackoffTimer), stoppingToken);
                }
                //{
                //    bool isSuccess = _businessLogic.GetAndReplaceTablesSqlServer();
                //    _logger.LogWarning("{POS Sale Service}", isSuccess);

                //    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                //}
            }
            catch (TaskCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);
            }
        }
    }
}