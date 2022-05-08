using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ooplab1
{
    public partial class Form2 : Form
    {
        TextWriter txt;
        bool square, triangle, circle, hexagon;
        int dif;
        int x, y;
        int color;
        
        public Form2()
        {
            InitializeComponent();
            label7.Hide();
            label8.Hide();
     
            label5.Hide();
            label6.Hide();

            textBox3.Hide();
            textBox4.Hide();



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
                    textBox3.Text = x.ToString();
                    textBox4.Text = y.ToString();
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
                else
                {
                    dif = int.Parse(lines[0]);
                    switch (lines[0] ){
                        case "0":
                            x = 15;
                            y = 15;
                            break;
                          case "1":
                            x = 9;
                            y = 9;
                            break;
                        case "2":
                            x = 6;
                            y = 6;
                            break;
                    }
                    textBox3.Text = x.ToString();
                    textBox4.Text = y.ToString();
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
                if (dif == 3)
                {
                    label5.Show();
                    label6.Show();
                    textBox3.Show();
                    textBox4.Show();
                }
            }
            }



        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
            {
                txt = new StreamWriter("settings.txt");
                if (dif == 3)
                {
                    try
                    {
                        x = int.Parse(textBox3.Text);
                        y = int.Parse(textBox4.Text);
                    }
                    catch (Exception ex) { }




                    txt.Write(dif + System.Environment.NewLine + x + System.Environment.NewLine + y + System.Environment.NewLine + checkBox1.Checked + System.Environment.NewLine + checkBox2.Checked + System.Environment.NewLine + checkBox3.Checked + System.Environment.NewLine + checkBox4.Checked + System.Environment.NewLine + color);
                }
                else
                    txt.Write(dif + System.Environment.NewLine + x + System.Environment.NewLine + y + System.Environment.NewLine + checkBox1.Checked + System.Environment.NewLine + checkBox2.Checked + System.Environment.NewLine + checkBox3.Checked + System.Environment.NewLine + checkBox4.Checked + System.Environment.NewLine + color);
            
            txt.Close();
                label7.Show();
                label8.Hide();
                
            }
            else
            {
                label8.Show();
            }
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
                    dif = 1;
                    break;
                case 2:
                    dif = 2;
                    break;
                case 3:
                    dif = 3;
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
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



    }
}
