using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using AdminPOS.Properties;


namespace AdminPOS
{
    public partial class frmItemMaintenance : Form
    {
        ServerConnection server = new ServerConnection();
        public string CID = "";
        public string IID = "";
        public string Available1 = "";
        public string Available2 = ""; 
        public bool DeleteCategory = true;
        public string ImageByteRetrive = "";

        public void CategoryRefresh()
        {
            button9.Enabled = false;
            txtCateg.Items.Clear();
            listCategory.Items.Clear();
            try
            {
                string sqlAdd = "select * from pos_categ";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["categ_desc"].ToString());
                    item.SubItems.Add(read["categ_id"].ToString());
                    listCategory.Items.Add(item);
                    txtCateg.Items.Add(read["categ_desc"].ToString());
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        public void ItemRefresh()
        {
            listView1.Items.Clear();
            try
            {
                string sqlAdd = "select i.itm_id, i.bar_code, i.itm_desc, c.categ_desc, i.itm_price, i.itm_avbl from pos_itm i join pos_categ c on i.itm_categ = c.categ_id";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["itm_id"].ToString());
                    item.SubItems.Add(read["bar_code"].ToString());
                    item.SubItems.Add(read["itm_desc"].ToString());
                    item.SubItems.Add(read["categ_desc"].ToString());
                    item.SubItems.Add(read["itm_price"].ToString());
                    item.SubItems.Add(read["itm_avbl"].ToString());

                    listView1.Items.Add(item);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        public frmItemMaintenance()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBarcode.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtRemarks.Clear();
            txtDescription.Clear();
            txtCateg.Text = " ";
            txtAvail.Text = " ";

            lblTitle.Text = "New Item";
            tabControl1.SelectTab("NewItem");
            button5.Text = "Submit Item Details";
        }

        private void btnItemList_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Item List";
            tabControl1.SelectTab("ItemList");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Category List";
            tabControl1.SelectTab("Category");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text != "")
            {
                try
                {
                    string sqlAdd = "insert into pos_categ(categ_desc)values('" + txtCategory.Text + "')";
                    server.Connection();
                    MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                    MySqlDataReader read;
                    server.OpenConnection();
                    read = cmd.ExecuteReader();
                    MessageBox.Show("New Category Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    server.CloseConnection();
                    CategoryRefresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    server.CloseConnection();
                }
            }
            else
            {
                MessageBox.Show("Missing Information", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                server.CloseConnection();
            }
        }

        private void frmItemMaintenance_Load(object sender, EventArgs e)
        {
            CategoryRefresh();
            ItemRefresh();
            button9.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bit = new Bitmap(open.FileName);
                    txtPath.Text = open.FileName.ToString();
                   
                    pictureBox1.Image = bit;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime MyDate = DateTime.Now;
            DateTime DateNow = DateTime.Parse(MyDate.ToString("MM/dd/yyyy"));
            string CategoryID = "";
            //txtDateCreate.Value = DateTime.Now; 

            //Console.WriteLine(txtDateCreate.Value + " " + DateNow.ToString("MM/dd/yyyy"));
            //if (txtDateCreate.Value.Date < DateNow)
            //{
                if (txtBarcode.Text != "" && txtDescription.Text != "" && txtPrice.Text != "" && txtQty.Text != "" && txtCateg.Text != "" && txtAvail.Text != "")
                {
                    if (txtPath.Text == "" || txtPath.Text == " ")
                    {
                        txtPath.Text = Settings.Default["ImagePath"].ToString();
                    }

                    #region Select Category ID

                    try
                    {
                        string sqlAdd = "select * from pos_categ where categ_desc = '"+ txtCateg.Text +"'";
                        server.Connection();
                        MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                        MySqlDataReader read;
                        server.OpenConnection();
                        read = cmd.ExecuteReader();
                        if (read.Read())
                        {
                            CategoryID = read["categ_id"].ToString();
                        }
                        server.CloseConnection();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Category Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        server.CloseConnection();
                    }

                    #endregion


                    #region Insert New Item

                    if (button5.Text == "Submit Item Details")
                    {
                        DialogResult result = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            byte[] imageByte = null;
                            FileStream fsStream = new FileStream(this.txtPath.Text, FileMode.Open, FileAccess.Read);
                            BinaryReader br = new BinaryReader(fsStream);
                            imageByte = br.ReadBytes((int)fsStream.Length);

                            string DirectoryPath = txtPath.Text.Replace("\\", "$");
                            Console.WriteLine(DirectoryPath);

                            try
                            {
                                string sqlAdd = "INSERT INTO pos_itm(" +
                                "bar_code, itm_desc, itm_price, itm_qty, itm_categ, itm_avbl, " +
                                "remarks, delivery_date, image_path, itm_real_date, itm_image) values (" +
                                "'" + txtBarcode.Text + "', " +
                                "'" + txtDescription.Text + "', " +
                                "'" + txtPrice.Text + "', " +
                                "'" + txtQty.Text + "', " +
                                "" + CategoryID + ", " +
                                "'" + txtAvail.Text + "', " +
                                "'" + txtRemarks.Text + "', " +
                                "'" + txtDateCreate.Text + "', " +
                                "'" + DirectoryPath + "', " +
                                "'" + DateNow.ToString("MM/dd/yyyy") + "', @IMG)";
                                server.Connection();
                                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                                MySqlDataReader read;
                                server.OpenConnection();
                                cmd.Parameters.Add(new MySqlParameter("@IMG", imageByte));
                                read = cmd.ExecuteReader();
                                MessageBox.Show("New Item Encoded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                txtBarcode.Clear();
                                txtPrice.Clear();
                                txtQty.Clear();
                                txtRemarks.Clear();
                                txtDescription.Clear();
                                ItemRefresh();
                                server.CloseConnection();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "New Item Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                server.CloseConnection();
                            }
                        }
                    }
                    #endregion


                    #region Update New Item

                    if (button5.Text == "Update Item Details")
                    {
                        DialogResult result = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            byte[] imageByteUpdate = null;
                            FileStream fsStreamUpdate = new FileStream(this.txtPath.Text, FileMode.Open, FileAccess.Read);
                            BinaryReader brUpdate = new BinaryReader(fsStreamUpdate);
                            imageByteUpdate = brUpdate.ReadBytes((int)fsStreamUpdate.Length);

                            string DirectoryPath = txtPath.Text.Replace("\\", "$");
                            Console.WriteLine(DirectoryPath);

                            try
                            {
                                string sqlUpdate = "UPDATE pos_itm set " +
                                "bar_code = '" + txtBarcode.Text + "' , " +
                                "itm_desc = '" + txtDescription.Text + "' , " +
                                "itm_price = '" + txtPrice.Text + "' , " +
                                "itm_qty = '" + txtQty.Text + "' , " +
                                "itm_categ = " + CategoryID + " , " +
                                "itm_avbl = '" + txtAvail.Text + "' , " +
                                "remarks ='" + txtRemarks.Text + "' , " +
                                "delivery_date = '" + txtDateCreate.Text + "', " +
                                "image_path = '" + DirectoryPath + "', " +
                                "itm_image = @IMG, " +
                                "itm_real_date ='" + DateNow.ToString("MM/dd/yyyy") + "'" +
                                "where itm_id = "+ IID +"";
                                server.Connection();
                                MySqlCommand cmd = new MySqlCommand(sqlUpdate, server.con);
                                cmd.Parameters.Add(new MySqlParameter("@IMG", imageByteUpdate));
                                MySqlDataReader read;
                                server.OpenConnection();
                                read = cmd.ExecuteReader();
                                MessageBox.Show("Item updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                txtBarcode.Clear();
                                txtPrice.Clear();
                                txtQty.Clear();
                                txtRemarks.Clear();
                                button5.Text = "Submit Item Details";
                                ItemRefresh();
                                lblTitle.Text = "Item List";
                                tabControl1.SelectTab("ItemList");
                                server.CloseConnection();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "New Item Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                server.CloseConnection();
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("Please review all detail of item.", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            //else
            //{
            //    MessageBox.Show("Delivery Date must not be greater than Date now", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void listCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCategory.SelectedItems.Count == 0) return;
            txtCategory.Text = listCategory.SelectedItems[0].SubItems[0].Text;
            CID = listCategory.SelectedItems[0].SubItems[1].Text;
            button9.Enabled = true;
            listItem.Items.Clear();
            try
            {
                string sqlAdd = "select itm_desc from pos_itm where itm_categ = " + CID + "";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                DeleteCategory = true;
                    while (read.Read())
                    {
                        ListViewItem item = new ListViewItem(read["itm_desc"].ToString());
                        listItem.Items.Add(item);
                        DeleteCategory = false;
                    }

                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Category Error : Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text != "")
            {
                try
                {
                    string sqlAdd = "update pos_categ set categ_desc = '"+ txtCategory.Text +"' where categ_id = "+ CID;
                    server.Connection();
                    MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                    MySqlDataReader read;
                    server.OpenConnection();
                    read = cmd.ExecuteReader();
                    MessageBox.Show("New Category Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    server.CloseConnection();
                    CategoryRefresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    server.CloseConnection();
                }
            }
            else
            {
                MessageBox.Show("Missing Information", "Cannot be update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                server.CloseConnection();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (DeleteCategory == true)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete category " + listCategory.SelectedItems[0].SubItems[0].Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string sqlAdd = "delete from pos_categ where categ_id = " + listCategory.SelectedItems[0].SubItems[1].Text + "";
                        server.Connection();
                        MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                        MySqlDataReader read;
                        server.OpenConnection();
                        read = cmd.ExecuteReader();
                        MessageBox.Show("Category Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        server.CloseConnection();
                        CategoryRefresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        server.CloseConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("There is some item using this category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as Microsoft.Office.Interop.Excel.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as Microsoft.Office.Interop.Excel.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void updateItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlAdd = "select i.*, c.* from pos_itm i join pos_categ c on i.itm_categ = c.categ_id where i.itm_id = " + IID + "";
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlAdd, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    txtBarcode.Text = read["bar_code"].ToString();
                    txtDescription.Text = read["itm_desc"].ToString();
                    txtCateg.Text = read["categ_desc"].ToString();
                    txtRemarks.Text = read["remarks"].ToString();
                    txtPrice.Text = read["itm_price"].ToString();
                    txtQty.Text = read["itm_qty"].ToString();
                    txtDateCreate.Text = read["delivery_date"].ToString();
                    txtAvail.Text = read["itm_avbl"].ToString();
                    CID = read["categ_id"].ToString();

                    txtPath.Text = read["image_path"].ToString().Replace("$", "\\");

                    byte[] img = (byte[])(read["itm_image"]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream mStream = new MemoryStream(img);
                        pictureBox1.Image = System.Drawing.Image.FromStream(mStream);
                    }
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }

            button5.Text = "Update Item Details";
            lblTitle.Text = "New Item";
            tabControl1.SelectTab("NewItem");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            IID = listView1.SelectedItems[0].SubItems[0].Text;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            #region Checking The Availability of Item

            Available1 = ""; Available2 = "";
            if (checkBox1.Checked && checkBox2.Checked)
            {
                Available1 = ""; Available2 = "";
            }
            else
            {
                if (checkBox1.Checked)
                {
                    Available1 = "YES"; Available2 = "";
                }

                if (checkBox2.Checked)
                {
                    Available1 = ""; Available2 = "NO";
                }
            }
            #endregion

            listView1.Items.Clear();
            try
            {
                string sqlSearch = "select i.itm_id, i.bar_code, i.itm_desc, c.categ_desc, i.itm_price, i.itm_avbl from pos_itm i join pos_categ c on i.itm_categ = c.categ_id where bar_code like '%" + txtBarCodeFilter.Text + "%'" +
                                "and itm_desc like '%"+ txtItemDescFilter.Text +"%'" + 
                                "and categ_desc like '%"+ txtCategoryFilter.Text +"%'" +
                                "and itm_avbl like '%"+ Available1 +"%'" +
                                "and itm_avbl like '%" + Available2 + "%'";
                Console.WriteLine(sqlSearch);
                server.Connection();
                MySqlCommand cmd = new MySqlCommand(sqlSearch, server.con);
                MySqlDataReader read;
                server.OpenConnection();
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    ListViewItem item = new ListViewItem(read["itm_id"].ToString());
                    item.SubItems.Add(read["bar_code"].ToString());
                    item.SubItems.Add(read["itm_desc"].ToString());
                    item.SubItems.Add(read["categ_desc"].ToString());
                    item.SubItems.Add(read["itm_price"].ToString());
                    item.SubItems.Add(read["itm_avbl"].ToString());

                    listView1.Items.Add(item);
                }
                server.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            ItemRefresh();
            txtCategoryFilter.Text = "";
            txtItemDescFilter.Text = "";
            txtBarCodeFilter.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    app.Visible = true;
                    ws.Cells[1, 1] = "No.";
                    ws.Cells[1, 2] = "Barcode";
                    ws.Cells[1, 3] = "Item Description";
                    ws.Cells[1, 4] = "Category";
                    ws.Cells[1, 5] = "Price";
                    ws.Cells[1, 6] = "Available";
                    int i = 3;
                    foreach (ListViewItem item in listView1.Items)
                    {
                        ws.Cells[i, 1] = item.SubItems[0].Text;
                        ws.Cells[i, 2] = item.SubItems[1].Text;
                        ws.Cells[i, 3] = item.SubItems[2].Text;
                        ws.Cells[i, 4] = item.SubItems[3].Text;
                        ws.Cells[i, 5] = item.SubItems[4].Text;
                        ws.Cells[i, 6] = item.SubItems[5].Text;
                        i++;
                    }
                    wb.SaveAs(save.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Exporting Data Success");
                }
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
