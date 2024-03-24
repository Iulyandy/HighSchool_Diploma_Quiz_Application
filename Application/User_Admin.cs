using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Atestat
{
    public partial class User_Admin : Form
    {
        public User_Admin()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }
        int user_id;
        SqlConnection conn;
        private void User_Admin_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            string q = "SELECT * FROM USERS ORDER BY ID";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataAdapter ok = new SqlDataAdapter(c);
            DataTable t = new DataTable();
            ok.Fill(t);
            dataGridView1.DataSource = t;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            user_id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Someone's been naughty, are you sure you to punish them by erasing their account?", "That could teach them a lesson!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string q = "DELETE FROM USERS WHERE ID=" + user_id;
                SqlCommand c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();

                q = "DELETE FROM STANDARD_RESULTS WHERE USER_ID=" + user_id;
                c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();

                q = "DELETE FROM RANDOM_RESULTS WHERE USER_ID=" + user_id;
                c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();

                q = "SELECT * FROM USERS ORDER BY ID";
                c = new SqlCommand(q, conn);
                SqlDataAdapter ok = new SqlDataAdapter(c);
                DataTable t = new DataTable();
                ok.Fill(t);
                dataGridView1.DataSource = t;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        } 
    }
}
