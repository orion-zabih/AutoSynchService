namespace AutoSynchDesktopApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSyncNewProduct = new System.Windows.Forms.Button();
            this.btnSyncVendors = new System.Windows.Forms.Button();
            this.btnSyncFiscalYears = new System.Windows.Forms.Button();
            this.btnSyncPosApp = new System.Windows.Forms.Button();
            this.btnUpdateDatabase = new System.Windows.Forms.Button();
            this.btnSyncSaleData = new System.Windows.Forms.Button();
            this.btnSyncPurchases = new System.Windows.Forms.Button();
            this.btnSyncUsers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSyncNewProduct
            // 
            this.btnSyncNewProduct.Location = new System.Drawing.Point(274, 66);
            this.btnSyncNewProduct.Name = "btnSyncNewProduct";
            this.btnSyncNewProduct.Size = new System.Drawing.Size(201, 90);
            this.btnSyncNewProduct.TabIndex = 0;
            this.btnSyncNewProduct.Text = "Sync New Products";
            this.btnSyncNewProduct.UseVisualStyleBackColor = true;
            this.btnSyncNewProduct.Click += new System.EventHandler(this.btnSyncNewProduct_Click);
            // 
            // btnSyncVendors
            // 
            this.btnSyncVendors.Location = new System.Drawing.Point(274, 178);
            this.btnSyncVendors.Name = "btnSyncVendors";
            this.btnSyncVendors.Size = new System.Drawing.Size(201, 90);
            this.btnSyncVendors.TabIndex = 1;
            this.btnSyncVendors.Text = "Sync Vendors";
            this.btnSyncVendors.UseVisualStyleBackColor = true;
            // 
            // btnSyncFiscalYears
            // 
            this.btnSyncFiscalYears.Location = new System.Drawing.Point(614, 290);
            this.btnSyncFiscalYears.Name = "btnSyncFiscalYears";
            this.btnSyncFiscalYears.Size = new System.Drawing.Size(201, 90);
            this.btnSyncFiscalYears.TabIndex = 2;
            this.btnSyncFiscalYears.Text = "Sync Fiscal Years";
            this.btnSyncFiscalYears.UseVisualStyleBackColor = true;
            // 
            // btnSyncPosApp
            // 
            this.btnSyncPosApp.Location = new System.Drawing.Point(614, 66);
            this.btnSyncPosApp.Name = "btnSyncPosApp";
            this.btnSyncPosApp.Size = new System.Drawing.Size(201, 90);
            this.btnSyncPosApp.TabIndex = 3;
            this.btnSyncPosApp.Text = "Update POS";
            this.btnSyncPosApp.UseVisualStyleBackColor = true;
            this.btnSyncPosApp.Click += new System.EventHandler(this.btnSyncPosApp_Click);
            // 
            // btnUpdateDatabase
            // 
            this.btnUpdateDatabase.Location = new System.Drawing.Point(614, 178);
            this.btnUpdateDatabase.Name = "btnUpdateDatabase";
            this.btnUpdateDatabase.Size = new System.Drawing.Size(201, 90);
            this.btnUpdateDatabase.TabIndex = 4;
            this.btnUpdateDatabase.Text = "Update Database";
            this.btnUpdateDatabase.UseVisualStyleBackColor = true;
            // 
            // btnSyncSaleData
            // 
            this.btnSyncSaleData.Location = new System.Drawing.Point(274, 290);
            this.btnSyncSaleData.Name = "btnSyncSaleData";
            this.btnSyncSaleData.Size = new System.Drawing.Size(201, 90);
            this.btnSyncSaleData.TabIndex = 5;
            this.btnSyncSaleData.Text = "Sync Sales Data";
            this.btnSyncSaleData.UseVisualStyleBackColor = true;
            // 
            // btnSyncPurchases
            // 
            this.btnSyncPurchases.Location = new System.Drawing.Point(274, 402);
            this.btnSyncPurchases.Name = "btnSyncPurchases";
            this.btnSyncPurchases.Size = new System.Drawing.Size(201, 90);
            this.btnSyncPurchases.TabIndex = 7;
            this.btnSyncPurchases.Text = "Sync Purchase Data";
            this.btnSyncPurchases.UseVisualStyleBackColor = true;
            // 
            // btnSyncUsers
            // 
            this.btnSyncUsers.Location = new System.Drawing.Point(614, 402);
            this.btnSyncUsers.Name = "btnSyncUsers";
            this.btnSyncUsers.Size = new System.Drawing.Size(201, 90);
            this.btnSyncUsers.TabIndex = 6;
            this.btnSyncUsers.Text = "Sync Users";
            this.btnSyncUsers.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 594);
            this.Controls.Add(this.btnSyncPurchases);
            this.Controls.Add(this.btnSyncUsers);
            this.Controls.Add(this.btnSyncSaleData);
            this.Controls.Add(this.btnUpdateDatabase);
            this.Controls.Add(this.btnSyncPosApp);
            this.Controls.Add(this.btnSyncFiscalYears);
            this.Controls.Add(this.btnSyncVendors);
            this.Controls.Add(this.btnSyncNewProduct);
            this.MaximumSize = new System.Drawing.Size(1171, 650);
            this.MinimumSize = new System.Drawing.Size(1171, 650);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnSyncNewProduct;
        private Button btnSyncVendors;
        private Button btnSyncFiscalYears;
        private Button btnSyncPosApp;
        private Button btnUpdateDatabase;
        private Button btnSyncSaleData;
        private Button btnSyncPurchases;
        private Button btnSyncUsers;
    }
}