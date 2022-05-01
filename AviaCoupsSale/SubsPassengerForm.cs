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
    public partial class SubsPassengerForm : Form
    {
        public SubsPassengerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SearchForm.subs_adult_pass_counter = Convert.ToInt32(textBox1.Text);
            }
            catch 
            {
                SearchForm.subs_adult_pass_counter = 0;
            }
            try
            {
                SearchForm.subs_ch_pass_counter = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                SearchForm.subs_ch_pass_counter = 0;
            }

            SearchForm.result_form_show();
            
            this.Close();
        }
    }
}
