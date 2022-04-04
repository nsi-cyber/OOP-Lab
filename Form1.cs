/*
 @author Enes Ay, Fatih Eroglu
 @date 23.03.2022
downgrade to net 5
 */
namespace ooplab1
{
    public partial class Form1 : Form
    {
        String[] usr = { "user", "admin" };
        String[] pass = { "user", "admin" };
        TextWriter txt,txtx;
        bool square, triangle, circle, hexagon;
        int dif;
        int x, y;
        int color;
        public static string lastUser;

        public Form1()
        {
            InitializeComponent();
            textBox1.Select();
            label3.Hide();
            groupBox2.Hide();
           
            if (File.Exists("lastuser.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("lastuser.txt");
                textBox1.Text= lines[0];
            }
               
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) { textBox2.PasswordChar = '\0'; }
            else { textBox2.PasswordChar = '*'; }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
         
            if (checkBox5.Checked) { textBox2.PasswordChar = '\0'; }
            else { textBox2.PasswordChar = '*'; }
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == usr[0] && textBox2.Text == pass[0]) || (textBox1.Text == usr[1] && textBox2.Text == pass[1]))
            {
                groupBox2.Show();
                lastUser = textBox1.Text;
                txtx = new StreamWriter("lastuser.txt");
                txtx.Write(lastUser);
                txtx.Close();
            }
            else
                label3.Show();
        }

     
        private void label4_Click(object sender, EventArgs e)
        {

            Form2 form=new Form2();
            form.TopMost = true;
            form.Show();
         
               
           

           
        }
    }
}