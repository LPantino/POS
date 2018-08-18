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
    public partial class frmPINReader : Form
    {
        ServerConnection server = new ServerConnection();
        public static string FullnameUser = "";
        public static string ResponsibilityUser = "";

        public frmPINReader()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            textBox1.Text += btn.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlAdd = "select * from acnt_usr a join usr_scrty_qustn b on a.usr_id = b.usr_id where a.usr_pin = "+ textBox1.Text +"";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    ResponsibilityUser = read["usr_rspnsblty"].ToString();
                    FullnameUser = read["usr_fullname"].ToString();
                    server.CloseConnection();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Invalid PIN", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    server.CloseConnection();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPINReader_Load(object sender, EventArgs e)
        {

        }
    }
}
