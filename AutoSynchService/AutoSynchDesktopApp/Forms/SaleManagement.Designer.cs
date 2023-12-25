namespace AutoSynchDesktopApp.Forms
{
    partial class SaleManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgUnsynchedData = new System.Windows.Forms.DataGridView();
            this.btnLoadUnSynch = new System.Windows.Forms.Button();
            this.btnSynch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnsynchedData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgUnsynchedData
            // 
            this.dgUnsynchedData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUnsynchedData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUnsynchedData.Location = new System.Drawing.Point(12, 128);
            this.dgUnsynchedData.Name = "dgUnsynchedData";
            this.dgUnsynchedData.RowHeadersWidth = 62;
            this.dgUnsynchedData.RowTemplate.Height = 33;
            this.dgUnsynchedData.Size = new System.Drawing.Size(1191, 421);
            this.dgUnsynchedData.TabIndex = 0;
            // 
            // btnLoadUnSynch
            // 
            this.btnLoadUnSynch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLoadUnSynch.BackColor = System.Drawing.Color.White;
            this.btnLoadUnSynch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLoadUnSynch.Location = new System.Drawing.Point(602, 43);
            this.btnLoadUnSynch.Name = "btnLoadUnSynch";
            this.btnLoadUnSynch.Size = new System.Drawing.Size(294, 60);
            this.btnLoadUnSynch.TabIndex = 1;
            this.btnLoadUnSynch.Text = "View Unsynched Data";
            this.btnLoadUnSynch.UseVisualStyleBackColor = false;
            this.btnLoadUnSynch.Click += new System.EventHandler(this.btnLoadUnSynch_Click);
            // 
            // btnSynch
            // 
            this.btnSynch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSynch.BackColor = System.Drawing.Color.White;
            this.btnSynch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSynch.Location = new System.Drawing.Point(287, 43);
            this.btnSynch.Name = "btnSynch";
            this.btnSynch.Size = new System.Drawing.Size(294, 60);
            this.btnSynch.TabIndex = 2;
            this.btnSynch.Text = "Synch Sale";
            this.btnSynch.UseVisualStyleBackColor = false;
            this.btnSynch.Click += new System.EventHandler(this.btnSynch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(1048, 559);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(155, 54);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SaleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1215, 625);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSynch);
            this.Controls.Add(this.btnLoadUnSynch);
            this.Controls.Add(this.dgUnsynchedData);
            this.Name = "SaleManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaleManagement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SaleManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgUnsynchedData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgUnsynchedData;
        private Button btnLoadUnSynch;
        private Button btnSynch;
        private Button btnCancel;
    }
}