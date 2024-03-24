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

namespace Atestat
{
    public partial class Standard_Tests : Form
    {
        public Standard_Tests()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        SqlConnection conn;
        List<question> LI;
        int pos;
        public int user_id;
        public string fandom;
        int id_test;


        private void Load_Tests()
        {
            string q = "SELECT * FROM TESTS";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            dr.Close();
        }

        private void Standard_Tests_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
            Load_Tests();
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
            int c = 0;
            foreach (question I in LI)
                if (I.Correct == I.Answer)
                    c++;
            string level = "";
            if (fandom == "ST")
            {
                if (c <= 2) level = "Ensign";
                else if (c <= 4) level = "Liutenant";
                else if (c <= 6) level = "Commander";
                else if (c <= 8) level = "Captain";
                else if (c <= 10) level = "Admiral";
            }
            else
            {
                if (c <= 2) level = "Youngling";
                else if (c <= 4) level = "Padawan";
                else if (c <= 6) level = "Knight";
                else if (c <= 8) level = "Master";
                else if (c <= 10) level = "Great Master";
            }
            MessageBox.Show("You answered correctly to " + c + " questions. Your current rank is that of " + level);
            string q = "INSERT INTO STANDARD_RESULTS (USER_ID, SCORE,RANK,TEST_ID) VALUES";
            q = q + "(" + user_id + "," + c + ",'" + level + "'," + id_test + ")";
            SqlCommand cc = new SqlCommand(q, conn);
            cc.ExecuteNonQuery();
            q = "UPDATE USERS SET RANK='" + level + "' WHERE ID=" + user_id;
            cc = new SqlCommand(q, conn);
            cc.ExecuteNonQuery();
            q = "SELECT * FROM USERS WHERE ID=" + user_id;
            SqlCommand cm = new SqlCommand(q, conn);
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                string name = dr[2].ToString();
                Welcome form = new Welcome();
                form.id_user = user_id;
                form.fandom = fandom;
                if (fandom == "SW")
                    form.Text = "Hello there " + level + " " + name;
                else
                    form.Text = level + " " + name + " on board.";
                form.ShowDialog();
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string[] S = comboBox1.SelectedItem.ToString().Split();
                id_test = int.Parse(S[0]);
                string q = "SELECT * FROM TESTS WHERE ID=" + id_test;
                SqlCommand c = new SqlCommand(q, conn);
                SqlDataReader dr = c.ExecuteReader();
                LI = new List<question>();
                string[] questions_ids = new string[31];
                while (dr.Read())
                {
                    questions_ids = dr[2].ToString().Split();
                }
                dr.Close();
                
                foreach (string id in questions_ids)
                        if (id != "")
                        {
                            string qi = "SELECT * FROM QUESTIONS WHERE ID=" + id;
                            SqlCommand ci = new SqlCommand(qi, conn);
                            SqlDataReader dri = ci.ExecuteReader();
                            string prompt, a, b, C, d, correct, image;
                            while (dri.Read())
                            {
                                prompt = dri[2].ToString();
                                a = dri[3].ToString();
                                b = dri[4].ToString();
                                C = dri[5].ToString();
                                d = dri[6].ToString();
                                correct = dri[7].ToString();
                                image = dri[8].ToString();
                                question I = new question(id_test, prompt, a, b, C, d, correct, image);
                                I.Answer = "";
                                LI.Add(I);
                            }

                            dri.Close();
                        }
                
                pos = 0;
                Show_Question();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
