using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Atestat
{
    public partial class Modify_Test : UserControl
    {
        public Modify_Test()
        {
            InitializeComponent();
        }
        SqlConnection conn;

        private void Load_Tests()
        {
            string q = "SELECT * FROM TESTS ORDER BY ID";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] S_ind = listBox1.SelectedItem.ToString().Split();
            int id = int.Parse(S_ind[0]);
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
                string q = "UPDATE TESTS SET ";
                q += "TITLE='" + title + "', ";
                q += "QUESTIONS='" + id_q + "' ";
                q += "WHERE ID=" + id;
                SqlCommand c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("The test ha-has been saved!");
            }
            else MessageBox.Show("You haven't filled in all the data, do continue...");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string q = "SELECT * FROM TESTS WHERE ID=" + id + " ORDER BY ID";
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataReader dr = c.ExecuteReader();
                string[] question_ids = new string[31];
                while (dr.Read())
                {
                    textBox1.Text = dr[1].ToString();
                    question_ids = dr[2].ToString().Split();
                }
                dr.Close();

                checkedListBox1.Items.Clear();
                int index = 0;
                string qi = "SELECT * FROM QUESTIONS ORDER BY ID";
                SqlCommand ci = new SqlCommand(qi, conn);
                SqlDataReader dri = ci.ExecuteReader();
                while (dri.Read())
                {
                    checkedListBox1.Items.Add(dri[0].ToString() + " - " + dri[2].ToString());
                    for (int i = 0; question_ids[i] != ""; i++)
                        if (dri[0].ToString() == question_ids[i])
                        {
                            checkedListBox1.SetItemChecked(index, true);
                        }
                    index++;
                }          
                dri.Close();            
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Are you extremely sure you want to obliterate this test out of existence?", "Stop where you are!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string[] S = listBox1.SelectedItem.ToString().Split();
                    int id = int.Parse(S[0]);
                    string q = "DELETE FROM TESTS WHERE ID=" + id;
                    SqlCommand c = new SqlCommand(q, conn);
                    c.ExecuteNonQuery();
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    if (listBox1.SelectedIndex == -1)
                        listBox1.SelectedIndex = 0;
                }
            }
            else MessageBox.Show("You haven't sellected one single test, are you even in the right tab?");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = checkedListBox1.SelectedItem.ToString();
        }

        private void Modify_Test_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            Load_Tests();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
