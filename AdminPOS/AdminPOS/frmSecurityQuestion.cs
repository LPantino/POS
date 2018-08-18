using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace AdminPOS
{
    public partial class frmSecurityQuestion : Form
    {
        ServerConnection server = new ServerConnection();
        public string UserAnswer = "";
        public static string Password = "";
        public static string UserID = "";

        public frmSecurityQuestion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int ctr = 0;

            try
            {
                string sqlAdd = "select a.usr_id, a.usr_password, s.usr_question, s.usr_answer from acnt_usr a join usr_scrty_qustn s on a.usr_id = s.usr_id where a.usr_id = '" + textBox1.Text + "'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    label3.Text = read["usr_question"].ToString();
                    UserAnswer = read["usr_answer"].ToString();
                    Password = read["usr_password"].ToString();
                    UserID = read["usr_id"].ToString();
                    ctr++;
                }
                server.CloseConnection();

                if (ctr <= 0)
                {
                    MessageBox.Show("Account number is invalid or not existing", "Contact Administrator or POS Provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {

                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAnswer.Text == UserAnswer && UserAnswer != "")
            {
                MessageBox.Show("You will be now redirecting to change password.\nYou only have 30 second to change your password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fmrChangePassword changePass = new fmrChangePassword();
                changePass.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Answer please try again", "Invalid Answer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
