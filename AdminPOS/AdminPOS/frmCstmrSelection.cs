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
    public partial class frmCstmrSelection : Form
    {
        ServerConnection server = new ServerConnection();
        public static string CstmrCode = "";
        public static string CstmrName = "";
        

        public frmCstmrSelection()
        {
            InitializeComponent();
        }

        public void GetCstmrList()
        {
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
                    listView1.Items.Add(cstmr);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Category Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        public void UpdateCstmrDefault()
        {
            try
            {
                string sqlAdd = "update cstmr_data set cstmr_default = 0 where cstmr_default = 1";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }

            try
            {
                string sqlAdd = "update cstmr_data set cstmr_default = 1 where cstmr_code = '"+ CstmrCode +"'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();         
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void frmCstmrSelection_Load(object sender, EventArgs e)
        {
            GetCstmrList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CstmrCode == "")
            {
                this.Hide();
            }
            else
            {
                DialogResult SetAsDefault = MessageBox.Show("Do you want to set this customer as default ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (SetAsDefault == DialogResult.Yes)
                {
                    UpdateCstmrDefault();
                }
                this.DialogResult = DialogResult.OK;
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            else
            {
                CstmrCode = listView1.SelectedItems[0].SubItems[0].Text;
                CstmrName = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void btnCstmrSearch_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            try
            {
                string sqlAdd = "select * from cstmr_data where cstmr_code like '%"+ txtCstmrCode.Text +"%' and cstmr_name like '%"+ txtCstmrName.Text +"%'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem cstmrResult = new ListViewItem(read["cstmr_code"].ToString());
                    cstmrResult.SubItems.Add(read["cstmr_name"].ToString());
                    listView1.Items.Add(cstmrResult);
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
