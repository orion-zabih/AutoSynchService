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
            this.btnSaleDataManagement = new System.Windows.Forms.Button();
            this.btnSyncPurchases = new System.Windows.Forms.Button();
            this.btnSyncUsers = new System.Windows.Forms.Button();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panelForm = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSyncNewProduct
            // 
            this.btnSyncNewProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncNewProduct.Location = new System.Drawing.Point(113, 25);
            this.btnSyncNewProduct.Name = "btnSyncNewProduct";
            this.btnSyncNewProduct.Size = new System.Drawing.Size(201, 90);
            this.btnSyncNewProduct.TabIndex = 0;
            this.btnSyncNewProduct.Text = "Sync New Products";
            this.btnSyncNewProduct.UseVisualStyleBackColor = true;
            this.btnSyncNewProduct.Click += new System.EventHandler(this.btnSyncNewProduct_Click);
            // 
            // btnSyncVendors
            // 
            this.btnSyncVendors.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncVendors.Location = new System.Drawing.Point(113, 137);
            this.btnSyncVendors.Name = "btnSyncVendors";
            this.btnSyncVendors.Size = new System.Drawing.Size(201, 90);
            this.btnSyncVendors.TabIndex = 1;
            this.btnSyncVendors.Text = "Sync Vendors";
            this.btnSyncVendors.UseVisualStyleBackColor = true;
            this.btnSyncVendors.Click += new System.EventHandler(this.btnSyncVendors_Click);
            // 
            // btnSyncFiscalYears
            // 
            this.btnSyncFiscalYears.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncFiscalYears.Location = new System.Drawing.Point(453, 249);
            this.btnSyncFiscalYears.Name = "btnSyncFiscalYears";
            this.btnSyncFiscalYears.Size = new System.Drawing.Size(201, 90);
            this.btnSyncFiscalYears.TabIndex = 6;
            this.btnSyncFiscalYears.Text = "Sync Fiscal Years";
            this.btnSyncFiscalYears.UseVisualStyleBackColor = true;
            this.btnSyncFiscalYears.Visible = false;
            this.btnSyncFiscalYears.Click += new System.EventHandler(this.btnSyncFiscalYears_Click);
            // 
            // btnSyncPosApp
            // 
            this.btnSyncPosApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncPosApp.Location = new System.Drawing.Point(453, 25);
            this.btnSyncPosApp.Name = "btnSyncPosApp";
            this.btnSyncPosApp.Size = new System.Drawing.Size(201, 90);
            this.btnSyncPosApp.TabIndex = 4;
            this.btnSyncPosApp.Text = "Update POS";
            this.btnSyncPosApp.UseVisualStyleBackColor = true;
            this.btnSyncPosApp.Click += new System.EventHandler(this.btnSyncPosApp_Click);
            // 
            // btnUpdateDatabase
            // 
            this.btnUpdateDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateDatabase.Location = new System.Drawing.Point(453, 137);
            this.btnUpdateDatabase.Name = "btnUpdateDatabase";
            this.btnUpdateDatabase.Size = new System.Drawing.Size(201, 90);
            this.btnUpdateDatabase.TabIndex = 5;
            this.btnUpdateDatabase.Text = "Update Database";
            this.btnUpdateDatabase.UseVisualStyleBackColor = true;
            this.btnUpdateDatabase.Visible = false;
            this.btnUpdateDatabase.Click += new System.EventHandler(this.btnUpdateDatabase_Click);
            // 
            // btnSaleDataManagement
            // 
            this.btnSaleDataManagement.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaleDataManagement.Location = new System.Drawing.Point(113, 249);
            this.btnSaleDataManagement.Name = "btnSaleDataManagement";
            this.btnSaleDataManagement.Size = new System.Drawing.Size(201, 90);
            this.btnSaleDataManagement.TabIndex = 2;
            this.btnSaleDataManagement.Text = "Sales Data Management";
            this.btnSaleDataManagement.UseVisualStyleBackColor = true;
            this.btnSaleDataManagement.Click += new System.EventHandler(this.btnSaleDataManagement_Click);
            // 
            // btnSyncPurchases
            // 
            this.btnSyncPurchases.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncPurchases.Location = new System.Drawing.Point(113, 361);
            this.btnSyncPurchases.Name = "btnSyncPurchases";
            this.btnSyncPurchases.Size = new System.Drawing.Size(201, 90);
            this.btnSyncPurchases.TabIndex = 3;
            this.btnSyncPurchases.Text = "Purchase Data Management";
            this.btnSyncPurchases.UseVisualStyleBackColor = true;
            this.btnSyncPurchases.Click += new System.EventHandler(this.btnSyncPurchases_Click);
            // 
            // btnSyncUsers
            // 
            this.btnSyncUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncUsers.Location = new System.Drawing.Point(453, 361);
            this.btnSyncUsers.Name = "btnSyncUsers";
            this.btnSyncUsers.Size = new System.Drawing.Size(201, 90);
            this.btnSyncUsers.TabIndex = 7;
            this.btnSyncUsers.Text = "Sync Users";
            this.btnSyncUsers.UseVisualStyleBackColor = true;
            this.btnSyncUsers.Visible = false;
            this.btnSyncUsers.Click += new System.EventHandler(this.btnSyncUsers_Click);
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.btnCancel);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.txtUserName);
            this.panelLogin.Controls.Add(this.txtPassword);
            this.panelLogin.Location = new System.Drawing.Point(151, 35);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(769, 471);
            this.panelLogin.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(390, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(169, 53);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.Location = new System.Drawing.Point(215, 261);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(169, 53);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtUserName.Location = new System.Drawing.Point(215, 127);
            this.txtUserName.MaxLength = 32;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(344, 39);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.Location = new System.Drawing.Point(215, 191);
            this.txtPassword.MaxLength = 32;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(344, 39);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // panelForm
            // 
            this.panelForm.Controls.Add(this.btnLogout);
            this.panelForm.Controls.Add(this.btnSyncVendors);
            this.panelForm.Controls.Add(this.btnSyncPurchases);
            this.panelForm.Controls.Add(this.btnSyncNewProduct);
            this.panelForm.Controls.Add(this.btnSyncUsers);
            this.panelForm.Controls.Add(this.btnSyncFiscalYears);
            this.panelForm.Controls.Add(this.btnSaleDataManagement);
            this.panelForm.Controls.Add(this.btnSyncPosApp);
            this.panelForm.Controls.Add(this.btnUpdateDatabase);
            this.panelForm.Location = new System.Drawing.Point(176, 32);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(769, 550);
            this.panelForm.TabIndex = 4;
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogout.Location = new System.Drawing.Point(453, 477);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(201, 70);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 594);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelLogin);
            this.MaximumSize = new System.Drawing.Size(1171, 650);
            this.MinimumSize = new System.Drawing.Size(1171, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Sync App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnSyncNewProduct;
        private Button btnSyncVendors;
        private Button btnSyncFiscalYears;
        private Button btnSyncPosApp;
        private Button btnUpdateDatabase;
        private Button btnSaleDataManagement;
        private Button btnSyncPurchases;
        private Button btnSyncUsers;
        private Panel panelLogin;
        private Button btnCancel;
        private Button btnLogin;
        private TextBox txtUserName;
        private TextBox txtPassword;
        private Panel panelForm;
        private Button btnLogout;
    }
}