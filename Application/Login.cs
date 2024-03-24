using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.Linq;

namespace Atestat
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        int id_user;

        private void Form1_Shown(object sender, EventArgs e)
        {
            string cs = @"Data Source = (localdb)\Atestat; Initial Catalog = Atestat; Integrated Security = True";
            conn = new SqlConnection(cs);
            conn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string u = textBox1.Text;
            string p = textBox2.Text;
            string q = "SELECT * FROM USERS WHERE USER_NAME='" + u + "' AND PASSWORD='" + p + "'";
            SqlCommand c = new SqlCommand(q, conn);
            SqlDataReader dr = c.ExecuteReader();

            if (dr.Read())
            {
                string full_name = dr[2].ToString();
                string type = dr[4].ToString();
                string level = dr[5].ToString();
                string franchise = dr[6].ToString();
                if (type == "admin")
                {
                    Admin form = new Admin();
                    form.Text = "Hello there " + full_name;
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
                else
                {
                    Welcome form = new Welcome();
                    if (franchise == "SW")
                        form.Text = "Hello there " + level + " " + full_name;
                    else if(franchise == "ST")
                        form.Text = level + " " + full_name + " on board.";
                    id_user = int.Parse(dr[0].ToString());
                    form.id_user = id_user;
                    form.fandom = franchise;
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Incorrect Login Data!");
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Create_Account form = new Create_Account();
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
