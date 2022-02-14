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
    public partial class ReisDetails : Form
    {
        public double fare = 0;
        public ReisDetails()
        {
            InitializeComponent();
        }

        private void ReisDetails_Load(object sender, EventArgs e)
        {
            textBox7.Text = ResultsForm.ak;
            textBox1.Text = ResultsForm.city_av;
            textBox2.Text = ResultsForm.city_ap;
            textBox3.Text = ResultsForm.DTArrving.ToString();
            textBox4.Text = ResultsForm.DTDeparture.ToString();

            try
            {
                SearchForm.conn.Open();
                string queryString = "select fare_cost from t_flights where id_flight = @id_reis";
                SqlCommand command = new SqlCommand();
                command.Connection = SearchForm.conn;
                command.CommandText = queryString;
                command.CommandType = CommandType.Text;
                SqlParameter id_reis = new SqlParameter();
                id_reis.ParameterName = "@id_reis";
                id_reis.SqlDbType = SqlDbType.Int;
                id_reis.Direction = ParameterDirection.Input;
                id_reis.Value = ResultsForm.id_reis;
                fare = Convert.ToDouble(command.ExecuteScalar().ToString());
                SearchForm.conn.Close();
                //transit_counter = Convert.ToInt32(dataGridView1.Rows[rInx].Cells[3].Value.ToString()); ;            
            }
            catch { }

            fare = fare * SearchForm.count_pass + fare * SearchForm.count_ch * 0.7 + 2000 * (SearchForm.count_pass + SearchForm.count_ch);
            textBox5.Text = fare.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox6.Text.Trim() != "") && (ResultsForm.id_reis != null) && (ResultsForm.id_reis != 0))
                MessageBox.Show("Сейчас Вы будете направлены на страницу оплаты!", "Подтверждение бронирования", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                if (textBox6.Text.Trim() == "")
                    MessageBox.Show("Задайте адрес электронной почты для отправки билетов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
