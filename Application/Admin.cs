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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Questions form = new Questions();
            form.ShowDialog();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tests form = new Tests();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Results_Admin form = new Results_Admin();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            User_Admin form = new User_Admin();
            form.ShowDialog();
        }
    }
}
