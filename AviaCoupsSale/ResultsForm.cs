using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AviaCoupsSale
{
    
    public partial class ResultsForm : Form
    {
        public static bool may_open_reis_detail = false;
        public static int rInx = 0;
        public static int id_reis = 0;
        public static string ak = "";
        public static string city_av = "";
        public static string city_ap = "";
        public static int transit_counter = 0;
        public static DateTime DTArrving = DateTime.Now;
        public static DateTime DTDeparture = DateTime.Now;
        public ResultsForm()
        {
            InitializeComponent();
        }

		private void ResultsForm_Load(object sender, EventArgs e)
		{
            for (int i = 0; i <= SearchForm.dt_flights.Rows.Count - 1; i++)
            {
                //получаем ссылку на эту форму
                Form f = (Form)sender;
                //Создаем новую панель
                // Panel temp = new Panel();
                // temp.Text = "Здесь будет информация о рейсе";
                // temp.Width = f.Width - 20;
                // temp.Height = 30;
                // temp.Location = new Point(f.Location.X + 10, f.Location.Y);                
                //Добавляем элемент на форму
                // this.Controls.Add(temp);               
                try
                {                    
                    if (SearchForm.dt_flights.Rows.Count >= 0)
                    {
                        dataGridView1.DataSource = SearchForm.dt_flights;                       
                    }
                }
                catch { }

            }
		}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
            rInx = e.RowIndex;
            may_open_reis_detail = true;
            button1_Click(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                if (dataGridView1.Rows[i].Selected == true)
                {
                    may_open_reis_detail = true;
                    break;
                }
                else
                    may_open_reis_detail = false;

            if (may_open_reis_detail == true) 
            {
                id_reis = Convert.ToInt32(dataGridView1.Rows[rInx].Cells[6].Value.ToString());
                ak = dataGridView1.Rows[rInx].Cells[0].Value.ToString();
                city_av = dataGridView1.Rows[rInx].Cells[1].Value.ToString();
                city_ap = dataGridView1.Rows[rInx].Cells[2].Value.ToString();
                transit_counter = Convert.ToInt32(dataGridView1.Rows[rInx].Cells[3].Value.ToString()); ;
                DTArrving = Convert.ToDateTime(dataGridView1.Rows[rInx].Cells[4].Value.ToString()); 
                DTDeparture = Convert.ToDateTime(dataGridView1.Rows[rInx].Cells[5].Value.ToString());
                ReisDetails ReisDetailsForm = new ReisDetails();
                ReisDetailsForm.Show();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Selected == true)
            {
                may_open_reis_detail = true;
                rInx = e.RowIndex;
            }
        }
    }
}
