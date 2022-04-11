/*
 @author Enes Ay, Fatih Eroglu
 @date 23.03.2022
downgrade to net 5
 */
using System.Xml.Serialization;

namespace ooplab1
{
    public partial class Form1 : Form
    {
        bool isAdmin = false;
        String[] usr = { "user", "admin" };
        String[] pass = { "user", "admin" };
        TextWriter txt,txtx;
        bool square, triangle, circle, hexagon;
        int dif;
        int x, y;
        int color;
        int actv;
        public static string lastUser;
        List<UserBase> list = new List<UserBase>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        public Form1()
        {
            InitializeComponent();
            textBox1.Select();
            label3.Hide();
            label5.Hide();
            groupBox2.Hide();
            try
            {
                using (FileStream fsr = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Read))
                {

                    list = srl.Deserialize(fsr) as List<UserBase>;

                }
            }
            catch (Exception ex)
            {
                list.Add(new UserBase() { username = "admin", password = SHA512("admin") });
                using (FileStream fsw = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Create, FileAccess.Write))
                {

                    srl.Serialize(fsw, list);
                }
            }
            /*

            list.Add(new UserBase() { username = "admin", password = SHA512("admin") });
            using (FileStream fsw = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Create, FileAccess.Write))
            {

                srl.Serialize(fsw, list);
            }
            
              */






                if (File.Exists("lastuser.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("lastuser.txt");
                textBox1.Text= lines[0];
            }
               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            signup form = new signup();
            form.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Panel f=new Panel();
            f.Show();
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) { textBox2.PasswordChar = '\0'; }
            else { textBox2.PasswordChar = '*'; }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            profile prf=new profile();
            prf.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            info a=new info();
            a.ShowDialog();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
         
            if (checkBox5.Checked) { textBox2.PasswordChar = '\0'; }
            else { textBox2.PasswordChar = '*'; }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream fsr = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Read))
            {

                list = srl.Deserialize(fsr) as List<UserBase>;

            }


            bool a = true;
            int i = 0;
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].username == textBox1.Text && list[i].password == SHA512(textBox2.Text))
                {
                    if (list[i].username == "admin")
                    {

                        isAdmin = true;
                        label5.Show();

                    }
                    groupBox2.Show();
                    lastUser = textBox1.Text;
                    txtx = new StreamWriter("lastuser.txt");
                    txtx.Write(lastUser);
                    txtx.Close();
                    actv = i;
                    txtx = new StreamWriter("actv.txt");
                    txtx.Write(i);
                    txtx.Close();
                    break;

                }
                if (!a) label3.Show();

            }

        }

     
        private void label4_Click(object sender, EventArgs e)
        {

            Form2 form=new Form2();
            form.TopMost = true;
            form.Show();
         
               
           

           
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


        public static bool IsTextFileEmpty(string fileName)
        {
            var info = new FileInfo(fileName);
            if (info.Length == 0)
                return true;

            // only if your use case can involve files with 1 or a few bytes of content.
            if (info.Length < 6)
            {
                var content = File.ReadAllText(fileName);
                return content.Length == 0;
            }
            return false;
        }





















    }
}