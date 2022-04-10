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
    public partial class profile : Form
    {
        bool add=false;
        List<UserBase> list = new List<UserBase>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        int actv;
        public profile()
        {
            InitializeComponent();
            textBox2.Hide();
            using (FileStream fsr = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Read))
            {

                list = srl.Deserialize(fsr) as List<UserBase>;

            }
            if (File.Exists("actv.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("actv.txt");
                actv = int.Parse(lines[0]);
            }
            label1.Text = "";
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            label1.Text = list[actv].username;
         
           

            textBox3.Text = list[actv].name;
            textBox4.Text = list[actv].phone;
            textBox5.Text = list[actv].address;
            textBox6.Text = list[actv].city;
            textBox7.Text = list[actv].country;
            textBox8.Text = list[actv].email;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (add)
                list[actv].password = SHA512(textBox2.Text);

            list[actv].name = textBox3.Text;
            list[actv].phone = textBox4.Text;
            list[actv].address = textBox5.Text;
            list[actv].city = textBox6.Text;
            list[actv].country = textBox7.Text;
            list[actv].email = textBox8.Text;
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Write))
            {
                srl.Serialize(fs, list);
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
