using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AviaCoupsSale
{
    
    public partial class ResultsForm : Form
    {
        public static bool may_open_reis_detail = false;
        public static int rInx = 0;
        public static int id_reis_choosed = 0;
        public static string ak = "";
        public static string city_av = "";
        public static string city_ap = "";
        public static int transit_counter = 0;
        public static DateTime DTArrving = DateTime.Now;
        public static DateTime DTDeparture = DateTime.Now;
        public static double fare = 0; //общая стоимость всех билетов
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
                    dataGridView1.Columns[0].HeaderText = "Авиакомпания";
                    dataGridView1.Columns[1].HeaderText = "Город вылета"; 
                    dataGridView1.Columns[2].HeaderText = "Город прилета";
                    dataGridView1.Columns[3].HeaderText = "Кол-во пересадок";
                    dataGridView1.Columns[4].HeaderText = "Дата вылета";
                    dataGridView1.Columns[5].HeaderText = "Дата прибытия";
                    dataGridView1.Columns[6].HeaderText = "id_first_parent_flight";

                    dataGridView1.Columns[6].Visible = false;
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
                id_reis_choosed = Convert.ToInt32(dataGridView1.Rows[rInx].Cells[6].Value.ToString());
                ak = dataGridView1.Rows[rInx].Cells[0].Value.ToString();
                city_av = dataGridView1.Rows[rInx].Cells[1].Value.ToString();
                city_ap = dataGridView1.Rows[rInx].Cells[2].Value.ToString();
                transit_counter = Convert.ToInt32(dataGridView1.Rows[rInx].Cells[3].Value.ToString()); ;
                DTArrving = Convert.ToDateTime(dataGridView1.Rows[rInx].Cells[4].Value.ToString()); 
                DTDeparture = Convert.ToDateTime(dataGridView1.Rows[rInx].Cells[5].Value.ToString());

                //получаем значение базового тарифа:
                try
                {
                    SearchForm.conn.Open();
                    string queryString = "select fare_cost from t_flights where id_flight = @id_reis";
                    queryString = queryString.Replace("@id_reis", id_reis_choosed.ToString());
                    SqlCommand command = new SqlCommand();
                    command.Connection = SearchForm.conn;
                    command.CommandText = queryString;
                    command.CommandType = CommandType.Text;
                    fare = Convert.ToDouble(command.ExecuteScalar().ToString());
                    SearchForm.conn.Close();
                    //transit_counter = Convert.ToInt32(dataGridView1.Rows[rInx].Cells[3].Value.ToString()); ;            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                for (int i = 0; i <= SearchForm.count_pass; i++)
                {
                    PassengerClass adult_passenger = new AdultPassengerClass();
                    if (SearchForm.subs_adult_pass_counter > 0)
                        adult_passenger = new SubsPassenger(adult_passenger);
                    fare = fare + adult_passenger.GetCost() * fare;
                }

                for (int i = 0; i <= SearchForm.count_ch; i++)
                {
                    PassengerClass child_passenger = new ChildrenPassengerClass();
                    if (SearchForm.subs_ch_pass_counter > 0)
                        child_passenger = new SubsPassenger(child_passenger);
                    fare = fare + child_passenger.GetCost() * fare;
                }               

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
