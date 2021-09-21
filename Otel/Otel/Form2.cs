using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Otel
{
    public partial class Form2 : Form

    {
        
        public Form1 fr1;
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:oteldatabase1.accdb");
        private void odalar()
        {
            foreach (Control item in fr1.Controls)
            {
                if (item is Button )
                {
                    if (item.BackColor == Color.White)
                    {
                        comboBox1.Items.Add(item.Text);
                    }   
                }
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                odalar();
            }
            
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Insert Into odabilgileri(oda,kat,yataksayisi,banyosayisi,cephe,gucret,durumu) values('" + comboBox1.Text + "','" + textBox9.Text + "','" + textBox8.Text + "','" + textBox7.Text + "','" + textBox6.Text + "','" + textBox10.Text + "','BOŞ') ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Oda Kaydı Eklendi.", "Kayıt");
            comboBox1.Text = "";
            //comboBox2.Text = "";
            
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is TextBox)
                {
                    groupBox1.Controls[i].Text = "";
                }
            }
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            fr1.comboBox1.Items.Clear();
            fr1.odadurumu();
            odalar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update odabilgileri set kat = @kat, yataksayisi=@yataksayisi, banyosayisi=@banyosayisi, cephe = @cephe, gucret=@gucret where oda=@oda",baglanti);
            komut.Parameters.AddWithValue("@kat",textBox4.Text );
            komut.Parameters.AddWithValue("@yataksayisi",textBox3.Text );
            komut.Parameters.AddWithValue("@banyosayisi", textBox2.Text );
            komut.Parameters.AddWithValue("@cephe",textBox1.Text);
            komut.Parameters.AddWithValue("@gucret",textBox5.Text);
            komut.Parameters.AddWithValue("@oda", comboBox2.Text );
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Oda kaydı güncellendi!");
            for (int i = 0; i < groupBox2.Controls.Count; i++)
			{
			    if (groupBox2.Controls[i] is TextBox)
	                {
		                groupBox2.Controls[i].Text = "" ;
	                }
			}
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            fr1.comboBox1.Items.Clear();
            fr1.odadurumu();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
             OleDbCommand komut  = new OleDbCommand("select * from  odabilgileri where oda= '"+comboBox2.SelectedItem+ "' ",baglanti);
             OleDbDataReader read = komut.ExecuteReader();
             while (read.Read())
             {
                 textBox4.Text = read["kat"].ToString();
                 textBox3.Text = read["yataksayisi"].ToString();
                 textBox2.Text = read["banyosayisi"].ToString();
                 textBox1.Text = read["cephe"].ToString();
                 textBox5.Text = read["gucret"].ToString();
             }
             baglanti.Close();  
             
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
