using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ooplab1
{

    public partial class signup : Form
    {
        SqlConnection con = new SqlConnection();

        public signup()
        {
            InitializeComponent();
            string asdasd = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Trusted_Connection=True;TrustServerCertificate=True;";
            con = new SqlConnection(asdasd);
       
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private TextBox GetTextBox1()
        {
            return textBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into[OOPtable].[dbo].[Table_2] values(@username,@password,@name,@email,@phone,@address,@city,@country,@highscore)", con );






            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);
            cmd.Parameters.AddWithValue("@name", textBox3.Text);
            cmd.Parameters.AddWithValue("@email", textBox8.Text);
            cmd.Parameters.AddWithValue("@phone", textBox4.Text);
            cmd.Parameters.AddWithValue("@address", textBox5.Text);
            cmd.Parameters.AddWithValue("@city", textBox6.Text);
            cmd.Parameters.AddWithValue("@country", textBox7.Text);
            cmd.Parameters.AddWithValue("@highscore", 0);














            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("User Added");





           

        
        }
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }



    }
}
