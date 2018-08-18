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
    public partial class frmItemOption : Form
    {

        ServerConnection server = new ServerConnection();
        double ServiceCharge = 0;
        public bool QuantityFocus = true;
        public bool DiscountFocus = false;

        //Data to be displayed in Order List
        public static string Quantity;
        public static string Discount;
        public static string Total;
        public static string Service;

        public frmItemOption()
        {
            InitializeComponent();
        }

        private void frmItemOption_Load(object sender, EventArgs e)
        {
            lblItemName.Text = frmPOS.ItemName;
            lblUnitPrice.Text = Convert.ToDouble(frmPOS.ItemPrice).ToString("#,##0.00");
            lblTotal.Text = Convert.ToDouble(lblUnitPrice.Text).ToString("#,##0.00");

            rdbtnTakeOut.Checked = true;
            Service = "Take Out";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblQuantity_TextChanged(object sender, EventArgs e)
        {
            if (lblQuantity.Text == "" || lblQuantity.Text == ".")
            {
                lblTotal.Text = Convert.ToDouble(lblUnitPrice.Text).ToString("#,##0.00");
                return;
            }

            lblTotal.Text = ((Convert.ToDouble(lblUnitPrice.Text) * Convert.ToDouble(lblQuantity.Text)) + ServiceCharge).ToString("#,##0.00");
        }

        private void lblQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double OriginalAmount = Convert.ToDouble(lblUnitPrice.Text) * Convert.ToDouble(lblQuantity.Text);

            if (!cmb100.Checked)
            {
                if (lblDiscount.TextLength > 2)
                {
                    MessageBox.Show("invalid discount.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //string Character = lblDiscount.Text;
                    //lblDiscount.Text = Character.Substring(0, Character.Length - 1);
                    lblDiscount.Text = "0";
                    lblTotal.Text = OriginalAmount.ToString("#,##0.00");
                    return;
                }

                if (lblDiscount.Text != "")
                {
                    if (!lblDiscount.Text.Contains("."))
                    {
                        double Discount = Convert.ToDouble(lblDiscount.Text) / 100;
                        double DiscountDeduct = Convert.ToDouble(OriginalAmount) * Discount;
                        lblTotal.Text = (Convert.ToDouble(OriginalAmount) - DiscountDeduct).ToString("#,##0.00");
                    }
                    else
                    {
                        if (lblDiscount.Text != ".")
                        {
                            double Discount = Convert.ToDouble(lblDiscount.Text);
                            double DiscountDeduct = Convert.ToDouble(OriginalAmount) * Discount;
                            lblTotal.Text = (Convert.ToDouble(OriginalAmount) - DiscountDeduct).ToString("#,##0.00");
                        }
                    }
                }
            }
            else
            {
                lblTotal.Text = (Convert.ToDouble(OriginalAmount) - Convert.ToDouble(lblDiscount.Text)).ToString("#,##0.00");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            lblQuantity.Focus();
            QuantityFocus = true;
            DiscountFocus = false;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (QuantityFocus == true)
            {
                lblQuantity.Text += btn.Text;
            }

            if (DiscountFocus == true)
            {
                lblDiscount.Text += btn.Text;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            lblDiscount.Focus();
            QuantityFocus = false;
            DiscountFocus = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (QuantityFocus == true)
            {
                lblQuantity.Text = "0";
            }

            if (DiscountFocus == true)
            {
                lblDiscount.Text = "0";

            }
        }

        private void lblQuantity_Click(object sender, EventArgs e)
        {
            lblQuantity.Focus();
            QuantityFocus = true;
            DiscountFocus = false;
        }

        private void lblDiscount_Click(object sender, EventArgs e)
        {
            lblDiscount.Focus();
            QuantityFocus = false;
            DiscountFocus = true;
        }

        private void cmb100_CheckedChanged(object sender, EventArgs e)
        {
            double OriginalAmount = Convert.ToDouble(lblUnitPrice.Text) * Convert.ToDouble(lblQuantity.Text);

            if (cmb100.Checked == true && lblDiscount.Text != "")
            {
                lblTotal.Text = (OriginalAmount - Convert.ToDouble(lblDiscount.Text)).ToString("#,##0.00");
            }
            else
            {
                if (lblDiscount.TextLength > 2)
                {
                    MessageBox.Show("invalid discount.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //string Character = lblDiscount.Text;
                    //lblDiscount.Text = Character.Substring(0, Character.Length - 1);
                    lblDiscount.Text = "0";
                    lblTotal.Text = OriginalAmount.ToString("#,##0.00");
                    return;
                }

                if (lblDiscount.Text != "")
                {
                    if (!lblDiscount.Text.Contains("."))
                    {
                        double Discount = Convert.ToDouble(lblDiscount.Text) / 100;
                        double DiscountDeduct = Convert.ToDouble(OriginalAmount) * Discount;
                        lblTotal.Text = (Convert.ToDouble(OriginalAmount) - DiscountDeduct).ToString("#,##0.00");
                    }
                    else
                    {
                        if (lblDiscount.Text != ".")
                        {
                            double Discount = Convert.ToDouble(lblDiscount.Text);
                            double DiscountDeduct = Convert.ToDouble(OriginalAmount) * Discount;
                            lblTotal.Text = (Convert.ToDouble(OriginalAmount) - DiscountDeduct).ToString("#,##0.00");
                        }
                    }
                }
            }
        }

        private void rdbtnDineIn_Click(object sender, EventArgs e)
        {
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
                    ServiceCharge = Convert.ToDouble(read["srvc_chrge"]);
                }
                server.CloseConnection();

                double percentCharge = ServiceCharge / 100;
                ServiceCharge = Convert.ToDouble(lblTotal.Text) * percentCharge;
                lblTotal.Text = ((Convert.ToDouble(lblTotal.Text)) + ServiceCharge).ToString("#,##0.00");
                Service = "Dine In";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Administrator or POS provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.CloseConnection();
            }
        }

        private void rdbtnTakeOut_Click(object sender, EventArgs e)
        {
            lblTotal.Text = (Convert.ToDouble(lblTotal.Text) - ServiceCharge).ToString("#,##0.00");
            ServiceCharge = 0;

            double OriginalAmount = Convert.ToDouble(lblUnitPrice.Text) * Convert.ToDouble(lblQuantity.Text);

            Service = "Take Out";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            lblDiscount.Text = "0";
            lblQuantity.Text = "1";
            ServiceCharge = 0;
            rdbtnTakeOut.Checked = true;
            lblTotal.Text = lblUnitPrice.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Quantity = lblQuantity.Text;
            Discount = lblDiscount.Text;
            Total = lblTotal.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
