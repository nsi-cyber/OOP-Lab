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

    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
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
            //UserBase newUser = new UserBase(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text);
       List<UserBase> userBases = new List<UserBase>();
            XmlSerializer srl= new XmlSerializer(typeof(List<UserBase>));


            using (FileStream fsr = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Read))
            {

                userBases = srl.Deserialize(fsr) as List<UserBase>;

            }







            userBases.Add(new UserBase() { username = textBox1.Text,password = SHA512(textBox2.Text), name=textBox3.Text, phone= textBox4.Text,address= textBox5.Text, city=textBox6.Text,country= textBox7.Text,email= textBox8.Text });
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Write)) { 
            
                srl.Serialize(fs,userBases);
                MessageBox.Show("new user signed up");


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
