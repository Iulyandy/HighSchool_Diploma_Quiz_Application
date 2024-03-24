using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Atestat
{
    public partial class Add_Question : UserControl
    {
        public Add_Question()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        List<int> LID;
        string image = "";
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                image = file;
                pictureBox1.Load(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                int chapter = LID[comboBox1.SelectedIndex];
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
                //daca s-a completat tot
                if (question != "" && A != "" && B != "" && C != "" && D != "" && correct != "")
                {
                    string q = "INSERT INTO QUESTIONS (ID_CATEGORY, PROMPT, A, B, C, D, CORRECT, IMAGE) VALUES ";
                    q = q + "(" + chapter + ", '" + question + "', '" + A + "', '" + B + "', '" + C + "', '" + D + "', '" + correct + "', '" + image + "')";
                    SqlCommand c = new SqlCommand(q, conn);
                    c.ExecuteNonQuery();
                    MessageBox.Show("The question was processed!");
                }
                else
                    MessageBox.Show("Wait a minute, We're not done yet! You haven't filled in all the necessary data!");
            }
            else
                MessageBox.Show("Even more importantly, you haven't chosen the fandom. You're no fun...");
        }

        private void Add_Question_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            LID = new List<int>();
            string q = "SELECT * FROM CATEGORY";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[1].ToString());
                LID.Add(int.Parse(dr[0].ToString()));
            }
            dr.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
