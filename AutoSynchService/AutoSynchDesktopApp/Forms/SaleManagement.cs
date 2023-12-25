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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace AutoSynchDesktopApp.Forms
{
    public partial class SaleManagement : Form
    {
        private string _localDb;
        private string _branchId;
        private string _upload_records_no;
        private BusinessLogic businessLogic ;
        public SaleManagement(string local_db,string branch_id, string upload_record_no)
        {
            InitializeComponent();
            _localDb = local_db;
            _branchId = branch_id;
            _upload_records_no = upload_record_no;
            businessLogic = new BusinessLogic();
        }

        private void btnLoadUnSynch_Click(object sender, EventArgs e)
        {

            LoadUnsynchedData();
        }

        private void SaleManagement_Load(object sender, EventArgs e)
        {
        }
        private void LoadUnsynchedData()
        {
            List<InvSaleMaster> invSaleMasters = new List<InvSaleMaster>();
            
            invSaleMasters = businessLogic.GetUnSynchedInvSale(_localDb, _branchId);
            dgUnsynchedData.DataSource = invSaleMasters;
        }
        private void btnSynch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (businessLogic.UploadInvSaleToServerAll(_localDb,_branchId,_upload_records_no))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Sale Data uploaded to server successfully");
                //Logger.write("Sale Data uploaded to server successfully at: {time}");
            }
            else
            {
                MessageBox.Show("Sale Data upload to server failed");
                //Logger.write("Sale Data upload to server failed at: {time}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
