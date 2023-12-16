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
            ((System.ComponentModel.ISupportInitialize)(this.dgUnsynchedData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgUnsynchedData
            // 
            this.dgUnsynchedData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUnsynchedData.Location = new System.Drawing.Point(12, 128);
            this.dgUnsynchedData.Name = "dgUnsynchedData";
            this.dgUnsynchedData.RowHeadersWidth = 62;
            this.dgUnsynchedData.RowTemplate.Height = 33;
            this.dgUnsynchedData.Size = new System.Drawing.Size(1191, 471);
            this.dgUnsynchedData.TabIndex = 0;
            // 
            // btnLoadUnSynch
            // 
            this.btnLoadUnSynch.Location = new System.Drawing.Point(602, 43);
            this.btnLoadUnSynch.Name = "btnLoadUnSynch";
            this.btnLoadUnSynch.Size = new System.Drawing.Size(294, 60);
            this.btnLoadUnSynch.TabIndex = 1;
            this.btnLoadUnSynch.Text = "View Unsynched Data";
            this.btnLoadUnSynch.UseVisualStyleBackColor = true;
            this.btnLoadUnSynch.Click += new System.EventHandler(this.btnLoadUnSynch_Click);
            // 
            // btnSynch
            // 
            this.btnSynch.Location = new System.Drawing.Point(287, 43);
            this.btnSynch.Name = "btnSynch";
            this.btnSynch.Size = new System.Drawing.Size(294, 60);
            this.btnSynch.TabIndex = 2;
            this.btnSynch.Text = "Synch Sale";
            this.btnSynch.UseVisualStyleBackColor = true;
            this.btnSynch.Click += new System.EventHandler(this.btnSynch_Click);
            // 
            // SaleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 625);
            this.Controls.Add(this.btnSynch);
            this.Controls.Add(this.btnLoadUnSynch);
            this.Controls.Add(this.dgUnsynchedData);
            this.Name = "SaleManagement";
            this.Text = "SaleManagement";
            this.Load += new System.EventHandler(this.SaleManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgUnsynchedData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgUnsynchedData;
        private Button btnLoadUnSynch;
        private Button btnSynch;
    }
}