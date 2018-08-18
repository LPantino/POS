using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdminPOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTitle.Text = frmLogin.FullName;
            lblResponsible.Text = "Account Level : " + frmLogin.Responsibility;
            if (frmLogin.Responsibility != "Admin")
            {
                btnSettings.Enabled = false;
                btnUserManage.Enabled = false;
                btnItem.Enabled = false;
            }
            else
            {
                btnSettings.Enabled = true;
                btnUserManage.Enabled = true;
                btnItem.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult Logout = MessageBox.Show("Do you want to Logout ?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Logout == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Hide();
            }
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {
            frmUserManage user = new frmUserManage();
            user.ShowDialog();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            frmItemMaintenance itm_maint = new frmItemMaintenance();
            itm_maint.ShowDialog();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPINReader pin = new frmPINReader();
            DialogResult result = pin.ShowDialog();
            if (result == DialogResult.OK)
            {
                frmPOS pos = new frmPOS();
                pos.Show();
                this.Hide();
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer cust = new frmCustomer();
            cust.ShowDialog();
        }
    }
}
