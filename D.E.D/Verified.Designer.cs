namespace D.E.D
{
    partial class Verified
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
            this.components = new System.ComponentModel.Container();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Encrypted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DateOfAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeElapsedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeOfFileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dBDataSet1 = new D.E.D.DBDataSet1();
            this.logTableAdapter1 = new D.E.D.DBDataSet1TableAdapters.LogTableAdapter();
            this.pbEncrypt = new System.Windows.Forms.ProgressBar();
            this.pbDecrypt = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(7, 12);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(98, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(7, 41);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(98, 23);
            this.btnDecrypt.TabIndex = 1;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.Encrypted,
            this.DateOfAction,
            this.timeElapsedDataGridViewTextBoxColumn,
            this.sizeOfFileDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.logBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(7, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(543, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.FillWeight = 185F;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 185;
            // 
            // Encrypted
            // 
            this.Encrypted.DataPropertyName = "Encrypted";
            this.Encrypted.FillWeight = 70F;
            this.Encrypted.HeaderText = "Encrypted";
            this.Encrypted.Name = "Encrypted";
            this.Encrypted.ReadOnly = true;
            this.Encrypted.Width = 70;
            // 
            // DateOfAction
            // 
            this.DateOfAction.DataPropertyName = "DateOfAction";
            this.DateOfAction.HeaderText = "DateOfAction";
            this.DateOfAction.Name = "DateOfAction";
            this.DateOfAction.ReadOnly = true;
            // 
            // timeElapsedDataGridViewTextBoxColumn
            // 
            this.timeElapsedDataGridViewTextBoxColumn.DataPropertyName = "TimeElapsed";
            this.timeElapsedDataGridViewTextBoxColumn.FillWeight = 75F;
            this.timeElapsedDataGridViewTextBoxColumn.HeaderText = "TimeElapsed";
            this.timeElapsedDataGridViewTextBoxColumn.Name = "timeElapsedDataGridViewTextBoxColumn";
            this.timeElapsedDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeElapsedDataGridViewTextBoxColumn.Width = 75;
            // 
            // sizeOfFileDataGridViewTextBoxColumn
            // 
            this.sizeOfFileDataGridViewTextBoxColumn.DataPropertyName = "SizeOfFile";
            this.sizeOfFileDataGridViewTextBoxColumn.FillWeight = 70F;
            this.sizeOfFileDataGridViewTextBoxColumn.HeaderText = "SizeOfFile";
            this.sizeOfFileDataGridViewTextBoxColumn.Name = "sizeOfFileDataGridViewTextBoxColumn";
            this.sizeOfFileDataGridViewTextBoxColumn.ReadOnly = true;
            this.sizeOfFileDataGridViewTextBoxColumn.Width = 70;
            // 
            // logBindingSource1
            // 
            this.logBindingSource1.DataMember = "Log";
            this.logBindingSource1.DataSource = this.dBDataSet1;
            // 
            // dBDataSet1
            // 
            this.dBDataSet1.DataSetName = "DBDataSet1";
            this.dBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // logTableAdapter1
            // 
            this.logTableAdapter1.ClearBeforeFill = true;
            // 
            // pbEncrypt
            // 
            this.pbEncrypt.Location = new System.Drawing.Point(111, 12);
            this.pbEncrypt.Name = "pbEncrypt";
            this.pbEncrypt.Size = new System.Drawing.Size(434, 23);
            this.pbEncrypt.TabIndex = 3;
            // 
            // pbDecrypt
            // 
            this.pbDecrypt.Location = new System.Drawing.Point(111, 41);
            this.pbDecrypt.Name = "pbDecrypt";
            this.pbDecrypt.Size = new System.Drawing.Size(434, 23);
            this.pbDecrypt.TabIndex = 4;
            // 
            // Verified
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 249);
            this.Controls.Add(this.pbDecrypt);
            this.Controls.Add(this.pbEncrypt);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Verified";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Verified";
            this.Load += new System.EventHandler(this.Verified_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource logBindingSource;
        private DBDataSet1 dBDataSet1;
        private System.Windows.Forms.BindingSource logBindingSource1;
        private DBDataSet1TableAdapters.LogTableAdapter logTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Encrypted;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeElapsedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeOfFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.ProgressBar pbEncrypt;
        private System.Windows.Forms.ProgressBar pbDecrypt;
    }
}