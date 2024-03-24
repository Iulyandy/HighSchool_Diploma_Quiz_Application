using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }
        public int idu;
        public int type;
        SqlConnection conn;
        string name_by_id(int id)
        {
            string q = "SELECT USER_NAME FROM USERS WHERE ID=" + id;
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
                return dr[0].ToString();
            return "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Results_Shown(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            if (type == 1)
            {
                string q;
                if (idu != 0)
                    q = "SELECT SCORE,TEST_ID, RANK FROM STANDARD_RESULTS WHERE USER_ID=" + idu;
                else
                    q = "SELECT * FROM  STANDARD_RESULTS";
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataAdapter ok = new SqlDataAdapter(c);
                DataTable t = new DataTable();
                ok.Fill(t);
                dataGridView1.DataSource = t;
            }
            else
            {
                string q;
                if (idu != 0)
                    q = "SELECT NUMBER_OF_QUESTIONS, SCORE FROM RANDOM_RESULTS WHERE USER_ID=" + idu;
                else
                    q = "SELECT * FROM RANDOM_RESULTS";
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataAdapter ok = new SqlDataAdapter(c);
                DataTable t = new DataTable();
                ok.Fill(t);
                dataGridView1.DataSource = t;
            }
        }

        private void Results_Load(object sender, EventArgs e)
        {

        }
    }
}