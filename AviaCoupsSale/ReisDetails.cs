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
            textBox5.Text = Math.Round(ResultsForm.fare,2).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox6.Text.Trim() != "") && (ResultsForm.id_reis_choosed != null) && (ResultsForm.id_reis_choosed != 0))
                MessageBox.Show("Сейчас Вы будете направлены на страницу оплаты!", "Подтверждение бронирования", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                if (textBox6.Text.Trim() == "")
                    MessageBox.Show("Задайте адрес электронной почты для отправки билетов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
