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
    public partial class frmUserManage : Form
    {
        public void ReFresh()
        {
            txtID.Text = "";
            txtUser.Text = "";
            txtFullname.Text = "";
            txtAccount.Text = "";
            txtPassword.Text = "";
            btnUpdate.Enabled = true;
            txtID.Enabled = true;

            button6.Text = "New";
            txtAccountCode.Text = "";
            txtAccountDesc.Text = "";
            button5.Enabled = true;

            ServerConnection server = new ServerConnection();
            listView1.Items.Clear();
            listView2.Items.Clear();
            txtAccount.Items.Clear();

            #region Get the Acnt User
            try
            {
                string sqlAdd = "select * from acnt_usr a join usr_scrty_qustn b on a.usr_id = b.usr_id order by a.usr_id;";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["usr_id"].ToString());
                    item.SubItems.Add(read["usr_name"].ToString());
                    item.SubItems.Add(read["usr_fullname"].ToString());
                    item.SubItems.Add(read["usr_rspnsblty"].ToString());
                    item.SubItems.Add(read["usr_password"].ToString());
                    item.SubItems.Add(read["usr_question"].ToString());
                    item.SubItems.Add(read["usr_answer"].ToString());
                    item.SubItems.Add(read["usr_pin"].ToString());
                    listView1.Items.Add(item);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
            #endregion

            #region Get The Acnt Responsibility
            try
            {
                string sqlAdd = "SELECT * FROM acnt_rspnsblty";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtAccount.Items.Add(read["acnt_code"].ToString());
                    ListViewItem item = new ListViewItem(read["acnt_code"].ToString());
                    item.SubItems.Add(read["acnt_desc"].ToString());
                    listView2.Items.Add(item);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
            #endregion
        }

        public frmUserManage()
        {
            InitializeComponent();
        }

        private void frmUserManage_Load(object sender, EventArgs e)
        {
            ReFresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("UserControl");
            lblTitle.Text = "User Control";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("AccountLevel");
            lblTitle.Text = "Account Level";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            else
            {
                txtID.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtUser.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtFullname.Text = listView1.SelectedItems[0].SubItems[2].Text;
                txtAccount.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtPassword.Text = listView1.SelectedItems[0].SubItems[4].Text;
                txtQuestion.Text = listView1.SelectedItems[0].SubItems[5].Text;
                txtAnswer.Text = listView1.SelectedItems[0].SubItems[6].Text;
                txtPIN.Text = listView1.SelectedItems[0].SubItems[7].Text;
                btnNew.Text = "New";
                btnUpdate.Enabled = true;
                txtID.Enabled = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ServerConnection server = new ServerConnection();


            if (btnNew.Text == "New")
            {
                btnNew.Text = "Add";
                btnUpdate.Enabled = false;
                txtID.Text = "";
                txtUser.Text = "";
                txtFullname.Text = "";
                txtAccount.Text = "";
                txtPassword.Text = "";
                txtAnswer.Text = "";
                txtID.Enabled = true;
            }
            else
            {
                if (txtID.Text == "" || txtUser.Text == "" || txtFullname.Text == "")
                {
                    return;
                }

                DialogResult Add = MessageBox.Show("Add this User Data ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Add == DialogResult.No)
                {
                    return;
                }

                try
                {
                    string sqlAdd = "insert into acnt_usr(usr_id,usr_name,usr_password,usr_rspnsblty,usr_fullname, usr_pin)values" +
                        "('" + txtID.Text + "','" + txtUser.Text + "','" + txtPassword.Text + "','" + txtAccount.Text + "','" + txtFullname.Text + "','"+ txtPIN.Text +"')";
                    server.Connection();
                    MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                    MySqlDataReader read;
                    server.OpenConnection();
                    read = cmd.ExecuteReader();
                    server.CloseConnection();

                    string sqlAdd1 = "insert into usr_scrty_qustn(usr_id, usr_question, usr_answer)values" +
                        "('" + txtID.Text + "','" + txtQuestion.Text + "','" + txtAnswer.Text + "')";
                    server.Connection();
                    MySqlCommand cmd1 = new MySqlCommand(sqlAdd1, server.con);
                    MySqlDataReader read1;
                    server.OpenConnection();
                    read1 = cmd1.ExecuteReader();
                    server.CloseConnection();

                    MessageBox.Show("New User Added", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReFresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    server.CloseConnection();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtUser.Text == "" || txtAnswer.Text == "")
            {
                MessageBox.Show("Missing Information.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            DialogResult Update = MessageBox.Show("Do you want to Update this User ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Update == DialogResult.No)
            {
                return;
            }

            ServerConnection server = new ServerConnection();
            try
            {
                string sqlAdd = "update acnt_usr set usr_name = '" + txtUser.Text + "', usr_password = '" + txtPassword.Text + "', " +
                    "usr_rspnsblty = '" + txtAccount.Text + "', usr_fullname = '" + txtFullname.Text + "', usr_pin = "+ txtPIN.Text +" where usr_id = '" + txtID.Text + "'";
                Console.WriteLine(sqlAdd);
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                server.CloseConnection();

                string sqlAdd1 = "update usr_scrty_qustn set usr_question = '" + txtQuestion.Text + "', usr_answer = '" + txtAnswer.Text + 
                    "' where usr_id = '" + txtID.Text + "'";
                server.Connection();
                MySqlCommand cmd1 = new MySqlCommand(sqlAdd1, server.con);
                MySqlDataReader read1;
                server.OpenConnection();
                read1 = cmd1.ExecuteReader();
                server.CloseConnection();

                MessageBox.Show("Updating Data Success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReFresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            DialogResult Delete = MessageBox.Show("Do you want to REMOVE this person ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Delete == DialogResult.No)
            {
                return;
            }

            ServerConnection server = new ServerConnection();
            try
            {
                string sqlAdd = "delete from acnt_usr where usr_id = '" + txtID.Text + "'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                server.CloseConnection();

                string sqlAdd1 = "delete from usr_scrty_qustn where usr_id = '" + txtID.Text + "'";
                server.Connection();
                MySqlCommand cmd1 = new MySqlCommand(sqlAdd1, server.con);
                MySqlDataReader read1;
                server.OpenConnection();
                read1 = cmd1.ExecuteReader();
                server.CloseConnection();

                MessageBox.Show("Deleting Data Success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReFresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count <= 0)
            {
                return;
            }
            else
            {
                txtAccountCode.Text = listView2.SelectedItems[0].SubItems[0].Text;
                txtAccountDesc.Text = listView2.SelectedItems[0].SubItems[1].Text;
                button6.Text = "New";
                button5.Enabled = true;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ServerConnection server = new ServerConnection();
            if (txtAccountCode.Text != "" && txtAccountDesc.Text != "")
            {
                DialogResult Add = MessageBox.Show("Add this Account Level ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Add == DialogResult.No)
                {
                    return;
                }
                try
                {
                    string sqlAdd = "update acnt_rspnsblty set acnt_desc = '"+txtAccountDesc.Text+"', acnt_code = '"+txtAccountCode.Text+"'"+
                        "where acnt_desc = '" + txtAccountDesc.Text + "' or acnt_code = '"+txtAccountCode.Text+"'";
                    server.Connection();
                    MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                    MySqlDataReader read;
                    server.OpenConnection();
                    read = cmd.ExecuteReader();
                    server.CloseConnection();
                    MessageBox.Show("Updating Account Level Success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReFresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    server.CloseConnection();
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ServerConnection server = new ServerConnection();

            if (button6.Text == "New")
            {
                button6.Text = "Add";
                txtAccountCode.Text = "";
                txtAccountDesc.Text = "";
                button5.Enabled = false;
            }
            else
            {
                if (txtAccountCode.Text != "" && txtAccountDesc.Text != "")
                {
                    DialogResult Add = MessageBox.Show("Add this Account Level ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Add == DialogResult.No)
                    {
                        return;
                    }
                    try
                    {
                        string sqlAdd = "insert into acnt_rspnsblty(acnt_code,acnt_desc)values" +
                            "('" + txtAccountCode.Text + "','" + txtAccountDesc.Text + "')";
                        server.Connection();
                        MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                        MySqlDataReader read;
                        server.OpenConnection();
                        read = cmd.ExecuteReader();
                        server.CloseConnection();
                        MessageBox.Show("New Account Level Added", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReFresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        server.CloseConnection();
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult Delete = MessageBox.Show("Do you want to REMOVE this Account Level ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Delete == DialogResult.No)
            {
                return;
            }

            ServerConnection server = new ServerConnection();
            int ctrUsed = 0;

            try
            {
                string sqlAdd = "select * from acnt_usr where usr_rspnsblty = '"+txtAccountCode.Text+"'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    ctrUsed++;
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
                return;
            }

            if (ctrUsed > 0)
            {
                MessageBox.Show("There is someone using this Account Level.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sqlAdd = "delete from acnt_rspnsblty where acnt_desc = '" + txtAccountDesc.Text + "' or acnt_code = '" + txtAccountCode.Text + "'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                server.CloseConnection();
                MessageBox.Show("Deleting Account Level Success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReFresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            if (txtPIN.Text.Length > 4)
            {
                return;
            }
        }

        private void txtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPIN.MaxLength = 4;
            if (txtPIN.MaxLength >= 4)
            {
                return;
            }
            else
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
        }

        private void SeePin_CheckedChanged(object sender, EventArgs e)
        {
            if (SeePin.Checked)
            {
                txtPIN.UseSystemPasswordChar = false;
            }
            else
            {
                txtPIN.UseSystemPasswordChar = true;
            }
        }

        private void AccountLevel_Click(object sender, EventArgs e)
        {

        }
    }
}
