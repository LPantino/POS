using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AdminPOS.Properties;
using MySql.Data.MySqlClient;

namespace AdminPOS
{
    public partial class frmSettings : Form
    {
     
        ServerConnection server = new ServerConnection();

        public frmSettings()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Maintenance";
            tabControl1.SelectTab("Maintenance");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Server Connection";
            tabControl1.SelectTab("ServerConnection");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Store Information";
            tabControl1.SelectTab("StoreInformation");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult Warning = MessageBox.Show("Do you want to save changes ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Warning == DialogResult.No)
            {
                return;
            }

            Settings.Default["POSCode"] = txtPOSCode.Text;
            Settings.Default["Server"] = txtServer.Text;
            Settings.Default["User"] = txtUser.Text;
            Settings.Default["Password"] = txtPassword.Text;
            Settings.Default["Port"] = txtPort.Text;
            Settings.Default.Save();
            MessageBox.Show("Database Changes Complete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtServer.Text = Settings.Default["Server"].ToString();
            txtPort.Text = Settings.Default["Port"].ToString();
            txtPOSCode.Text = Settings.Default["POSCode"].ToString();
            txtUser.Text = Settings.Default["User"].ToString();
            txtPassword.Text = Settings.Default["Password"].ToString();
            if (frmLogin.DefaultUserAccess == false)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }


            //Get The POS Settings in built in settings
            //
            txtImageDefaultPath.Text = Settings.Default["ImagePath"].ToString();
            //
            //


            //Get the Store Information in Database
            //
            try
            {
                string sqlAdd = "select * from str_info";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtServiceCharge.Text = read["srvc_chrge"].ToString();
                    txtInvoiceNumber.Text = read["OR_Num"].ToString();
                }
                server.CloseConnection();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
            //
            //
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default["ImagePath"] = txtImageDefaultPath.Text;
                Settings.Default.Save();
                MessageBox.Show("Saving Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtImageDefaultPath.Text = open.FileName.ToString(); 
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as Microsoft.Office.Interop.Excel.TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlAdd = "update str_info set srvc_chrge = "+ txtServiceCharge.Text +", OR_Num = "+ txtInvoiceNumber.Text +"";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                MessageBox.Show("Store information updated");
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void txtInvoiceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as Microsoft.Office.Interop.Excel.TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }
    }
}
