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
    public partial class Show_Question : UserControl
    {
        public Show_Question()
        {
            InitializeComponent();
        }
        SqlConnection conn;

        private void Show_Question_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            string q = "SELECT * FROM QUESTIONS ORDER BY ID";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataAdapter ok = new SqlDataAdapter(c);
            DataTable t = new DataTable();
            ok.Fill(t);
            dataGridView1.DataSource = t;
            string q1 = "SELECT * FROM CATEGORY";
            SqlCommand c1 = new SqlCommand(q1, conn);
            SqlDataReader dr = c1.ExecuteReader();           
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All questions in the galaxy-ies actually");
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] text = comboBox1.Text.Split();
            if (text[0]=="All")
            {
                string q = "SELECT * FROM QUESTIONS ORDER BY ID";
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataAdapter da = new SqlDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
            else
            {
                int id = int.Parse(text[0]);
                string q = "SELECT * FROM QUESTIONS WHERE ID_CATEGORY=" + id + " ORDER BY ID";
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataAdapter da = new SqlDataAdapter(c);
                DataTable t = new DataTable();
                da.Fill(t);
                dataGridView1.DataSource = t;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
