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
    public partial class scorelist : Form
    {
        List<UserBase> list = new List<UserBase>();
        List<String> liste = new List<String>();
        XmlSerializer srl = new XmlSerializer(typeof(List<UserBase>));
        SqlConnection con = new SqlConnection();

        public scorelist()
        {
            InitializeComponent();
            string asdasd = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Trusted_Connection=True;TrustServerCertificate=True;";
            con = new SqlConnection(asdasd);
            con.Open();
            SqlCommand cmd = new SqlCommand("select username, highscore from [OOPtable].[dbo].[Table_2] order by highscore",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();


            dataGridView1.DataSource = dt;
            




        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
