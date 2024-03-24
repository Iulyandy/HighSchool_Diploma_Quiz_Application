using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat
{
    public partial class Welcome : Form
    {
        public int id_user, standard_tests, random_tests;
        public string fandom;
        public Welcome()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;      
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            if (fandom == "ST")
            {
                tabPage3.Text = "Databank";
                ///richTextBox6.Hide();
                pictureBox14.Hide();
               /* pictureBox3.Hide();
                pictureBox4.Hide();
                pictureBox5.Hide();
                pictureBox6.Hide();
                pictureBox7.Hide();*/
            }
            else if (fandom == "SW")
            {
                tabPage3.Text = "Holocron";
              //  richTextBox7.Hide();
                pictureBox13.Hide();
               /* pictureBox8.Hide();
                pictureBox9.Hide();
                pictureBox10.Hide();
                pictureBox11.Hide();
                pictureBox12.Hide();*/
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Standard_Tests form = new Standard_Tests();
            form.user_id = id_user;
            form.fandom = fandom;
            //this.Hide();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random_Tests form = new Random_Tests();
            form.user_id = id_user;
            form.fandom = fandom;
            //this.Hide();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Results r = new Results();
            r.idu = id_user;
            r.type = 1;
            r.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Results r = new Results();
            r.idu = id_user;
            r.type = 2;
            r.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
