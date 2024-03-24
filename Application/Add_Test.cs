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
    public partial class Add_Test : UserControl
    {
        public Add_Test()
        {
            InitializeComponent();
        }
        SqlConnection conn;

        private void load_questions()
        {
            string q = "SELECT * FROM QUESTIONS";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            checkedListBox1.Items.Clear();
            while (dr.Read())
                checkedListBox1.Items.Add(dr[0].ToString() + " - " + dr[2].ToString());
            dr.Close();
            string q1 = "SELECT * FROM CATEGORY";
            SqlCommand c1 = new SqlCommand(q1, conn);
            SqlDataReader dr1 = c1.ExecuteReader();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All possible questions in the galaxy-ies actually");          
            while (dr1.Read())
            {
                comboBox1.Items.Add(dr1[0].ToString() + " - " + dr1[1].ToString());
            }
            dr1.Close();
            comboBox1.SelectedIndex = 0;
        }

        
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = checkedListBox1.SelectedItem.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            int nrquestions = checkedListBox1.CheckedItems.Count;
            if (nrquestions != 0 && title != "")
            {
                string id_q = "";
                foreach (string s in checkedListBox1.CheckedItems)
                {
                    string[] S = s.Split();
                    id_q = id_q + S[0] + " ";
                }
                string q = "INSERT INTO TESTS (TITLE, QUESTIONS) VALUES ";
                q = q + "('" + title + "', '" + id_q + "')";
                SqlCommand c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("The test has been saved");
            }
            else MessageBox.Show("You haven't filled in all required data! Come back!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] text = comboBox1.Text.Split();
            if (text[0] == "All")
            {
                string q = "SELECT * FROM QUESTIONS ORDER BY ID";
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataReader dr = c.ExecuteReader();
                checkedListBox1.Items.Clear();
                while (dr.Read())
                    checkedListBox1.Items.Add(dr[0].ToString() + " - " + dr[2].ToString());
                dr.Close();
            }
            else
            {
                int id = int.Parse(text[0]);
                string q1 = "SELECT * FROM QUESTIONS WHERE ID_CATEGORY=" + id + " ORDER BY ID";
                SqlCommand c1 = new SqlCommand(q1, conn);
                SqlDataReader dr1 = c1.ExecuteReader();
                checkedListBox1.Items.Clear();
                while (dr1.Read())
                    checkedListBox1.Items.Add(dr1[0].ToString() + " - " + dr1[2].ToString());
                dr1.Close();
            }
        }

        private void Add_Test_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            load_questions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string title = textBox2.Text;
            int nrquestions= 10;
            if (title != "")//date corecte
            {
                string id_q = "";
                string qr = "SELECT TOP " + nrquestions + " ID FROM QUESTIONS ORDER BY NEWID()";
                SqlCommand cr = new SqlCommand(qr, conn);
                SqlDataReader dr = cr.ExecuteReader();
                while (dr.Read())
                    id_q = id_q + dr[0].ToString() + " ";
                dr.Close();
                string q = "INSERT INTO TESTS (TITLE, QUESTIONS) VALUES ";
                q = q + "('" + title + "', '" + id_q + "')";
                SqlCommand c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("The tests has been saved!");
            }
            else MessageBox.Show("Let me tempt you to a spot of lunch but first, fill in all the data needed");
        }
    }
}
