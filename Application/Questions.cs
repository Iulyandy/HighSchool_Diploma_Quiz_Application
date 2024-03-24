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
    public partial class Questions : Form
    {
        public Questions()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Add_Question i = new Add_Question();
            panel2.Controls.Add(i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Show_Question c1 = new Show_Question();
            panel2.Controls.Add(c1);
        }

       private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Modify_Question c1 = new Modify_Question();
            panel2.Controls.Add(c1);
          
        }
    }
}
