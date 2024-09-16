using MebbisStaj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;
using System.Collections;
using MySqlX.XDevAPI;
using System.IO;

namespace SANEKTİS
{
    public partial class Form1 : Form
    {
        int sayac1, sayac2, sayac3, sayac4;
        List<string> veriTabaniListesi = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            VeriTabaniIsmiCekme();
        }
        private void VeriTabaniIsmiEkle(string name)
        {
            foreach (string item in veriTabaniListesi)
            {
                if (item != name)
                {
                    YeniDonemOlustur();
                }
            }
            if(veriTabaniListesi.Count ==0) 
            {
                YeniDonemOlustur();
             }
            try
            {
                string OlusturulanDonem = "CREATE DATABASE " + " `" +   name + "`" + " COLLATE 'utf32_turkish_ci' ;";
                Mebbis.dc(OlusturulanDonem);
                YeniForm2Acma();
                
            }
            catch (Exception ex)
            {          
             DialogResult result = MessageBox.Show("Bu isimde bir dönem oluşturulmuş\nEğer bu dönemle ilgili işleminiz yarıda kaldıysa Evet butonuna basın\nBu dönemi sıfırdan oluşturmak istiyorsanız Hayır butonuna basın\nBu dönemle ilgili işleminiz bitti ise İptal butonuna basın ", "Hata", MessageBoxButtons.YesNoCancel, 
                 MessageBoxIcon.Error);
            
                if(result == DialogResult.Yes)
                {
                    SayacDevam();
                    YeniForm2Acma();
                }              
                else if(result == DialogResult.No)
                {
                    SayacSayisiSifirlama();

                    YeniForm2Acma();
                }
            }
        }
     
        private void YeniForm2Acma()
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
        private void SayacSayisiKayit()
        {
            using (StreamWriter sw = new StreamWriter("sayac_values.txt"))
            {
                sw.WriteLine(sayac1);
                sw.WriteLine(sayac2);
                sw.WriteLine(sayac3);
                sw.WriteLine(sayac4);
            }
        }
        private void SayacSayisiSifirlama()
        {         
                sayac1 = 1;
                sayac2 = 1;
                sayac3 = 1;
                sayac4 = 1;
                SayacSayisiKayit();
            
        }
        private void SayacDevam()
        {
            if (System.IO.File.Exists("sayac_values.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("sayac_values.txt");
                if (lines.Length >= 4)
                {
                    sayac1 = int.Parse(lines[0]);
                    sayac2 = int.Parse(lines[1]);
                    sayac3 = int.Parse(lines[2]);
                    sayac4 = 1;
                }
            }
            SayacSayisiKayit();
        }
        private void YeniDonemOlustur()
        {           
                sayac1 = 1;
                sayac2 = 1;
                sayac3 = 1;
                sayac4 = 0;
                SayacSayisiKayit();
            
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            
            VeriTabaniIsmiEkle(textBox1.Text);
            
            Mebbis.Baglanti(textBox1.Text);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Yeni Dönem Oluşturmak İçin Dönem Adı Giriniz")
            {
                textBox1.Text = string.Empty;
            }

        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9\b-]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrEmpty(textBox1.Text);
        }

      

        private void comboBox1_Click(object sender, EventArgs e)
        {          
           VeriTabaniIsmiCekme();                                
        }


        private void VeriTabaniIsmiCekme()
        {
            veriTabaniListesi.Clear();
            comboBox1.Items.Clear();
            int i = 0;
            string SqlString = "SHOW DATABASES;";
            DataSet veriTabaniIsmi = Mebbis.ds(SqlString);
            int rowCount = veriTabaniIsmi.Tables[0].Rows.Count;
            while (i < rowCount)
            {                              
                    veriTabaniListesi.Add(veriTabaniIsmi.Tables[0].Rows[i][0].ToString());                
                    i++;               
            }
            veriTabaniListesi = veriTabaniListesi.Where(x => x != "mysql" && x!= "information_schema" && x != "performance_schema").ToList();

            foreach (var item in veriTabaniListesi)
            {
                comboBox1.Items.Add(item);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SecilenDönem = "USE `" + comboBox1.SelectedItem + "`;";
            Mebbis.dc(SecilenDönem);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
            label3.Text = DateTime.Now.ToLongDateString();
        }

        private void pictureBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = MessageBox.Show("      Tasarlayanlar:\n\n      Nazmi Yakar\n\n      Alp Bora Korkmaz\n\n      Sabri Balsever\n\n      Ece Koyuncu");
            
        }      
    }
}
