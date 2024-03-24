using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat
{
    public partial class Create_Account : Form
    {
        public Create_Account()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        int id_user;

        private void Create_Account_Load(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();

        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            pictureBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string names = textBox1.Text;
            string user = textBox2.Text;
            string password = textBox3.Text;
            string checkpass= textBox4.Text;
            string type = "user";
            string fan = "ST";
            string level = "Ensign";
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                label2.Text = "Come on, choose one already!";
                // pictureBox1.Image = //image
                return;
            }
            else if (radioButton1.Checked == true)
            {
                fan = "ST";
                level = "Ensign";
            }

            else if (radioButton2.Checked == true)
            {
                fan = "SW";
                level = "Youngling";
            }

            if (checkpass == password)
            {
                string q = "INSERT INTO USERS (USER_NAME,NAME,PASSWORD,TYPE,RANK,FRANCHISE) VALUES";
                q += "('" + user + "', '" + names + "', '" + password + "','" + type + "','" + level + "','" + fan + "')";
                SqlCommand c = new SqlCommand(q, conn);
                c.ExecuteNonQuery();
                MessageBox.Show("Your account has been created successfully!");
                q = "SELECT * FROM USERS WHERE USER_NAME='" + user + "' AND PASSWORD='" + checkpass + "'";
                c = new SqlCommand(q, conn);
                SqlDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                    Welcome form = new Welcome();
                    if (fan == "SW")
                        form.Text = "Hello there " + level + " " + names;
                    else if(fan == "ST")
                        form.Text = level + " " + names + " on board.";
                    id_user = int.Parse(dr[0].ToString());
                    form.id_user = id_user;
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
            }
            else 
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
