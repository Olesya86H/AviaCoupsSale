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
                Panel temp = new Panel();
                temp.Text = "Здесь будет информация о рейсе";
                temp.Width = f.Width - 20;
                temp.Height = 30;
                temp.Location = new Point(f.Location.X + 10, f.Location.Y);                
                //Добавляем элемент на форму
                this.Controls.Add(temp);
            }
		}
	}
}
