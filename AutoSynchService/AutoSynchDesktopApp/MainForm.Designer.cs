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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panelForm = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSyncNewProduct
            // 
            this.btnSyncNewProduct.BackColor = System.Drawing.Color.White;
            this.btnSyncNewProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncNewProduct.Location = new System.Drawing.Point(113, 25);
            this.btnSyncNewProduct.Name = "btnSyncNewProduct";
            this.btnSyncNewProduct.Size = new System.Drawing.Size(201, 90);
            this.btnSyncNewProduct.TabIndex = 0;
            this.btnSyncNewProduct.Text = "Sync New Products";
            this.btnSyncNewProduct.UseVisualStyleBackColor = false;
            this.btnSyncNewProduct.Click += new System.EventHandler(this.btnSyncNewProduct_Click);
            // 
            // btnSyncVendors
            // 
            this.btnSyncVendors.BackColor = System.Drawing.Color.White;
            this.btnSyncVendors.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncVendors.Location = new System.Drawing.Point(113, 137);
            this.btnSyncVendors.Name = "btnSyncVendors";
            this.btnSyncVendors.Size = new System.Drawing.Size(201, 90);
            this.btnSyncVendors.TabIndex = 1;
            this.btnSyncVendors.Text = "Sync Vendors";
            this.btnSyncVendors.UseVisualStyleBackColor = false;
            this.btnSyncVendors.Click += new System.EventHandler(this.btnSyncVendors_Click);
            // 
            // btnSyncFiscalYears
            // 
            this.btnSyncFiscalYears.BackColor = System.Drawing.Color.White;
            this.btnSyncFiscalYears.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncFiscalYears.Location = new System.Drawing.Point(453, 25);
            this.btnSyncFiscalYears.Name = "btnSyncFiscalYears";
            this.btnSyncFiscalYears.Size = new System.Drawing.Size(201, 90);
            this.btnSyncFiscalYears.TabIndex = 6;
            this.btnSyncFiscalYears.Text = "Sync Fiscal Years";
            this.btnSyncFiscalYears.UseVisualStyleBackColor = false;
            this.btnSyncFiscalYears.Click += new System.EventHandler(this.btnSyncFiscalYears_Click);
            // 
            // btnSyncPosApp
            // 
            this.btnSyncPosApp.BackColor = System.Drawing.Color.White;
            this.btnSyncPosApp.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncPosApp.Location = new System.Drawing.Point(453, 249);
            this.btnSyncPosApp.Name = "btnSyncPosApp";
            this.btnSyncPosApp.Size = new System.Drawing.Size(201, 90);
            this.btnSyncPosApp.TabIndex = 4;
            this.btnSyncPosApp.Text = "Update POS";
            this.btnSyncPosApp.UseVisualStyleBackColor = false;
            this.btnSyncPosApp.Visible = false;
            this.btnSyncPosApp.Click += new System.EventHandler(this.btnSyncPosApp_Click);
            // 
            // btnUpdateDatabase
            // 
            this.btnUpdateDatabase.BackColor = System.Drawing.Color.White;
            this.btnUpdateDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateDatabase.Location = new System.Drawing.Point(453, 137);
            this.btnUpdateDatabase.Name = "btnUpdateDatabase";
            this.btnUpdateDatabase.Size = new System.Drawing.Size(201, 90);
            this.btnUpdateDatabase.TabIndex = 5;
            this.btnUpdateDatabase.Text = "Update Database";
            this.btnUpdateDatabase.UseVisualStyleBackColor = false;
            this.btnUpdateDatabase.Visible = false;
            this.btnUpdateDatabase.Click += new System.EventHandler(this.btnUpdateDatabase_Click);
            // 
            // btnSaleDataManagement
            // 
            this.btnSaleDataManagement.BackColor = System.Drawing.Color.White;
            this.btnSaleDataManagement.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaleDataManagement.Location = new System.Drawing.Point(113, 249);
            this.btnSaleDataManagement.Name = "btnSaleDataManagement";
            this.btnSaleDataManagement.Size = new System.Drawing.Size(201, 90);
            this.btnSaleDataManagement.TabIndex = 2;
            this.btnSaleDataManagement.Text = "Sales Data Management";
            this.btnSaleDataManagement.UseVisualStyleBackColor = false;
            this.btnSaleDataManagement.Click += new System.EventHandler(this.btnSaleDataManagement_Click);
            // 
            // btnSyncPurchases
            // 
            this.btnSyncPurchases.BackColor = System.Drawing.Color.White;
            this.btnSyncPurchases.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncPurchases.Location = new System.Drawing.Point(113, 361);
            this.btnSyncPurchases.Name = "btnSyncPurchases";
            this.btnSyncPurchases.Size = new System.Drawing.Size(201, 90);
            this.btnSyncPurchases.TabIndex = 3;
            this.btnSyncPurchases.Text = "Purchase Data Management";
            this.btnSyncPurchases.UseVisualStyleBackColor = false;
            this.btnSyncPurchases.Click += new System.EventHandler(this.btnSyncPurchases_Click);
            // 
            // btnSyncUsers
            // 
            this.btnSyncUsers.BackColor = System.Drawing.Color.White;
            this.btnSyncUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSyncUsers.Location = new System.Drawing.Point(453, 361);
            this.btnSyncUsers.Name = "btnSyncUsers";
            this.btnSyncUsers.Size = new System.Drawing.Size(201, 90);
            this.btnSyncUsers.TabIndex = 7;
            this.btnSyncUsers.Text = "Sync Users";
            this.btnSyncUsers.UseVisualStyleBackColor = false;
            this.btnSyncUsers.Visible = false;
            this.btnSyncUsers.Click += new System.EventHandler(this.btnSyncUsers_Click);
            // 
            // panelLogin
            // 
            this.panelLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelLogin.BackColor = System.Drawing.Color.White;
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Controls.Add(this.pictureBox1);
            this.panelLogin.Controls.Add(this.btnCancel);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.txtUserName);
            this.panelLogin.Controls.Add(this.txtPassword);
            this.panelLogin.Location = new System.Drawing.Point(176, 72);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(769, 471);
            this.panelLogin.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(300, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 67);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sign In";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AutoSynchDesktopApp.Properties.Resources.LOGO3;
            this.pictureBox1.Location = new System.Drawing.Point(251, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(278, 109);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(157)))), ((int)(((byte)(68)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(390, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(169, 53);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(157)))), ((int)(((byte)(68)))));
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(215, 373);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(169, 53);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtUserName.Location = new System.Drawing.Point(218, 239);
            this.txtUserName.MaxLength = 32;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(344, 39);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.Location = new System.Drawing.Point(218, 303);
            this.txtPassword.MaxLength = 32;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(344, 39);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // panelForm
            // 
            this.panelForm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelForm.BackColor = System.Drawing.Color.AliceBlue;
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
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogout.Location = new System.Drawing.Point(453, 477);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(201, 70);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AutoSynchDesktopApp.Properties.Resources.bgg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1149, 594);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelForm);
            this.MinimumSize = new System.Drawing.Size(1171, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Sync App";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private PictureBox pictureBox1;
        private Label label1;
    }
}