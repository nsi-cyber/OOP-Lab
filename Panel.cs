using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Panel()
        {
            InitializeComponent();
            textBox1.Hide();
            button4.Hide();
            using (FileStream fsr = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Read))
            {

                list = srl.Deserialize(fsr) as List<UserBase>;

            }

       

            listBox1.DataSource = list;
            listBox1.DisplayMember = "username";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            temp= list[listBox1.SelectedIndex].highscore;
            label1.Text=list[listBox1.SelectedIndex].username;
            textBox2.Text = list[listBox1.SelectedIndex].password;
            textBox3.Text = list[listBox1.SelectedIndex].name;
            textBox4.Text = list[listBox1.SelectedIndex].phone;
            textBox5.Text=list[listBox1.SelectedIndex].address;
            textBox6.Text = list[listBox1.SelectedIndex].city;
            textBox7.Text = list[listBox1.SelectedIndex].country;
            textBox8.Text= list[listBox1.SelectedIndex].email;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            list[listBox1.SelectedIndex].username = label1.Text;
            list[listBox1.SelectedIndex].password = textBox2.Text ;
            list[listBox1.SelectedIndex].name = textBox3.Text ;
            list[listBox1.SelectedIndex].phone = textBox4.Text ;
           list[listBox1.SelectedIndex].address = textBox5.Text ;
           list[listBox1.SelectedIndex].city = textBox6.Text ;
          list[listBox1.SelectedIndex].country = textBox7.Text;
           list[listBox1.SelectedIndex].email = textBox8.Text;
            list[listBox1.SelectedIndex].highscore = temp;
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Write))
            {
                srl.Serialize(fs, list);
            }

            listBox1.DataSource = list;
            listBox1.DisplayMember = "username";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            list.Remove(list[listBox1.SelectedIndex]);
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Write))
            {
                srl.Serialize(fs, list);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Hide();
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
            list.Add(new UserBase() { username = textBox1.Text, password = SHA512(textBox2.Text), name = textBox3.Text, phone = textBox4.Text, address = textBox5.Text, city = textBox6.Text, country = textBox7.Text, email = textBox8.Text, highscore = 0 }); ;
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Write))
            {
                srl.Serialize(fs, list);
            }
            listBox1.DataSource = list;
            listBox1.DisplayMember = "username";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
