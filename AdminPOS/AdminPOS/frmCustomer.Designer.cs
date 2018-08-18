namespace AdminPOS
{
    partial class frmCustomer
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnItemList = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.CustomerList = new System.Windows.Forms.TabPage();
            this.ListCustomer = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewCustomer = new System.Windows.Forms.TabPage();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCustomerSave = new System.Windows.Forms.Button();
            this.txtCstmrCreate = new System.Windows.Forms.DateTimePicker();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtCstmrName = new System.Windows.Forms.TextBox();
            this.txtCstmrCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCstmrCodeSearch = new System.Windows.Forms.TextBox();
            this.txtCstmrNameSearch = new System.Windows.Forms.TextBox();
            this.btnSearchCustomer = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.CustomerList.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.NewCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Location = new System.Drawing.Point(142, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(624, 44);
            this.panel2.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(123, 24);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Customer List";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnItemList);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(141, 474);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 43);
            this.button1.TabIndex = 6;
            this.button1.Text = "New Customer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnItemList
            // 
            this.btnItemList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemList.Location = new System.Drawing.Point(3, 53);
            this.btnItemList.Name = "btnItemList";
            this.btnItemList.Size = new System.Drawing.Size(133, 43);
            this.btnItemList.TabIndex = 5;
            this.btnItemList.Text = "Customer List";
            this.btnItemList.UseVisualStyleBackColor = true;
            this.btnItemList.Click += new System.EventHandler(this.btnItemList_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(4, 419);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 43);
            this.button4.TabIndex = 4;
            this.button4.Text = "Exit Customer";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.CustomerList);
            this.tabControl1.Controls.Add(this.NewCustomer);
            this.tabControl1.Location = new System.Drawing.Point(142, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 462);
            this.tabControl1.TabIndex = 3;
            // 
            // CustomerList
            // 
            this.CustomerList.Controls.Add(this.btnRefresh);
            this.CustomerList.Controls.Add(this.btnSearchCustomer);
            this.CustomerList.Controls.Add(this.txtCstmrNameSearch);
            this.CustomerList.Controls.Add(this.txtCstmrCodeSearch);
            this.CustomerList.Controls.Add(this.label8);
            this.CustomerList.Controls.Add(this.label7);
            this.CustomerList.Controls.Add(this.ListCustomer);
            this.CustomerList.Location = new System.Drawing.Point(4, 22);
            this.CustomerList.Name = "CustomerList";
            this.CustomerList.Padding = new System.Windows.Forms.Padding(3);
            this.CustomerList.Size = new System.Drawing.Size(551, 436);
            this.CustomerList.TabIndex = 0;
            this.CustomerList.Text = "CustomerList";
            this.CustomerList.UseVisualStyleBackColor = true;
            // 
            // ListCustomer
            // 
            this.ListCustomer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ListCustomer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.ListCustomer.ContextMenuStrip = this.contextMenuStrip1;
            this.ListCustomer.FullRowSelect = true;
            this.ListCustomer.Location = new System.Drawing.Point(10, 124);
            this.ListCustomer.Name = "ListCustomer";
            this.ListCustomer.Size = new System.Drawing.Size(528, 292);
            this.ListCustomer.TabIndex = 0;
            this.ListCustomer.UseCompatibleStateImageBehavior = false;
            this.ListCustomer.View = System.Windows.Forms.View.Details;
            this.ListCustomer.SelectedIndexChanged += new System.EventHandler(this.ListCustomer_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Code";
            this.columnHeader1.Width = 173;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 173;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Address";
            this.columnHeader3.Width = 177;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateCustomerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 26);
            // 
            // updateCustomerToolStripMenuItem
            // 
            this.updateCustomerToolStripMenuItem.Name = "updateCustomerToolStripMenuItem";
            this.updateCustomerToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.updateCustomerToolStripMenuItem.Text = "Update Customer";
            this.updateCustomerToolStripMenuItem.Click += new System.EventHandler(this.updateCustomerToolStripMenuItem_Click);
            // 
            // NewCustomer
            // 
            this.NewCustomer.Controls.Add(this.txtAddress);
            this.NewCustomer.Controls.Add(this.label6);
            this.NewCustomer.Controls.Add(this.btnCustomerSave);
            this.NewCustomer.Controls.Add(this.txtCstmrCreate);
            this.NewCustomer.Controls.Add(this.txtPhoneNumber);
            this.NewCustomer.Controls.Add(this.txtCstmrName);
            this.NewCustomer.Controls.Add(this.txtCstmrCode);
            this.NewCustomer.Controls.Add(this.label5);
            this.NewCustomer.Controls.Add(this.label4);
            this.NewCustomer.Controls.Add(this.label3);
            this.NewCustomer.Controls.Add(this.label2);
            this.NewCustomer.Controls.Add(this.label1);
            this.NewCustomer.Location = new System.Drawing.Point(4, 22);
            this.NewCustomer.Name = "NewCustomer";
            this.NewCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.NewCustomer.Size = new System.Drawing.Size(551, 436);
            this.NewCustomer.TabIndex = 1;
            this.NewCustomer.Text = "NewCustomer";
            this.NewCustomer.UseVisualStyleBackColor = true;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(92, 196);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(161, 20);
            this.txtAddress.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Address";
            // 
            // btnCustomerSave
            // 
            this.btnCustomerSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomerSave.Location = new System.Drawing.Point(136, 385);
            this.btnCustomerSave.Name = "btnCustomerSave";
            this.btnCustomerSave.Size = new System.Drawing.Size(278, 43);
            this.btnCustomerSave.TabIndex = 4;
            this.btnCustomerSave.Text = "Save Customer";
            this.btnCustomerSave.UseVisualStyleBackColor = true;
            this.btnCustomerSave.Click += new System.EventHandler(this.btnCustomerSave_Click);
            // 
            // txtCstmrCreate
            // 
            this.txtCstmrCreate.CustomFormat = "yyyy-MM-dd";
            this.txtCstmrCreate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCstmrCreate.Location = new System.Drawing.Point(110, 164);
            this.txtCstmrCreate.Name = "txtCstmrCreate";
            this.txtCstmrCreate.Size = new System.Drawing.Size(143, 20);
            this.txtCstmrCreate.TabIndex = 8;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(92, 133);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(161, 20);
            this.txtPhoneNumber.TabIndex = 7;
            // 
            // txtCstmrName
            // 
            this.txtCstmrName.Location = new System.Drawing.Point(92, 98);
            this.txtCstmrName.Name = "txtCstmrName";
            this.txtCstmrName.Size = new System.Drawing.Size(161, 20);
            this.txtCstmrName.TabIndex = 6;
            // 
            // txtCstmrCode
            // 
            this.txtCstmrCode.Location = new System.Drawing.Point(92, 68);
            this.txtCstmrCode.Name = "txtCstmrCode";
            this.txtCstmrCode.Size = new System.Drawing.Size(161, 20);
            this.txtCstmrCode.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Customer Since :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Phone";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Details :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Code :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Name :";
            // 
            // txtCstmrCodeSearch
            // 
            this.txtCstmrCodeSearch.Location = new System.Drawing.Point(60, 19);
            this.txtCstmrCodeSearch.Name = "txtCstmrCodeSearch";
            this.txtCstmrCodeSearch.Size = new System.Drawing.Size(205, 20);
            this.txtCstmrCodeSearch.TabIndex = 3;
            // 
            // txtCstmrNameSearch
            // 
            this.txtCstmrNameSearch.Location = new System.Drawing.Point(60, 53);
            this.txtCstmrNameSearch.Name = "txtCstmrNameSearch";
            this.txtCstmrNameSearch.Size = new System.Drawing.Size(205, 20);
            this.txtCstmrNameSearch.TabIndex = 4;
            // 
            // btnSearchCustomer
            // 
            this.btnSearchCustomer.Location = new System.Drawing.Point(60, 85);
            this.btnSearchCustomer.Name = "btnSearchCustomer";
            this.btnSearchCustomer.Size = new System.Drawing.Size(136, 28);
            this.btnSearchCustomer.TabIndex = 5;
            this.btnSearchCustomer.Text = "Search Customer";
            this.btnSearchCustomer.UseVisualStyleBackColor = true;
            this.btnSearchCustomer.Click += new System.EventHandler(this.btnSearchCustomer_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(202, 85);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(63, 28);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 474);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Maintenance";
            this.Load += new System.EventHandler(this.frmCustomer_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.CustomerList.ResumeLayout(false);
            this.CustomerList.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.NewCustomer.ResumeLayout(false);
            this.NewCustomer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnItemList;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage CustomerList;
        private System.Windows.Forms.TabPage NewCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtCstmrCreate;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtCstmrName;
        private System.Windows.Forms.TextBox txtCstmrCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCustomerSave;
        private System.Windows.Forms.ListView ListCustomer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateCustomerToolStripMenuItem;
        private System.Windows.Forms.Button btnSearchCustomer;
        private System.Windows.Forms.TextBox txtCstmrNameSearch;
        private System.Windows.Forms.TextBox txtCstmrCodeSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRefresh;
    }
}