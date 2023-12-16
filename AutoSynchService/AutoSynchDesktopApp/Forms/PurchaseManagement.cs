using AutoSynchClientEngine;
using AutoSynchSqlServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSynchDesktopApp.Forms
{
    public partial class PurchaseManagement : Form
    {
        private string _localDb;
        private string _branchId;
        private BusinessLogic businessLogic;
        public PurchaseManagement(string local_db, string branch_id)
        {
            InitializeComponent();
            _localDb = local_db;
            _branchId = branch_id;
            businessLogic = new BusinessLogic();
        }
        private void PurchaseManagement_Load(object sender, EventArgs e)
        {
        }
        private void LoadUnsynchedData()
        {
            List<InvPurchaseMaster> invPurchaseMaster = new List<InvPurchaseMaster>();

            invPurchaseMaster = businessLogic.GetUnSynchedInvPurchase(_localDb);
            dgUnsynchedData.DataSource = invPurchaseMaster;
        }
        private void btnLoadUnSynch_Click(object sender, EventArgs e)
        {
            LoadUnsynchedData();
        }
        private void btnSynch_Click(object sender, EventArgs e)
        {
            if (businessLogic.UploadInvPurchaseToServer(_localDb))
            {
                MessageBox.Show("Purchase Data uploaded to server successfully");
                //Logger.write("Purchase Data uploaded to server successfully at: {time}");
            }
            else
            {
                MessageBox.Show("Purchase Data upload to server failed");
                //Logger.write("Purchase Data upload to server failed at: {time}");
            }
        }        
    }
}
