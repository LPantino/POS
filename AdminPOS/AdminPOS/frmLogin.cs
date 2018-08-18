using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AdminPOS.Properties;

namespace AdminPOS
{
    public partial class frmLogin : Form
    {
        public string DefaultUser = Settings.Default["DefaultUsername"].ToString();
        public string DefaultPassword = Settings.Default["DefaultPassword"].ToString();
        public static string FullName = "";
        public static string Responsibility = "";
        public static bool DefaultUserAccess = false;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerConnection server = new ServerConnection();

            if (checkBox1.Checked)
            {
                if (txtUsername.Text == DefaultUser && txtPassword.Text == DefaultPassword)
                {
                    DefaultUserAccess = true;
                    Responsibility = "Admin";
                    FullName = "Dashboard - Default User Account";

                    this.Hide();
                    Form1 Dashboard = new Form1();
                    DialogResult LoginSuccess = Dashboard.ShowDialog();
                    if (LoginSuccess == DialogResult.OK)
                    {
                        this.Show();
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        checkBox1.Checked = false;
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid Default User", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                DefaultUserAccess = false;
                try
                {
                    string sqlAdd = "SELECT * FROM acnt_usr where usr_name = '" + txtUsername.Text + "' and usr_password = '" + txtPassword.Text + "'";
                    server.Connection();
                    MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                    MySqlDataReader read;
                    server.OpenConnection();
                    read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        Responsibility = read["usr_rspnsblty"].ToString();
                        FullName = "Dashboard - " + read["usr_fullname"].ToString();
                        this.Hide();
                        Form1 Dashboard = new Form1();
                        DialogResult LoginSuccess = Dashboard.ShowDialog();
                        if (LoginSuccess == DialogResult.OK)
                        {
                            this.Show();
                            txtPassword.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password", "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSecurityQuestion security = new frmSecurityQuestion();
            DialogResult resultSecured = security.ShowDialog();
            if (resultSecured == DialogResult.OK)
            {

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
