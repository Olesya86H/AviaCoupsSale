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
    public partial class ChildrenAgesForm : Form
    {
        public static int counter_added_children = 2;
        public ChildrenAgesForm()
        {
            InitializeComponent();
        }

		private void ChildrenAgesForm_Load(object sender, EventArgs e)
		{           
            label1.Text = "Возраст " + SearchForm.count_ch.ToString() + "-го ребёнка:";
		}

		private void button1_Click(object sender, EventArgs e)
		{
            try
            {
                if (SearchForm.dt_children.Columns.Count == 0)
                        for (int i = 0; i <= 6; i++)
                            SearchForm.dt_children.Columns.Add();
                    if (SearchForm.dt_children.Rows.Count == 0)
                    {
                        DataRow dr0 = SearchForm.dt_children.NewRow();
                        dr0[0] = SearchForm.age_ch1;
                        dr0[1] = "Фамилия ребенка загрузится при бронировании";
                        dr0[2] = "Имя ребенка загрузится при бронировании";
                        dr0[3] = "Отчество ребенка загрузится при бронировании";
                        dr0[4] = "Дата рождения ребенка загрузится при бронировании";
                        dr0[5] = "Данные св-ва о рождении ребенка загрузятся при бронировании";
                        dr0[6] = "Паспортные данные сопровождающего ребенка загрузятся при бронировании";
                        SearchForm.dt_children.Rows.Add(dr0);

                        DataRow dr1 = SearchForm.dt_children.NewRow();
                        dr1[0] = SearchForm.age_ch2;
                        dr1[1] = "Фамилия ребенка загрузится при бронировании";
                        dr1[2] = "Имя ребенка загрузится при бронировании";
                        dr1[3] = "Отчество ребенка загрузится при бронировании";
                        dr1[4] = "Дата рождения ребенка загрузится при бронировании";
                        dr1[5] = "Данные св-ва о рождении ребенка загрузятся при бронировании";
                        dr1[6] = "Паспортные данные сопровождающего ребенка загрузятся при бронировании";
                        SearchForm.dt_children.Rows.Add(dr1);
                    }
                    DataRow dr = SearchForm.dt_children.NewRow();
                    dr[0] = Convert.ToInt32(comboBox1.Text);
                    dr[1] = "Фамилия ребенка загрузится при бронировании";
                    dr[2] = "Имя ребенка загрузится при бронировании";
                    dr[3] = "Отчество ребенка загрузится при бронировании";
                    dr[4] = "Дата рождения ребенка загрузится при бронировании";
                    dr[5] = "Данные св-ва о рождении ребенка загрузятся при бронировании";
                    dr[6] = "Паспортные данные сопровождающего ребенка загрузятся при бронировании";
                    SearchForm.dt_children.Rows.Add(dr);                   
                    this.Close();
            }
            catch
            {
                MessageBox.Show("Проверьте, пожалуйста, на корректность введенные данные!");
            }
		}
	}
}
