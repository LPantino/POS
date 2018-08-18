using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AdminPOS
{
    public partial class frmCustomer : Form
    {
        ServerConnection server = new ServerConnection();

        public frmCustomer()
        {
            InitializeComponent();
        }

        public void GetCstmrList()
        {
            ListCustomer.Items.Clear();
            try
            {
                string sqlAdd = "select * from cstmr_data";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem cstmr = new ListViewItem(read["cstmr_code"].ToString());
                    cstmr.SubItems.Add(read["cstmr_name"].ToString());
                    cstmr.SubItems.Add(read["cstmr_address"].ToString());

                    ListCustomer.Items.Add(cstmr);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Category Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnItemList_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("CustomerList");
            lblTitle.Text = "Customer List";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("NewCustomer");
            lblTitle.Text = "New Customer";
            btnCustomerSave.Text = "Save Customer";
            txtCstmrCode.Enabled = true;
            txtCstmrCode.Clear();
            txtCstmrName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            GetCstmrList();
        }

        private void btnCustomerSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (btnCustomerSave.Text == "Save Customer")
                {
                    try
                    {
                        string sqlAdd = "INSERT INTO cstmr_data(" +
                        "cstmr_code, cstmr_name, cstmr_crtd, cstmr_phne, cstmr_default, cstmr_address) values (" +
                        "'" + txtCstmrCode.Text + "', " +
                        "'" + txtCstmrName.Text + "', " +
                        "'" + txtCstmrCreate.Text + "', " +
                        "'" + txtPhoneNumber.Text + "', " +
                        "0," +
                        "'" + txtAddress.Text + "'" +
                        ")";
                        server.Connection();
                        MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                        MySqlDataReader read;
                        server.OpenConnection();
                        read = cmd.ExecuteReader();
                        MessageBox.Show("New Customer Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        server.CloseConnection();
                        GetCstmrList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "New Customer Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        server.CloseConnection();
                    }
                }
                else
                {
                    try
                    {
                        string sqlAdd = "update cstmr_data set "
                        + "cstmr_name = '" + txtCstmrName.Text + "',"
                        + "cstmr_crtd = '" + txtCstmrCreate.Text + "',"
                        + "cstmr_phne = " + txtPhoneNumber.Text + ","
                        + "cstmr_address = '" + txtAddress.Text+ "'"
                        + "where cstmr_code = '"+ txtCstmrCode.Text +"'";
                        Console.WriteLine(sqlAdd);
                        server.Connection();
                        MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                        MySqlDataReader read;
                        server.OpenConnection();
                        read = cmd.ExecuteReader();
                        MessageBox.Show("Updating Customer Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btnCustomerSave.Text = "Save Customer";
                        txtCstmrCode.Enabled = true;
                        txtCstmrCode.Clear();
                        txtCstmrName.Clear();
                        txtPhoneNumber.Clear();
                        txtAddress.Clear();
                        server.CloseConnection();
                        GetCstmrList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Updating Customer Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        server.CloseConnection();
                    }
                }
            }
        }

        private void updateCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCstmrCode.Enabled = false;

            try
            {
                string sqlAdd = "select * from cstmr_data where cstmr_code = '"+txtCstmrCode.Text +"'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtCstmrCode.Text = read["cstmr_code"].ToString();
                    txtCstmrName.Text = read["cstmr_name"].ToString();
                    txtPhoneNumber.Text = read["cstmr_phne"].ToString();
                    txtCstmrCreate.Text = read["cstmr_crtd"].ToString();
                    txtAddress.Text = read["cstmr_address"].ToString();
                }
                server.CloseConnection();
                tabControl1.SelectTab("NewCustomer");
                lblTitle.Text = "New Customer";
                btnCustomerSave.Text = "Update Customer";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void ListCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListCustomer.SelectedItems.Count <= 0)
            {
                return;
            }
            else
            {
                txtCstmrCode.Text = ListCustomer.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            ListCustomer.Items.Clear();

            try
            {
                string sqlAdd = "select * from cstmr_data where cstmr_code like '%" + txtCstmrCodeSearch.Text + "%' and cstmr_name like '%" + txtCstmrNameSearch.Text + "%'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem cstmrResult = new ListViewItem(read["cstmr_code"].ToString());
                    cstmrResult.SubItems.Add(read["cstmr_name"].ToString());
                    cstmrResult.SubItems.Add(read["cstmr_address"].ToString());
                    ListCustomer.Items.Add(cstmrResult);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListCustomer.Items.Clear();

            try
            {
                string sqlAdd = "select * from cstmr_data";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem cstmrResult = new ListViewItem(read["cstmr_code"].ToString());
                    cstmrResult.SubItems.Add(read["cstmr_name"].ToString());
                    cstmrResult.SubItems.Add(read["cstmr_address"].ToString());
                    ListCustomer.Items.Add(cstmrResult);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }
    }
}
