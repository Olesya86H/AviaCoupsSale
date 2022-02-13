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
        public static DataTable dt_flights = new DataTable();
        public static DataTable dt_children = new DataTable();
        public static string city1 = "";
        public static string city2 = "";
        public static bool reverse_route = false;
        public static int count_pass = 1;
        public static int count_ch = 0;
        public static int age_ch1 = 0;
        public static int age_ch2 = 0;
        public SearchForm()
        {
            InitializeComponent();
            dt_children.Clear();
            try
            {
                for (int i = 0; i <= 6; i++)
                    dt_children.Columns.RemoveAt(0);
            }
            catch { }
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int errors = 0; //счетчик ошибок

            //проверка, нужен ли поиск обратного билета:
            if (radioButton1.Checked == true)
                reverse_route = false;
            if (radioButton2.Checked == true)
                reverse_route = true;

            //провека, задан ли маршрут:
            if ((comboBox1.Text == "") || (comboBox2.Text == ""))
                errors++;

            //проверка, целое ли число задано в кол-ве взрослых пассажиров:
            try
            {
                count_pass = Convert.ToInt32(textBox1.Text);
            }
            catch 
            {
                errors++;
            }

            //очистка таблицы по детям, когда поездка без детей:
            if (checkBox1.Checked == false)
            {
                dt_children.Clear();
                try
                {
                    for (int i = 0; i <= 6; i++)
                        dt_children.Columns.RemoveAt(0);
                }
                catch { }
            }
            else
            {
                try
                {
                    age_ch1 = Convert.ToInt32(comboBox3.Text);
                    age_ch2 = Convert.ToInt32(comboBox4.Text);
                }
                catch 
                {
                    errors++;
                }
            }


            city1 = comboBox1.Text; //город вылета
            city2 = comboBox2.Text; //город прилета
            dt_flights.Clear(); //очистка таблицы полетов от предыдущих возможных данных
            try
            {
                for (int i = 0; i <= 6; i++)
                    dt_flights.Columns.RemoveAt(0);
            }
            catch { }

            if (dt_flights.Columns.Count == 0)
            {
                for (int i = 0; i <= 6; i++)
                    dt_flights.Columns.Add();
            }

            if (errors == 0) //когда на форме нет ошибок - отправляем запрос в БД:
            {
                try
                {
                    conn.Open();
                    string queryString =
                        @"select distinct case when (select count (tff.ID_AK) from T_Flights tff where tff.ID_First_Parent_Flight = tf.ID_First_Parent_Flight) > 1 
			                  then  stuff((select ','+ sa.Name_AK from s_ak sa where sa.ID_AK = tf.ID_AK group by sa.name_ak for xml path(''), type).value('.','VARCHAR(max)'), 1, 1, '')
			                  else ak.Name_AK
			                  end as Авиакомпания, 
	                     @city1 as Город_вылета, 
	                     @city2 as Город_прилета,
	                     (select count(id_flight) - 1
	                      from t_flights tff 
		                  where tff.ID_First_Parent_Flight = tf.ID_First_Parent_Flight) as кол_во_пересадок,	                     
	                     Date_Departure, Date_Arriving, tf.id_first_parent_flight	   
                  from O_AK_Route o, S_AK ak, T_Flights tf, t_routes rt, S_Cities sc
                  where o.ID_AK = ak.ID_AK
                  and tf.ID_AK = o.ID_AK
                  and tf.id_route= rt.ID_route
                  and o.ID_Route = rt.ID_route
                  and rt.ID_City_1 = (select sc.ID_City from S_Cities sc where sc.Name_City = @city1)
                  and rt.ID_City_2 = (select sc.ID_City from S_Cities sc where sc.Name_City = @city2)";
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = queryString;
                    command.CommandType = CommandType.Text;
                    SqlParameter parameter_city1 = new SqlParameter();
                    parameter_city1.ParameterName = "@city1";
                    parameter_city1.SqlDbType = SqlDbType.NVarChar;
                    parameter_city1.Direction = ParameterDirection.Input;
                    parameter_city1.Value = city1;
                    command.Parameters.Add(parameter_city1);
                    SqlParameter parameter_city2 = new SqlParameter();
                    parameter_city2.ParameterName = "@city2";
                    parameter_city2.SqlDbType = SqlDbType.NVarChar;
                    parameter_city2.Direction = ParameterDirection.Input;
                    parameter_city2.Value = city2;
                    command.Parameters.Add(parameter_city2);
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read()) //заполняем таблицу dt_flights результатами запроса:
                    {
                        DataRow dr = dt_flights.NewRow(); 
                        dr[0] = rdr.GetValue(0);
                        dr[1] = rdr.GetValue(1);
                        dr[2] = rdr.GetValue(2);
                        dr[3] = rdr.GetValue(3);
                        dr[4] = rdr.GetValue(4);
                        dr[5] = rdr.GetValue(5);
                        dr[6] = rdr.GetValue(6);
                        dt_flights.Rows.Add(dr);
                    }
                    conn.Close();

                    //показываем результаты запроса в форме с рейсами:
                    ResultsForm FlightsChoosing = new ResultsForm();
                    FlightsChoosing.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("Проверьте, пожалуйста, задание параметров на корректность!");
        }
              
        //command.ExecuteNonQuery();

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //очистка таблицы по детям:
            dt_children.Clear();
            try
            {
                for (int i = 0; i <= 6; i++)
                    dt_children.Columns.RemoveAt(0);
            }
            catch { }

            //очистка таблицы полётов:
            dt_flights.Clear();
            try
            {
                for (int i = 0; i <= 6; i++)
                    dt_flights.Columns.RemoveAt(0);
            }
            catch { }

            comboBox1.Text = "";
            comboBox2.Text = "";

            textBox1.Text = "";
            textBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

            checkBox1.Checked = false;
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
            if (checkBox1.Checked == false)
                groupBox2.Visible = false;
            else
                groupBox2.Visible = true;
		}

		private void button3_Click(object sender, EventArgs e)
		{
            count_ch = dt_children.Rows.Count + 2;
            ChildrenAgesForm ChildrenForm = new ChildrenAgesForm();
            ChildrenForm.Show();
		}

		private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
		{
            try
            {
                if ((e.KeyChar.ToString() == "1") && (textBox2.Text == "1"))
                {
                    comboBox3.Visible = true;
                    label5.Visible = true;
                    comboBox4.Visible = false;
                    label6.Visible = false;
                }
                if (((e.KeyChar.ToString() == "2") && (textBox2.Text == "2")) || (Convert.ToInt32(textBox2.Text) > 2))
                {
                    comboBox3.Visible = true;
                    label5.Visible = true;
                    comboBox4.Visible = true;
                    label6.Visible = true;
                }
            }
            catch { }
        }
	}
}
