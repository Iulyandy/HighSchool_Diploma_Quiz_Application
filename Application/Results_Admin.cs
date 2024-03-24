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
    public partial class Results_Admin : Form
    {
        public Results_Admin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Results r = new Results();
            r.idu = 0;
            r.type = 1;
            r.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Results r = new Results();
            r.idu = 0;
            r.type = 2;
            r.ShowDialog();
        }
    }
}
