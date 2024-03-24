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

namespace Atestat
{
    public partial class Random_Tests : Form
    {
        public Random_Tests()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        SqlConnection conn;
        List<question> LI;
        int pos;
        public int user_id;
        public string fandom;
        int nrquestions = 0;
        private void Load_Tests()
        {
            string q = "SELECT * FROM TESTS";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
        }
        private void Random_Tests_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
        }

        private void Show_Question()
        {
            question I = LI[pos];
            textBox1.Text = I.Prompt;
            textBox2.Text = I.A;
            textBox3.Text = I.B;
            textBox4.Text = I.C;
            textBox5.Text = I.D;
            string r = I.Answer;
            if (r.IndexOf('A') != -1) checkBox1.Checked = true;
            else checkBox1.Checked = false;
            if (r.IndexOf('B') != -1) checkBox2.Checked = true;
            else checkBox2.Checked = false;
            if (r.IndexOf('C') != -1) checkBox3.Checked = true;
            else checkBox3.Checked = false;
            if (r.IndexOf('D') != -1) checkBox4.Checked = true;
            else checkBox4.Checked = false;
            string image = I.Image;
            if (image != "")
                pictureBox1.Load(Application.StartupPath + "/Images/" + image);
            else
                pictureBox1.ImageLocation = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[pos].Answer = r;
            int correct = 0, number_of_questions = 0;
            foreach (question I in LI)
            {
                number_of_questions++;
                if (I.Correct == I.Answer)
                    correct++;
                
            }
            
            MessageBox.Show("You answered adequately to  " + correct + " questions out of  " + number_of_questions + ".");
            string query = "INSERT INTO RANDOM_RESULTS (USER_ID, SCORE, NUMBER_OF_QUESTIONS) VALUES";
            query = query + "(" + user_id + ", " + correct + "," + number_of_questions + ")";
            SqlCommand cc = new SqlCommand(query, conn);
            cc.ExecuteNonQuery();
            query = "SELECT * FROM USERS WHERE ID=" + user_id;
            SqlCommand cm = new SqlCommand(query, conn);
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                string name = dr[2].ToString();
                string rank = dr[5].ToString();
                Welcome form = new Welcome();
                form.id_user = user_id;
                form.fandom = fandom;
                form.Text = "Hello there " + rank + " " + name;
                form.ShowDialog();
            }
            dr.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[pos].Answer = r;
            pos--;
            if (pos < 0) pos = 0;
            Show_Question();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string r = "";
            if (checkBox1.Checked) r = r + "A";
            if (checkBox2.Checked) r = r + "B";
            if (checkBox3.Checked) r = r + "C";
            if (checkBox4.Checked) r = r + "D";
            LI[pos].Answer = r;
            pos++;
            if (pos >= LI.Count) pos--;
            Show_Question();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            nrquestions = (int)numericUpDown1.Value;
            LI = new List<question>();
            string id_q = " ";
            string qr = "SELECT TOP " + nrquestions + " ID FROM QUESTIONS ORDER BY NEWID()";
            SqlCommand cr = new SqlCommand(qr, conn);
            SqlDataReader dr = cr.ExecuteReader();
            while (dr.Read())
                id_q = id_q + dr[0].ToString() + " ";
            dr.Close();
            string[] SI = id_q.Split();
            foreach (string idi in SI)
                if (idi != "")
                {

                    string qi = "SELECT * FROM QUESTIONS WHERE ID=" + idi;
                    SqlCommand ci = new SqlCommand(qi, conn);
                    SqlDataReader dri = ci.ExecuteReader();
                    string prompt, a, b, C, d, correct, image;
                    while (dri.Read())
                    {
                        int idt = int.Parse(dri[0].ToString());
                        prompt = dri[2].ToString();
                        a = dri[3].ToString();
                        b = dri[4].ToString();
                        C = dri[5].ToString();
                        d = dri[6].ToString();
                        correct = dri[7].ToString();
                        image = dri[8].ToString();
                        question I = new question(idt, prompt, a, b, C, d, correct, image);
                        I.Answer = "";
                        LI.Add(I);
                    }

                    dri.Close();
                }
            pos = 0;
            Show_Question();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
