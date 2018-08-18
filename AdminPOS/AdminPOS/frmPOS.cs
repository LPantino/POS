using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace AdminPOS
{
    public partial class frmPOS : Form
    {
        ServerConnection server = new ServerConnection();
        public static string ItemPrice;
        public static string ItemName;
        public static string ItemBarcode;
        public static string OR_Num;
        public string ORSeries = "00000000";
     

        public frmPOS()
        {
            InitializeComponent();
        }

        public void GetCstmrDefault()
        {
            try
            {
                string sqlAdd = "select * from cstmr_data where cstmr_default = 1";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                if (read.Read())
                {
                    txtCustomerName.Text = read["cstmr_name"].ToString();
                    txtCustomerCode.Text = read["cstmr_code"].ToString();
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Category Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm tt");
            lblDate.Text = DateTime.Now.ToString("MMMM dd yyyy");
            lblCashierName.Text = frmPINReader.FullnameUser;
            lblLevel.Text = frmPINReader.ResponsibilityUser;

            GetCstmrDefault();

            btnPayout.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            
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
                    OR_Num = read["OR_Num"].ToString();
                }
                server.CloseConnection();

                ORSeries = ORSeries.Substring(0, 6 - OR_Num.Length);
                OR_Num = ORSeries + OR_Num;
                txtOrNumSeries.Text = OR_Num;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }


            try
            {
                string sqlAdd = "select * from pos_categ";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                int top = 10;
                int left = 10;
                int width = 178;
                int height = 39;
                int newSize = 12;
                while (read.Read())
                {
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Width = width;
                    button.Height = height;
                    button.BackColor = Color.DodgerBlue;
                    button.ForeColor = Color.White;
                    button.Text = read["categ_desc"].ToString();
                    button.Name = read["categ_id"].ToString();
                    button.Font = new Font(button.Font.FontFamily, newSize);
                    button.Click += new EventHandler(button_Click);
                    CategoryPanel.Controls.Add(button);
                    top += button.Height + 5;
                    
                }


                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        public void button_Click(object sender, EventArgs e)
        {

            ItemPanel.Controls.Clear();
            var button = sender as Button;

            try
            {
                string sqlAdd = "select * from pos_itm where itm_avbl = 'Yes' and itm_categ = "+ button.Name +"";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                int top = 10;
                int left = 10;
                int ctr = 0;

                //int paneltop = 10;
                //int panelleft = 10;
                int panelWidth = 157;

                while (read.Read())
                {

                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Top = top;
                    pictureBox1.Left = left;
                    pictureBox1.Name = read["bar_code"].ToString();
                    pictureBox1.Size = new System.Drawing.Size(157, 140);
                    byte[] img = (byte[])(read["itm_image"]);
                    MemoryStream mStream = new MemoryStream(img);
                    pictureBox1.Image = System.Drawing.Image.FromStream(mStream);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Click += new EventHandler(Item_Click);
                    ItemPanel.Controls.Add(pictureBox1);
                    left += pictureBox1.Width + 24;

                    Label label = new Label();
                    label.Text = read["itm_desc"].ToString();
                    label.Width = panelWidth;
                    label.Top = 120;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.BackColor = Color.White;
                    label.ForeColor = Color.Black;
                    pictureBox1.Controls.Add(label);

                    ctr++;
                    if (ctr >= 5)
                    {
                        top += pictureBox1.Height + 30;
                        left = 10;
                        ctr = 0;
                    }

                }


                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }

        }

        private void txtCategorySearch_TextChanged(object sender, EventArgs e)
        {
            CategoryPanel.Controls.Clear();

            try
            {
                string sqlAdd = "select * from pos_categ where categ_desc like '%"+txtCategorySearch.Text+"%'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                int top = 10;
                int left = 10;
                int width = 178;
                int height = 39;
                int newSize = 12;
                while (read.Read())
                {
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Width = width;
                    button.Height = height;
                    button.BackColor = Color.DodgerBlue;
                    button.ForeColor = Color.White;
                    button.Text = read["categ_desc"].ToString();
                    button.Name = read["categ_id"].ToString();
                    button.Font = new Font(button.Font.FontFamily, newSize);
                    button.Click += new EventHandler(button_Click);
                    CategoryPanel.Controls.Add(button);
                    top += button.Height + 5;
                }

                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to pin out your POS?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 dashboard = new Form1();
                dashboard.Show();
                this.Hide();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ItemPanel.Controls.Clear();
            var button = sender as Button;

            try
            {
                string sqlAdd = "select * from pos_itm where itm_avbl = 'Yes' and bar_code like '%" + textBox1.Text + "%'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                int top = 10;
                int left = 10;
                int ctr = 0;

                //int paneltop = 10;
                //int panelleft = 10;
                int panelWidth = 157;

                while (read.Read())
                {

                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Top = top;
                    pictureBox1.Left = left;
                    pictureBox1.Name = read["bar_code"].ToString();
                    pictureBox1.Size = new System.Drawing.Size(157, 140);
                    byte[] img = (byte[])(read["itm_image"]);
                    MemoryStream mStream = new MemoryStream(img);
                    pictureBox1.Image = System.Drawing.Image.FromStream(mStream);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Click += new EventHandler(Item_Click);
                    ItemPanel.Controls.Add(pictureBox1);
                    left += pictureBox1.Width + 24;

                    Label label = new Label();
                    label.Text = read["itm_desc"].ToString();
                    label.Width = panelWidth;
                    label.Top = 120;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.BackColor = Color.White;
                    label.ForeColor = Color.Black;
                    pictureBox1.Controls.Add(label);

                    ctr++;
                    if (ctr >= 5)
                    {
                        top += pictureBox1.Height + 30;
                        left = 10;
                        ctr = 0;
                    }

                }


                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ItemPanel.Controls.Clear();
            var button = sender as Button;

            try
            {
                string sqlAdd = "select * from pos_itm where itm_avbl = 'Yes' and itm_desc like '%" + textBox2.Text + "%'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                int top = 10;
                int left = 10;
                int ctr = 0;

                //int paneltop = 10;
                //int panelleft = 10;
                int panelWidth = 157;

                while (read.Read())
                {

                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Top = top;
                    pictureBox1.Left = left;
                    pictureBox1.Name = read["bar_code"].ToString();
                    pictureBox1.Size = new System.Drawing.Size(157, 140);
                    byte[] img = (byte[])(read["itm_image"]);
                    MemoryStream mStream = new MemoryStream(img);
                    pictureBox1.Image = System.Drawing.Image.FromStream(mStream);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Click += new EventHandler(Item_Click);
                    ItemPanel.Controls.Add(pictureBox1);
                    left += pictureBox1.Width + 24;

                    Label label = new Label();
                    label.Text = read["itm_desc"].ToString();
                    label.Width = panelWidth;
                    label.Top = 120;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.BackColor = Color.White;
                    label.ForeColor = Color.Black;
                    pictureBox1.Controls.Add(label);

                    ctr++;
                    if (ctr >= 5)
                    {
                        top += pictureBox1.Height + 30;
                        left = 10;
                        ctr = 0;
                    }

                }


                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm tt");
            TimeTimer.Start();
        }

        public void Item_Click(object sender, EventArgs e)
        {

            var pictureBox1 = sender as PictureBox;

            try
            {
                string sqlAdd = "select * from pos_itm where bar_code = '" + pictureBox1.Name + "'";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ItemPrice = read["itm_price"].ToString();
                    ItemName = read["itm_desc"].ToString();
                    ItemBarcode = read["bar_code"].ToString();
                }


                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }

            frmItemOption ITemOption = new frmItemOption();
            DialogResult result = ITemOption.ShowDialog();
            bool SameItem = false;
            if (result == DialogResult.OK)
            {

                foreach (ListViewItem items in listView1.Items)
                {
                    if (ItemBarcode == items.Text)
                    {
                        double Quantity = Convert.ToDouble(frmItemOption.Quantity) + Convert.ToDouble(items.SubItems[3].Text);
                        double Total = Convert.ToDouble(frmItemOption.Total) + Convert.ToDouble(items.SubItems[4].Text);
                        double Discount = Convert.ToDouble(frmItemOption.Discount) + Convert.ToDouble(items.SubItems[5].Text);

                        items.SubItems[3].Text = Quantity.ToString();
                        items.SubItems[4].Text = Total.ToString("#,##0.00");
                        items.SubItems[5].Text = Discount.ToString();

                        SameItem = true;
                        break;
                    }
                    else
                    {
                        SameItem = false;
                    }
                }

                if (SameItem == false)
                {
                    ListViewItem item = new ListViewItem(ItemBarcode);
                    item.SubItems.Add(ItemName);
                    item.SubItems.Add(ItemPrice);
                    item.SubItems.Add(frmItemOption.Quantity);
                    item.SubItems.Add(frmItemOption.Total);
                    item.SubItems.Add(frmItemOption.Discount);
                    item.SubItems.Add(frmItemOption.Service);
                    listView1.Items.Add(item);
                }


                double TotalAmount = (Convert.ToDouble(txtTotal.Text) + Convert.ToDouble(frmItemOption.Total));
                double SubTotal = (TotalAmount / 1.12);
                double TotalVAT = SubTotal * 0.12;

                txtTotal.Text = TotalAmount.ToString("#,##0.00");
                txtSubtotal.Text = SubTotal.ToString("#,##0.00");
                txtVat.Text = TotalVAT.ToString("#,##0.00");

                btnPayout.Enabled = true;
                btnPayout.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;

            }
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count <= 0)
            {
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to remove this Item ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string PriceRemove = listView1.SelectedItems[0].SubItems[4].Text;
                    double NewTotalAmount = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(PriceRemove);
                    double SubTotal = (NewTotalAmount / 1.12);
                    double TotalVAT = SubTotal * 0.12;

                    txtTotal.Text = NewTotalAmount.ToString("#,##0.00");
                    txtSubtotal.Text = SubTotal.ToString("#,##0.00");
                    txtVat.Text = TotalVAT.ToString("#,##0.00");

                    listView1.SelectedItems[0].Remove();

                    if (listView1.Items.Count <= 0)
                    {
                        btnPayout.Enabled = false;
                        button1.Enabled = false;
                        button2.Enabled = false;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count <= 0)
            {
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to remove this Item ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem eachItem in listView1.SelectedItems)
                    {

                        //string PriceRemove = listView1.SelectedItems[0].SubItems[4].Text;
                        string PriceRemove = eachItem.SubItems[4].Text;
                        double NewTotalAmount = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(PriceRemove);
                        double SubTotal = (NewTotalAmount / 1.12);
                        double TotalVAT = SubTotal * 0.12;

                        txtTotal.Text = NewTotalAmount.ToString("#,##0.00");
                        txtSubtotal.Text = SubTotal.ToString("#,##0.00");
                        txtVat.Text = TotalVAT.ToString("#,##0.00");

                        listView1.Items.Remove(eachItem);
                    }
                    if (listView1.Items.Count <= 0)
                    {
                        btnPayout.Enabled = false;
                        button1.Enabled = false;
                        button2.Enabled = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to clear the order list ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                listView1.Items.Clear();
                txtTotal.Text = "0.00";
                txtVat.Text = "0.00";
                txtSubtotal.Text = "0.00";
                btnPayout.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCstmrSelection cstmr = new frmCstmrSelection();
            DialogResult cstmrForm = cstmr.ShowDialog();
            if (cstmrForm == DialogResult.OK)
            {
                txtCustomerCode.Text = frmCstmrSelection.CstmrCode;
                txtCustomerName.Text = frmCstmrSelection.CstmrName;
            }
        }

        private void txtCustomerCode_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPayout payout = new frmPayout();
            payout.ShowDialog();
        }
    }
}
