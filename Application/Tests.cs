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
    public partial class Tests : Form
    {
        public Tests()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Add_Test c = new Add_Test();
            panel2.Controls.Add(c);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Show_Test c = new Show_Test();
            panel2.Controls.Add(c);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Modify_Test c = new Modify_Test();
            panel2.Controls.Add(c);
        }

        private void Tests_Load(object sender, EventArgs e)
        {

        }
    }
}
