using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Atestat
{
    public partial class Modify_Question : UserControl
    {
        public Modify_Question()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string image = "";

        private void load_questions()
        {
            string q = "SELECT * FROM QUESTIONS ORDER BY ID";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString() + " - " + dr[2].ToString());
            }
            dr.Close();
        }
        string get_category_by_id(int id)
        {
            string q = "SELECT * FROM CATEGORY WHERE ID=" + id;
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                return dr[1].ToString();
            }
            dr.Close();
            return "";
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {            
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                string category = get_category_by_id(id);
                string q = "SELECT * FROM QUESTIONS WHERE ID=" + id;
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Text = dr[1].ToString() + " - " + category;
                    textBox1.Text = dr[2].ToString();
                    textBox2.Text = dr[3].ToString();
                    textBox3.Text = dr[4].ToString();
                    textBox4.Text = dr[5].ToString();
                    textBox5.Text = dr[6].ToString();
                    string correct = dr[7].ToString();
                    if (correct.IndexOf('A') != -1) checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    if (correct.IndexOf('B') != -1) checkBox2.Checked = true;
                    else checkBox2.Checked = false;
                    if (correct.IndexOf('C') != -1) checkBox3.Checked = true;
                    else checkBox3.Checked = false;
                    if (correct.IndexOf('D') != -1) checkBox4.Checked = true;
                    else checkBox4.Checked = false;
                    string image = dr[8].ToString();
                    if (image != "")
                        pictureBox1.Load(Application.StartupPath + "/Images/" + image);
                    else
                        pictureBox1.ImageLocation = null;
                }
                dr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] S = listBox1.SelectedItem.ToString().Split();
                int id = int.Parse(S[0]);
                S = comboBox1.Text.Split();
                int id_cap = int.Parse(S[0]);
                string question = textBox1.Text;
                string A = textBox2.Text;
                string B = textBox3.Text;
                string C = textBox4.Text;
                string D = textBox5.Text;
                string correct = "";
                if (checkBox1.Checked) correct = correct + "A";
                if (checkBox2.Checked) correct = correct + "B";
                if (checkBox3.Checked) correct = correct + "C";
                if (checkBox4.Checked) correct = correct + "D";
                if (question != "" && A != "" && B != "" && C != "" && D != "" && correct != "")
                {
                    string q = "UPDATE QUESTIONS SET ";
                    q = q + "ID_CATEGORY=" + id_cap + ", ";
                    q = q + "PROMPT='" + question + "', ";
                    q = q + "A='" + A + "', ";
                    q = q + "B='" + B + "', ";
                    q = q + "C='" + C + "', ";
                    q = q + "D='" + D + "', ";
                    q = q + "CORRECT='" + correct + "', ";
                    q = q + "IMAGE='" + image + "' ";
                    q = q + " WHERE ID=" + id;
                    SqlCommand c = new SqlCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("The changes are safe, for now... don't worry?");
                    load_questions();
                }
            }
            else MessageBox.Show("First things first, you may choose a question");
        }

        private void Modify_Question_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            load_questions();
            string q = "SELECT * FROM CATEGORY";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Are you extremely sure you want to obliterate this question out of existence?", "Wait a second...", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string[] S = listBox1.SelectedItem.ToString().Split();
                    int id = int.Parse(S[0]);
                    string q = "DELETE FROM QUESTIONS WHERE ID=" + id;
                    SqlCommand c = new SqlCommand(q, conn);
                    c.ExecuteNonQuery();
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    if (listBox1.SelectedIndex == -1)
                        listBox1.SelectedIndex = 0;
                }
            }
            else MessageBox.Show("You haven't sellected one single question, are you even in the right tab?");
        }
        string file_name(string s)
        {
            int i = s.Length - 1;
            while (s[i] != '\\') i--;
            return s.Substring(i + 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string file = openFileDialog1.FileName;
                image = file_name(file);                
                pictureBox1.Load(file);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
