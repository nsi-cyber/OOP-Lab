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
    public partial class scorelist : Form
    {
        List<UserBase> list = new List<UserBase>();
        List<String> liste = new List<String>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        public scorelist()
        {
            InitializeComponent();
            using (FileStream fsr = new FileStream(Environment.CurrentDirectory + "\\userData.xml", FileMode.Open, FileAccess.Read))
            {

                list = srl.Deserialize(fsr) as List<UserBase>;

            }

            for (int i = 0; i < list.Count; i++)
            {
                liste.Add(list[i].username + " - " + list[i].highscore);
            }



            listBox1.DataSource = liste;
            




        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
