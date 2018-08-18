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
    public partial class fmrChangePassword : Form
    {
        public int timeLeft = 30;
        ServerConnection server = new ServerConnection();

        public fmrChangePassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void fmrChangePassword_Load(object sender, EventArgs e)
        {
            frmSecurityQuestion security = new frmSecurityQuestion();

            timeLeft = 30;
            txtPassword.Text = frmSecurityQuestion.Password;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft.ToString();
            }
            else
            {
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtReTypeNewPass.Text)
            {
                MessageBox.Show("Password are not same", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string sqlAdd = "update acnt_usr set usr_password = '"+ txtNewPassword.Text +"' where usr_id = '"+ frmSecurityQuestion.UserID +"'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                MessageBox.Show("Changing Password success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                server.CloseConnection();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }
    }
}
