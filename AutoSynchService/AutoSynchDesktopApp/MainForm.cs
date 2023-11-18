using AutoSynchClientEngine.Classes;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AutoSynchDesktopApp
{
    public partial class MainForm : Form
    {
        private MySettings settings = null;
        private FtpCredentials ftpCredentials = null;
        public MainForm()
        {
            InitializeComponent();


        }

        private void btnSyncNewProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnSyncPosApp_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            settings = config.GetSection("MySettings").Get<MySettings>();
            string branchId = settings.BranchId;
            int _branchId = 0;
            if (!string.IsNullOrEmpty(branchId))
            {
                if (int.TryParse(branchId, out _branchId))
                {
                    Global.BranchId = _branchId;
                }
            }
            ftpCredentials = config.GetSection("FtpCredentials").Get<FtpCredentials>();
        }
    }
}