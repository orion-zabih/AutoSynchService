using AutoSynchService.ApiClient;
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
                //UploadToServer();
                AccountsClient accountsClient = new AccountsClient();
                DataResponse apiResponse = accountsClient.GetAccounts();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        private bool UploadToServer()
        {
            InvSaleDao invSaleDao = new InvSaleDao();
            
            List<InvSaleDetail> invSaleDetailList = invSaleDao.GetSaleDetails();
            return true;
        }


    }
}