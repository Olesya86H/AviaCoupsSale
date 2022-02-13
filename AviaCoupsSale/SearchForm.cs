using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AviaCoupsSale
{
    public partial class SearchForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=AISHA\SQLEXPRESS;Initial Catalog=AviaCoupSale;User ID=AviaCoupSale;Password=Avia");
        public SearchForm()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string queryString = "select name_flight from t_flights where ";
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = queryString;
            command.CommandType = CommandType.Text;
            SqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                rdr.GetValue(0);
            }
            conn.Close();
        }

        /*SqlParameter parameter = new SqlParameter();
           parameter.ParameterName = "@indexNbr";
           parameter.SqlDbType = SqlDbType.NVarChar;
           parameter.Direction = ParameterDirection.Input;
           parameter.Value = indexNbr;*/
        //command.ExecuteNonQuery();

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            checkBox1.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
    }
}
