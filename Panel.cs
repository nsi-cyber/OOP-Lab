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
    public partial class Panel : Form
    {
        public static string lastUser;
        List<UserBase> list = new List<UserBase>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        int temp;
        SqlConnection con = new SqlConnection();
        DataTable usernames = new DataTable();
        public Panel()
        {
            InitializeComponent();
            dataGridView2.Hide();
            textBox1.Hide();
            button4.Hide();
            string asdasd = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Trusted_Connection=True;TrustServerCertificate=True;";
            con = new SqlConnection(asdasd);
            refresh();







        }
        public DataTable SelectAll(string procName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (con)
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(procName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception sqlEx)
            {
                Console.WriteLine(@"：Unable to establish a connection: {0}", sqlEx);
            }

            return dt;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        /*
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [OOPtable].[dbo].[Table_2]");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
          


            label1.Text = dt.Rows[listBox1.SelectedIndex][0].ToString(); 
            textBox2.Text = dt.Rows[listBox1.SelectedIndex][1].ToString();
            textBox3.Text = dt.Rows[listBox1.SelectedIndex][2].ToString();
            textBox4.Text = dt.Rows[listBox1.SelectedIndex][3].ToString();
            textBox5.Text= dt.Rows[listBox1.SelectedIndex][4].ToString();
            textBox6.Text = dt.Rows[listBox1.SelectedIndex][5].ToString();
            textBox7.Text = dt.Rows[listBox1.SelectedIndex][6].ToString();
            textBox8.Text= dt.Rows[listBox1.SelectedIndex][7].ToString();
  temp = Int32.Parse(dt.Rows[listBox1.SelectedIndex][8].ToString() );


        }

*/
        public void clearAll() {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }
        public void refresh() {





            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT username FROM [OOPtable].[dbo].[Table_2]",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {

          

            con.Open(); 
            SqlCommand cmd = new SqlCommand("UPDATE [OOPtable].[dbo].[Table_2] SET password =@password ,name=@name ,email=@email ,phone=@phone ,adderss=@address ,city=@city ,country=@country WHERE username like @username",con);




            
            //cmd.Parameters.AddWithValue("@password", textBox2.Text);
            cmd.Parameters.AddWithValue("@password", SHA512(textBox2.Text));
            cmd.Parameters.AddWithValue("@name", textBox3.Text);
            cmd.Parameters.AddWithValue("@email", textBox8.Text);
            cmd.Parameters.AddWithValue("@phone", textBox4.Text);
            cmd.Parameters.AddWithValue("@address", textBox5.Text);
            cmd.Parameters.AddWithValue("@city", textBox6.Text);
            cmd.Parameters.AddWithValue("@country", textBox7.Text);
     cmd.Parameters.AddWithValue("@username", label1.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            refresh();
           clearAll();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string message = "Are you sure?";
            string caption = "Delete";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                
              con.Open();
            SqlCommand cmd = new SqlCommand("delete from [OOPtable].[dbo].[Table_2] where username like @user;",con);
                cmd.Parameters.AddWithValue("@user", label1.Text);

                cmd.ExecuteNonQuery();
            con.Close();
                refresh();
            }
            



        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            
            label1.Hide();
            textBox1.Show();
            button4.Show();
            button3.Hide(); 
            button2.Hide(); 
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

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

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into[OOPtable].[dbo].[Table_2] values(@username,@password,@name,@email,@phone,@address,@city,@country,@highscore)", con);






            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);
            // cmd.Parameters.AddWithValue("@password", SHA512(textBox2.Text));
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
            refresh();
            clearAll();






        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

      
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            { label1.Text = row.Cells[0].Value.ToString(); }
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [OOPtable].[dbo].[Table_2] where username LIKE '" + label1.Text + "';", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dts = new DataTable();
            da.Fill(dts);
            con.Close();
            dataGridView2.DataSource = dts;
            textBox2.Text = dataGridView2.Rows[0].Cells[1].Value?.ToString();
            textBox3.Text = dataGridView2.Rows[0].Cells[2].Value?.ToString();
            textBox4.Text = dataGridView2.Rows[0].Cells[4].Value?.ToString();
            textBox5.Text = dataGridView2.Rows[0].Cells[5].Value?.ToString();
            textBox6.Text = dataGridView2.Rows[0].Cells[6].Value?.ToString();
            textBox7.Text = dataGridView2.Rows[0].Cells[7].Value?.ToString();
            textBox8.Text = dataGridView2.Rows[0].Cells[3].Value?.ToString();

        }
    }

}
