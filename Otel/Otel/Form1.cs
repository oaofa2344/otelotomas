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
    public partial class Form1 : Form
    {
        public Form2 fr2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            fr2.fr1 = this;
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:oteldatabase1.accdb");
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void odadurumu()
        {
            int sayi = 101;
            foreach (Control btn in Controls)
            {
                if (btn is Button)
                {
                    if (btn.Name != "button1" && btn.Name != "button22")
                    {
                        btn.BackColor = Color.White;
                        btn.Text = "ODA " + sayi.ToString();
                        sayi++;
                    }
                }
            }
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from odabilgileri",baglanti);
             OleDbDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control oda in Controls)
                {
                    if (oda is Button)
                    {
                        if (read["oda"].ToString()==oda.Text && read["durumu"].ToString()=="BOŞ")
                        {
                            oda.BackColor = Color.Green;
                            comboBox1.Items.Add(read["oda"].ToString());
                            fr2.comboBox2.Items.Add(read["oda"].ToString());
                        }
                        if (read["oda"].ToString() == oda.Text && read["durumu"].ToString() == "DOLU")
                        {
                            oda.BackColor = Color.Red;

                           
                        }
                    }
                }
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            foreach (Control renkdegisimi in Controls)
            {
                if (renkdegisimi is Button )
                {
                    renkdegisimi.Click += renkdegisimi_Click;
                }
            }
            odadurumu();
            
        }

        private void renkdegisimi_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b.BackColor==Color.Red)
            {
                DialogResult cevap = MessageBox.Show("Oda boşaltılsın mı", "Boşaltma",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (cevap == DialogResult.Yes)
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("delete * from kayitbilgileri where oda='" + b.Text + "'", baglanti);
                    komut.ExecuteNonQuery();

                    OleDbCommand komut2 = new OleDbCommand("update odabilgileri set durumu='BOŞ' where oda='" + b.Text + "' ", baglanti);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Oda boşaltıldı");
                    fr2.comboBox2.Items.Clear();
                    fr2.comboBox1.Items.Clear();
                    comboBox1.Items.Clear();
                    odadurumu();
                  
                }
               
            }
           
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
             OleDbCommand komut  = new OleDbCommand("select * from  odabilgileri where oda= '"+comboBox1.SelectedItem+ "' ",baglanti);
             OleDbDataReader read = komut.ExecuteReader();
             while (read.Read())
             {
                 textBox9.Text = read["kat"].ToString();
                 textBox8.Text = read["yataksayisi"].ToString();
                 textBox7.Text = read["banyosayisi"].ToString();
                 textBox6.Text = read["cephe"].ToString();
                 textBox10.Text = read["gucret"].ToString();
             }
             baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into kayitbilgileri(ad,soyad,adres,telefon,email,oda,kat,gtarihi,ctarihi,gun,gucret,tutar,odemeturu,aciklama) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + textBox9.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + textBox13.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + comboBox2.Text + "','" + textBox12.Text + "') ", baglanti);
            komut.ExecuteNonQuery();
            OleDbCommand komut2 = new OleDbCommand("update odabilgileri set durumu ='DOLU' where oda = '"+comboBox1.SelectedItem+"' ",baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Rezervasyon Yapıldı.", "Rezervasyon");
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.Items.Clear();
            odadurumu();
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is TextBox)
                {
                    groupBox1.Controls[i].Text = "";
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            fr2.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan gun = new TimeSpan();
            gun = DateTime.Parse(dateTimePicker2.Text) - DateTime.Parse(dateTimePicker1.Text);
            textBox13.Text = gun.TotalDays.ToString();
            textBox11.Text = (double.Parse(textBox10.Text) * double.Parse(textBox13.Text)).ToString();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
       
        }
    }

