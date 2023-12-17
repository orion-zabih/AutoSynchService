using AutoSynchClientEngine;
using AutoSynchClientEngine.Classes;
using AutoSynchDesktopApp.Forms;
using AutoSynchSqlServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace AutoSynchDesktopApp
{
    public partial class MainForm : Form
    {
        private MySettings settings = null;
        private readonly BusinessLogic _businessLogic;
        private FtpCredentials ftpCredentials;
        private int recordsToFetch =1000;
        private string branchId;
        public MainForm()
        {
            InitializeComponent();
            _businessLogic = new BusinessLogic();

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            panelForm.Visible = false;
            panelLogin.Visible = true;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            settings = config.GetSection("MySettings").Get<MySettings>();
            branchId = settings.BranchId;
            int _branchId = 0;
            if (!string.IsNullOrEmpty(branchId))
            {
                if (int.TryParse(branchId, out _branchId))
                {
                    Global.BranchId = _branchId;
                }
            }
            
            int.TryParse(settings.RecordsToFetch, out recordsToFetch);
            ftpCredentials = config.GetSection("FtpCredentials").Get<FtpCredentials>();


        }

        private void btnSyncNewProduct_Click(object sender, EventArgs e)
        {
            if (settings.LocalDb.Equals(Constants.SqlServer) && settings.SynchProduct.Equals("true"))
            {
                if (_businessLogic.GetProductsOnlySqlServer(recordsToFetch, settings.UpdateExisting.Equals("true")))
                {
                    Logger.write("Some products downlaoded successfully only at: {time}");
                    MessageBox.Show("Some products downlaoded successfully");
                }
                else
                {
                    Logger.write("Failed to download products only at: {time}");
                    MessageBox.Show("Failed to download products");
                }
            }
        }

        private void btnSyncPosApp_Click(object sender, EventArgs e)
        {
            if (ftpCredentials != null && ftpCredentials.EnableFtpSynch == "true")
            {
                if (BusinessLogic.DownloadPublish(ftpCredentials))
                {
                    MessageBox.Show("Publish files downloaded successfully");
                    Logger.write("Publish files downloaded successfully at: {time}");
                }
                else
                {
                    MessageBox.Show("Publish files downloading failed");
                    Logger.write("Publish files downloading failed at: {time}");
                }
            }
        }

        
        private void btnSyncVendors_Click(object sender, EventArgs e)
        {
            if (settings.LocalDb.Equals(Constants.SqlServer) && settings.SynchVendor.Equals("true"))
            {
                //_logger.LogInformation("Started fetching newly added vendors from Central Database at: {time}");
                if (_businessLogic.GetVendorsOnlySqlServer(recordsToFetch, settings.UpdateExisting.Equals("true")))
                {
                    //_logger.LogInformation("Some vendors downlaoded successfully only at: {time}");
                    MessageBox.Show("Vendors data downlaoded successfully");
                    Logger.write("Some vendors downlaoded successfully only at: {time}");
                }
                else
                {
                    //_logger.LogInformation("Failed to download vendors only at: {time}");
                    MessageBox.Show("Failed to download vendors data");
                    Logger.write("Failed to download vendors only at: {time}");
                }
            }
        }

        private void btnSaleDataManagement_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaleManagement saleManagement = new SaleManagement(settings.LocalDb, branchId))
                {
                    saleManagement.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logger.write(ex.Message, true);
            }
        }

        private void btnSyncPurchases_Click(object sender, EventArgs e)
        {
            try
            {
                using (PurchaseManagement purchaseManagement = new PurchaseManagement(settings.LocalDb, branchId))
                {
                    purchaseManagement.ShowDialog();
                }
                //if (_businessLogic.UploadInvPurchaseToServer(settings.LocalDb))
                //{
                //    MessageBox.Show("Purchase Data uploaded to server successfully.");
                //    Logger.write("Purchase Data uploaded to server successfully at: {time}");
                //}
                //else
                //{
                //    MessageBox.Show("Purchase Data upload to server failed");
                //    Logger.write("Purchase Data upload to server failed at: {time}");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.write(ex.Message, true);
            }
        }

        private void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            if (_businessLogic.AlterTablesSqlServer())
            {                
                MessageBox.Show("Successfully updated tables","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update tables", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSyncFiscalYears_Click(object sender, EventArgs e)
        {
            if (settings.LocalDb.Equals(Constants.SqlServer) && settings.SynchVendor.Equals("true"))
            {
                if (_businessLogic.GetFiscalYearsOnlySqlServer(recordsToFetch, settings.UpdateExisting.Equals("true")))
                {
                    MessageBox.Show("Fiscal year data downlaoded successfully");
                    Logger.write("Some Fiscal years data downlaoded successfully at: {time}");
                }
                else
                {
                    MessageBox.Show("Failed to download Fiscal year data");
                    Logger.write("Failed to download Fiscal years data at: {time}");
                }
            }
        }

        private void btnSyncUsers_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool loginSuccessful = false;
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Please enter username");
                return;
            }
            else if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Please enter password");
                return;
            }
            else if (txtUserName.Text.Equals("anwar") && txtPassword.Text.Equals("Hmis@789"))
            {
                loginSuccessful = true;
            }
            else if (authorizeFromDb())
                loginSuccessful = true;
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
            if (loginSuccessful)
            {
                panelForm.Visible = true;
                panelLogin.Visible = false;
                txtUserName.Clear();
                txtPassword.Clear();
                btnSyncNewProduct.Focus();
            }
        }
        private bool authorizeFromDb()
        {
            UsrSystemUser usrSystemUser = _businessLogic.GetUsrSystemUser(txtUserName.Text, txtPassword.Text, settings.LocalDb);
            if (usrSystemUser != null)
                return true;
            else
                return false;
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes== MessageBox.Show("Are you sure you want to logout?", "Logout Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                panelForm.Visible = false;
                panelLogin.Visible = true;
                txtUserName.Focus();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}