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
        private string _upload_records_no;
        private BusinessLogic businessLogic;
        public PurchaseManagement(string local_db, string branch_id,string upload_record_no)
        {
            InitializeComponent();
            _localDb = local_db;
            _branchId = branch_id;
            _upload_records_no= upload_record_no;
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
            Cursor.Current = Cursors.WaitCursor;
            if (businessLogic.UploadInvPurchaseAll(_localDb, _upload_records_no))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Purchase Data uploaded to server successfully");
                //Logger.write("Purchase Data uploaded to server successfully at: {time}");
            }
            else
            {
                MessageBox.Show("Purchase Data upload to server failed");
                //Logger.write("Purchase Data upload to server failed at: {time}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
