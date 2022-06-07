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
    public partial class profile : Form
    {
        bool add=false;
        List<UserBase> list = new List<UserBase>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        string actvs;
        int actv;
        SqlConnection con = new SqlConnection();

        public profile()
        {
            InitializeComponent();
            textBox2.Hide();
            string asdasd = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Trusted_Connection=True;TrustServerCertificate=True;";
            con = new SqlConnection(asdasd);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT username FROM [OOPtable].[dbo].[Table_2]",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (File.Exists("actv.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("actv.txt");
                actvs = lines[0];
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ToString() == actvs) { actv = i; }

            }
           con.Close();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [OOPtable].[dbo].[Table_2] where username like @username",con);
            cmd.Parameters.AddWithValue("@username", actvs);
            da = new SqlDataAdapter(cmd);
             dt = new DataTable();
            da.Fill(dt);

            con.Close();
            label1.Text = "";
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();



            label1.Text = dt.Rows[0][0].ToString() ;
         
           

            textBox3.Text = dt.Rows[0][2].ToString();
            textBox4.Text = dt.Rows[0][3].ToString();
            textBox5.Text = dt.Rows[0][4].ToString();
            textBox6.Text = dt.Rows[0][5].ToString();
            textBox7.Text = dt.Rows[0][6].ToString();
            textBox8.Text = dt.Rows[0][7].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        { con.Open();
            SqlCommand cmd;
            if (textBox2.Text == "")
            {
               cmd = new SqlCommand("UPDATE [OOPtable].[dbo].[Table_2] SET name=@name ,email=@email ,phone=@phone ,adderss=@address ,city=@city ,country=@country WHERE username like @username", con);
                cmd.Parameters.AddWithValue("@name", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox8.Text);
                cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                cmd.Parameters.AddWithValue("@address", textBox5.Text);
                cmd.Parameters.AddWithValue("@city", textBox6.Text);
                cmd.Parameters.AddWithValue("@country", textBox7.Text);
                cmd.Parameters.AddWithValue("@username", label1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated!");
            }

            else
            {
                 cmd = new SqlCommand("UPDATE [OOPtable].[dbo].[Table_2] SET password =@password ,name=@name ,email=@email ,phone=@phone ,adderss=@address ,city=@city ,country=@country WHERE username like @username", con);

                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                cmd.Parameters.AddWithValue("@name", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox8.Text);
                cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                cmd.Parameters.AddWithValue("@address", textBox5.Text);
                cmd.Parameters.AddWithValue("@city", textBox6.Text);
                cmd.Parameters.AddWithValue("@country", textBox7.Text);
                cmd.Parameters.AddWithValue("@username", label1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated!");

            }
            





        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                add = true;
            textBox2.Show();
            }
            else
            { add = false;
                textBox2.Hide();
            }

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
