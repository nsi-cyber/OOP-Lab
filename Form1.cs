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
            label8.Hide();
            groupBox3.Hide();
            if (File.Exists("lastuser.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("lastuser.txt");
                textBox1.Text= lines[0];
            }
                if (File.Exists("settings.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("settings.txt");
                comboBox1.SelectedIndex = int.Parse(lines[0]);
                comboBox2.SelectedIndex = int.Parse(lines[7]);
                if (lines[0] == "3")
                {
                    dif = int.Parse(lines[0]);
                    x = int.Parse(lines[1]);
                    y = int.Parse(lines[2]);
                    textBox3.Text = "3";
                    textBox4.Text = "4";
                    square = bool.Parse(lines[3]);
                    triangle = bool.Parse(lines[4]);
                    circle = bool.Parse(lines[5]);
                    hexagon = bool.Parse(lines[6]);
                    color= int.Parse(lines[7]);
                    checkBox1.Checked = square;
                    checkBox2.Checked = triangle;
                    checkBox3.Checked = circle;
                    checkBox4.Checked = hexagon;
                }
                else
                {
                    dif = int.Parse(lines[0]);

                    square = bool.Parse(lines[3]);
                    triangle = bool.Parse(lines[4]);
                    circle = bool.Parse(lines[5]);
                    hexagon = bool.Parse(lines[6]);
                    color = int.Parse(lines[7]);
                    checkBox1.Checked = square;
                    checkBox2.Checked = triangle;
                    checkBox3.Checked = circle;
                    checkBox4.Checked = hexagon;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            if (checkBox1.Checked) { textBox2.PasswordChar = '\0'; }
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

        

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label5.Hide();
            label6.Hide();
            textBox3.Hide();
            textBox4.Hide();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dif = 0;
                    break;
                case 1:
                  dif=1;
                    break;
                case 2:
                   dif=2;
                    break;
                case 3:
                    dif=3;
                   label5.Show();
                    label6.Show();
                    textBox3.Show();
                    textBox4.Show();


                    break;
                
            }
        }



        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    dif = 0;
                    break;
                case 1:
                    dif = 1;
                    break;
                case 2:
                    dif = 2;
                    break;

            }
        }



        /*





        private void button6_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked|| checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
            {
                txt = new StreamWriter("settings.txt");
                if (dif == 3)
                {
                    x = int.Parse(textBox3.Text);
                    y = int.Parse(textBox4.Text);

                    txt.Write(dif + System.Environment.NewLine + x + System.Environment.NewLine + y + System.Environment.NewLine + checkBox1.Checked + System.Environment.NewLine + checkBox2.Checked + System.Environment.NewLine + checkBox3.Checked + System.Environment.NewLine + checkBox4.Checked+System.Environment.NewLine + color);
                }
                else
                    txt.Write(dif + System.Environment.NewLine + "" + System.Environment.NewLine + "" + System.Environment.NewLine + checkBox1.Checked + System.Environment.NewLine + checkBox2.Checked + System.Environment.NewLine + checkBox3.Checked + System.Environment.NewLine + checkBox4.Checked+System.Environment.NewLine + color);

                txt.Close();
                label7.Show();
                label8.Hide();
            }
            else
            {
                label8.Show();
            }

        }

       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                square = true;
            else
                square = false;

        }

        

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                triangle = true;
            else
                triangle = false;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                circle = true;
            else
                circle = false;

        }


        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                hexagon = true;
            else
                hexagon = false;

        }

        */
        private void label4_Click(object sender, EventArgs e)
        {

            Form2 form=new Form2();
            form.Show();
         
                label7.Hide();
                label5.Hide();
                label6.Hide();

                textBox3.Hide();
                textBox4.Hide();/*
                label4.Text = "Close Settings";
                if (dif == 3)
                {
                    label5.Show();
                    label6.Show();
                    textBox3.Show();
                    textBox4.Show();
                }
            }
            else
            {
                groupBox3.Hide();
                label4.Text = "Settings";
            }

            */
        }
    }
}