using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;





namespace MebbisStaj
{
    public partial class Form2 : Form
    {
        private bool navigating = false;
        private int sorguAlani = 0;
        private object[] args4;
        private int sayac1 = 1, sayac2 = 1, sayac3 = 1, sayac4 = 0;
        private string SqlString;
        string sorguNo = "";
        string[] kurumBaslikDevamDizisi = new string[] { };
        string kurumBaslıkIl = "", kurumBaslikIlce = "", kurumBaslikGenelMudurluk = "", kurumBaslikKurumTur = ""
      , kurumBaslikKurumKodu = "", kurumBaslikKurumAdi = "";
        string kurumDevamSQLString = "";
        string kurumBaslik = "";
        string kelimeAyiklaDevam = "";
        string alanAdlari = "";

        public Form2()
        {
            InitializeComponent();
            SayacSayisiKullanma();
            
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://mebbis.meb.gov.tr/");
            
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string html = "";
            
            if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/main.aspx" && !navigating)
            {

                navigating = true;
                webBrowser1.Navigate("https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx");
            }


            HtmlElement modul1 = webBrowser1.Document.GetElementById("cmbAlan");
            HtmlElement modul2 = webBrowser1.Document.GetElementById("cmbGrup");
            HtmlElement modul3 = webBrowser1.Document.GetElementById("cmbSorgu");


            if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 0)
            {
                sorguAlani = 1;

                if (sayac1 <= modul1.All.Count)
                {
                    if (sayac1 == modul1.All.Count)
                    {                                              
                        System.Environment.Exit(0);
                    }
                    modul1.SetAttribute("selectedIndex", sayac1.ToString());
                    
                }
                modul1.RaiseEvent("onChange");
                object[] args4 = { "someparameters" };
                SayacSayisiKayit();
                webBrowser1.Document.InvokeScript("__doPostBack", args4);
            }

            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 1)
            {
                sorguAlani = 2;

                if (sayac2 <= modul2.All.Count)
                {
                    modul2.SetAttribute("selectedIndex", sayac2.ToString());
                }
                modul2.RaiseEvent("onChange");
                object[] args4 = { "someparameters" };
                SayacSayisiKayit();
                webBrowser1.Document.InvokeScript("__doPostBack", args4);
            }
            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 2)
            {
                sorguAlani = 3;
                if (sayac3 <= modul3.All.Count)
                {
                    modul3.SetAttribute("selectedIndex", sayac3.ToString());
                }
                modul3.RaiseEvent("onChange");
                object[] args4 = { "someparameters" };
                SayacSayisiKayit();
                webBrowser1.Document.InvokeScript("__doPostBack", args4);


            }
            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 3)
            {
                IlSecme();
            }
            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 4)
            {
                CheckboxSecme();
            }
            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 5)
            {
                Sorgula();
            }
            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 6)
            {
                sorguAlani = 7;
                string truncateSabitTablolar = "";
                if (sayac4 == 0)
                {
                    string SqlIlceTablo = "CREATE TABLE IF NOT EXISTS ilceler (Id INT PRIMARY KEY AUTO_INCREMENT,IlceAdi VARCHAR(20)) COLLATE = UTF32_TURKISH_CI;";
                    Mebbis.dc(SqlIlceTablo);
                    truncateSabitTablolar = "TRUNCATE TABLE ilceler";
                    string SqlIlceVeri = "INSERT INTO  ilceler (IlceAdi) VALUES";
                    if (webBrowser1.Document != null)
                    {

                        var dropdown = webBrowser1.Document.GetElementById("Ddlilce");
                        var dropdownItems = dropdown.Children;


                        foreach (HtmlElement option in dropdownItems)
                        {
                            
                            string IlceTablosu = option.GetAttribute("innerHtml").ToString();
                            SqlIlceVeri += "('" + IlceTablosu + "')" + ",";

                        }
                    }

                    SqlIlceVeri = SqlIlceVeri.Substring(0, SqlIlceVeri.Length - 1);
                    SqlIlceVeri += ";";
                    Mebbis.dc(SqlIlceVeri);


                    string SqlGenmudTablo = "CREATE TABLE IF NOT EXISTS genel_mudurlukler (Id INT PRIMARY KEY AUTO_INCREMENT,GenelMudurlukAdi VARCHAR(100)) COLLATE = UTF32_TURKISH_CI;";
                    Mebbis.dc(SqlGenmudTablo);
                    truncateSabitTablolar = "TRUNCATE TABLE genel_mudurlukler";
                    string SqlGenmudVeri = "INSERT INTO genel_mudurlukler (GenelMudurlukAdi) VALUES";
                    if (webBrowser1.Document != null)
                    {

                        var dropdown = webBrowser1.Document.GetElementById("Ddlgm");
                        var dropdownItems = dropdown.Children;


                        foreach (HtmlElement option in dropdownItems)
                        {
                            
                            string GenmudTablosu = option.GetAttribute("innerHtml").ToString();
                            SqlGenmudVeri += "('" + GenmudTablosu + "')" + ",";

                        }
                    }

                    SqlGenmudVeri = SqlGenmudVeri.Substring(0, SqlGenmudVeri.Length - 1);
                    SqlGenmudVeri += ";";
                    Mebbis.dc(SqlGenmudVeri);


                    string SqlKurTurTablo = "CREATE TABLE IF NOT EXISTS kurum_tur (Id INT PRIMARY KEY AUTO_INCREMENT,KurumTurAdi VARCHAR(100)) COLLATE = UTF32_TURKISH_CI;";
                    Mebbis.dc(SqlKurTurTablo);
                    truncateSabitTablolar = "TRUNCATE TABLE kurum_tur";
                    string SqlKurTurVeri = "INSERT INTO kurum_tur (KurumTurAdi) VALUES";
                    if (webBrowser1.Document != null)
                    {

                        var dropdown = webBrowser1.Document.GetElementById("Ddlalttur");
                        var dropdownItems = dropdown.Children;


                        foreach (HtmlElement option in dropdownItems)
                        {
                           
                            string KurTurTablosu = option.GetAttribute("innerHtml").ToString();
                            SqlKurTurVeri += "('" + KurTurTablosu + "')" + ",";

                        }
                    }

                    SqlKurTurVeri = SqlKurTurVeri.Substring(0, SqlKurTurVeri.Length - 1);
                    SqlKurTurVeri += ";";
                    Mebbis.dc(SqlKurTurVeri);


                    sayac4 = 1;
                    SayacSayisiKayit();
                    webBrowser1.Document.InvokeScript("__doPostBack", args4);
                }
                else
                {
                    webBrowser1.Document.InvokeScript("__doPostBack", args4);

                }

            }

            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 7)
            {
                sorguAlani = 8;
                if (webBrowser1.Document != null)
                {
                    HtmlElementCollection selectElems = webBrowser1.Document.GetElementsByTagName("body");
                    foreach (HtmlElement selElem in selectElems)
                    {
                        html += selElem.OuterHtml.ToString();
                        SorguAdi(html);
                    }

                }
                webBrowser1.Document.InvokeScript("__doPostBack", args4);
            }



            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 8)
            {
                sorguAlani = 9;
                if (webBrowser1.Document != null)
                {
                    HtmlElementCollection selectElems = webBrowser1.Document.GetElementsByTagName("body");
                    foreach (HtmlElement selElem in selectElems)
                    {
                        html += selElem.OuterHtml.ToString();
                        KurumBaslikBul(html);
                    }

                }
                webBrowser1.Document.InvokeScript("__doPostBack", args4);
            }


            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 9)
            {
                sorguAlani = 10;
                TabloOlustur(kurumDevamSQLString);
                Array.Clear(kurumBaslikDevamDizisi, 0, kurumBaslikDevamDizisi.Length);
                Array.Resize(ref kurumBaslikDevamDizisi, 0);

                webBrowser1.Document.InvokeScript("__doPostBack", args4);
            }

            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 10)
            {
                sorguAlani = 11;
                string truncateString = "TRUNCATE TABLE ist_" + sorguNo;
                Mebbis.dc(truncateString);

                if (webBrowser1.Document != null)
                {
                    HtmlElementCollection selectElems = webBrowser1.Document.GetElementsByTagName("body");
                    foreach (HtmlElement selElem in selectElems)
                    {
                        html += selElem.OuterHtml.ToString();
                        KelimeAyikla(html);
                    }
                }
                webBrowser1.Document.InvokeScript("__doPostBack", args4);

            }
            else if (webBrowser1.Url.AbsoluteUri == "https://mebbis.meb.gov.tr/MeisSorgu/MSM01002.aspx" && sorguAlani == 11)
            {
                sayac3++;
                sorguAlani = 2;
                if (sayac3 == modul3.All.Count)
                {
                    sayac2++;
                    sorguAlani = 1;
                    sayac3 = 1;
                }
                if (sayac2 == modul2.All.Count)
                {
                    sayac1++;
                    sorguAlani = 0;
                    sayac2 = 1;
                }
                SayacSayisiKayit();
                webBrowser1.Document.InvokeScript("__doPostBack", args4);
            }
        }


        private void IlSecme()
        {
            sorguAlani = 4;
            HtmlElement modul = webBrowser1.Document.GetElementById("Ddlil");
            modul.SetAttribute("selectedIndex", "2");
            modul.RaiseEvent("onChange");
            object[] args4 = { "someparameters" };
            webBrowser1.Document.InvokeScript("__doPostBack", args4);
        }
        private void CheckboxSecme()
        {
            sorguAlani = 5;

            webBrowser1.Document.GetElementById("Chkilce").SetAttribute("checked", "checked");
            webBrowser1.Document.GetElementById("Chkgm").SetAttribute("checked", "checked");
            webBrowser1.Document.GetElementById("Chkkt").SetAttribute("checked", "checked");
            webBrowser1.Document.GetElementById("Chkkrm").SetAttribute("checked", "checked");
            webBrowser1.Document.GetElementById("Chkalt").SetAttribute("checked", "checked");
            webBrowser1.Document.InvokeScript("__doPostBack", args4);
        }
        private void Sorgula()
        {
            sorguAlani = 6;
            webBrowser1.Document.GetElementById("cmdListele").InvokeMember("click");
        }

        private void KelimeAyikla(string html)
        {
            string il = "", ilce = "", genmud = "", kurtur = "", kurkod = "", kurad = "", kuralttur = "";
            string kurumKoduKelimesi = "";
            string trTagi = "<tr>";
            string tdTagi = "<td>";
            string tdKapanisTagi = "</td>";
            int TrYeri = 0;
            int kurumKoduYeri = 0;
            int tdAcilisYeri, tdKapanisYeri;

            string bitisKelimesi = "lblverisayi";
            int bitisSayisi = html.IndexOf(bitisKelimesi);

            kurumKoduKelimesi = "KURUM_KODU</td>";
            kurumKoduYeri = html.IndexOf(kurumKoduKelimesi);
            TrYeri = html.IndexOf(trTagi, kurumKoduYeri + 1);


            //Tur Başlıyor **********************************************************

            while ((kurumKoduYeri > 120) && (TrYeri < bitisSayisi))
            {
                int sonrakiTr = html.IndexOf(trTagi, TrYeri + 1);

                string sqlVeriEkle = "INSERT INTO ist_" + sorguNo + "(" + alanAdlari + ") VALUES('";

                #region İL
                //İL
                tdAcilisYeri = html.IndexOf(tdTagi, TrYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                il = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                #endregion

                ////İLÇE
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                ilce = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();

                string ilceId = IlceIdBul(ilce);
                sqlVeriEkle += ilceId + "','";


                ////GENEL MÜDÜRLÜK

                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                genmud = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                string genmudId = GenelMudurlukIdBul(genmud);
                sqlVeriEkle += genmudId + "','";

                ////KURUM TÜRÜ
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurtur = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                string kurturId = KurumTurBul(kurtur);
                sqlVeriEkle += kurturId + "','";

                ////KURUM KODU
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurkod = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                sqlVeriEkle += kurkod + "','";


                ////KURUM ADI
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurad = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                sqlVeriEkle += kurad + "','";

                #region KURUM ALT TÜR
                //////KURUM ALT TÜR
                //tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                //tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                //kuralttur = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                //string kuraltturId = KurumAltTurBul(kuralttur);
                //sqlVeriEkle += kuralttur + "','";
                #endregion


                while (tdKapanisYeri < sonrakiTr)
                {
                    tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                    int tdKapanisOnceki = tdKapanisYeri;
                    tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                    if (tdKapanisYeri < sonrakiTr)
                    {
                        kelimeAyiklaDevam = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                        kelimeAyiklaDevam = kelimeAyiklaDevam.Replace("'", "`");
                        sqlVeriEkle += kelimeAyiklaDevam + "','";
                    }
                    else
                    {
                        tdKapanisYeri = tdKapanisOnceki;
                        break;
                    }

                }
                sqlVeriEkle = sqlVeriEkle.Substring(0, sqlVeriEkle.Length - 2);
                sqlVeriEkle += ");";
                Mebbis.dc(sqlVeriEkle);

                // SONRAKİ SATIR
                TrYeri = html.IndexOf(trTagi, tdKapanisYeri + 1);
                
            }
        }

        private string IlceIdBul(string ilce)
        {
            SqlString = "SELECT Id FROM ilceler WHERE IlceAdi='" + ilce + "'";
            DataSet dsilce = Mebbis.ds(SqlString);

            return dsilce.Tables[0].Rows[0][0].ToString();
        }   

        private string GenelMudurlukIdBul(string genmud)
        {
            SqlString = "SELECT Id FROM genel_mudurlukler WHERE GenelMudurlukAdi='" + genmud + "'";
            DataSet dsgenmud = Mebbis.ds(SqlString);

            if (dsgenmud.Tables[0].Rows.Count == 0)
            {
                string eksikGenMud = "INSERT INTO genel_mudurlukler (GenelMudurlukAdi) VALUES('" + genmud + "');";
                Mebbis.dc(eksikGenMud);
                SqlString = "SELECT Id FROM genel_mudurlukler WHERE GenelMudurlukAdi='" + genmud + "'";
                dsgenmud = Mebbis.ds(SqlString);                
                return dsgenmud.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return dsgenmud.Tables[0].Rows[0][0].ToString();
            }

        }
        private string KurumTurBul(string kurtur)
        {
            SqlString = "SELECT Id FROM kurum_tur WHERE KurumTurAdi='" + kurtur + "'";
            DataSet dskurtur = Mebbis.ds(SqlString);

            if (dskurtur.Tables[0].Rows.Count == 0)
            {
                string eksikGenMud = "INSERT INTO kurum_tur (KurumTurAdi) VALUES('" + kurtur + "');";
                Mebbis.dc(eksikGenMud);
                SqlString = "SELECT Id FROM kurum_tur WHERE KurumTurAdi='" + kurtur + "'";
                dskurtur = Mebbis.ds(SqlString);
                return dskurtur.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return dskurtur.Tables[0].Rows[0][0].ToString();
            }
        }

        //private string KurumAltTurBul(string kuralttur)
        //{
        //    SqlString = "SELECT Id FROM kurum_alttur WHERE KurumAltTurAdi='" + kuralttur + "'";
        //    DataSet dskuralttur = Mebbis.ds(SqlString);

        //    return dskuralttur.Tables[0].Rows[0][0].ToString();

        //}
        private void KurumBaslikBul(string html)
        {
            string baslangicNoktasi = "";
            string trKapanisTagi = "</tr>";
            string tdTagi = "<td>";
            string tdKapanisTagi = "</td>";
            int TdYeri = 0;
            int baslangicYeri = 0;
            string baslangicNoktası2 = "";
            int baslangicYeri2 = 0;
            int tdAcilisYeri, tdKapanisYeri;
            alanAdlari = "";



            string bitisKelimesi = "KURUM_KODU";
            int bitisKelimesiYeri = html.IndexOf(bitisKelimesi);
            if (bitisKelimesiYeri != -1)
            {
                int bitisSayisi = html.IndexOf(trKapanisTagi, bitisKelimesiYeri);

                baslangicNoktasi = "DataGrid1";
                baslangicYeri = html.IndexOf(baslangicNoktasi);
                TdYeri = html.IndexOf(tdTagi, baslangicYeri);


                ////İL
                tdAcilisYeri = html.IndexOf(tdTagi, TdYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurumBaslıkIl = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();

                ////İLÇE
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurumBaslikIlce = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                alanAdlari += kurumBaslikIlce + ",";

                ////GENEL MÜDÜRLÜK

                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurumBaslikGenelMudurluk = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                alanAdlari += kurumBaslikGenelMudurluk + ",";

                ////KURUM TÜRÜ
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurumBaslikKurumTur = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                alanAdlari += kurumBaslikKurumTur + ",";



                ////KURUM KODU
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurumBaslikKurumKodu = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                alanAdlari += kurumBaslikKurumKodu + ",";


                ////KURUM ADI
                tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                kurumBaslikKurumAdi = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                alanAdlari += kurumBaslikKurumAdi + ",";

                #region KURUM ALT TÜR
                //////KURUM ALT TÜR
                ///////////////////////BAZI SORGU ALANLARINDA KURUM ALT TÜRÜ SEÇİLEMEZ BOŞ YERE +4 ATLIYORUZ !!!VERİ KAYBI!!!
                //tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                //tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                //kurumBaslıkkAltTur = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                #endregion

                baslangicNoktası2 = "ALT_TUR";
                baslangicYeri2 = html.IndexOf(baslangicNoktası2);

                while (TdYeri < bitisSayisi)
                {
                    tdAcilisYeri = html.IndexOf(tdTagi, tdKapanisYeri) + 4;
                    tdKapanisYeri = html.IndexOf(tdKapanisTagi, tdAcilisYeri + 1);
                    kurumBaslik = html.Substring(tdAcilisYeri, tdKapanisYeri - tdAcilisYeri).Trim();
                    alanAdlari += kurumBaslik + ",";


                    kurumBaslikDevamDizisi = kurumBaslikDevamDizisi.Append(kurumBaslik).ToArray();

                    // SONRAKİ SATIR
                    TdYeri = html.IndexOf(tdTagi, tdKapanisYeri + 1);
                }

                alanAdlari = alanAdlari.Substring(0, alanAdlari.Length - 1);
                kurumDevamSQLString = "";
                foreach (var item in kurumBaslikDevamDizisi)
                {

                    kurumDevamSQLString += item + " " + "VARCHAR(300) NULL,";
                }
            }
            //Tablonun  başlıgının olmadığı durumlar için
            else
            {
                sorguAlani = 11;
            }
        }
        private void SorguAdi(string html)
        {
            string baslangicNoktasi = "";
            int baslangicYeri = 0;
            string optionTagi = "selected";
            int optionYeri = 0;
            string eksiIsareti = " -";
            int optionAcilisYeri;
            int eksiIsaretiYeri;
            string buyukturIsareti = ">";

            baslangicNoktasi = "cmbSorgu";
            baslangicYeri = html.IndexOf(baslangicNoktasi);
            optionYeri = html.IndexOf(optionTagi, baslangicYeri);

            string bitisKelimesi = "auto-style2";
            int bitisKelimesiYeri = html.IndexOf(bitisKelimesi, baslangicYeri);
            //Tur Başlıyor **********************************************************

            while ((baslangicYeri > 195) && (optionYeri < bitisKelimesiYeri))
            {
                optionAcilisYeri = html.IndexOf(buyukturIsareti, optionYeri) + 1;
                eksiIsaretiYeri = html.IndexOf(eksiIsareti, optionAcilisYeri + 1);
                sorguNo = html.Substring(optionAcilisYeri, eksiIsaretiYeri - optionAcilisYeri).Trim();

                Console.WriteLine(sorguNo);

                // SONRAKİ SATIR
                optionYeri = html.IndexOf(optionTagi, eksiIsaretiYeri + 1);
            }
        }

        private void TabloOlustur(string kurumDevam)
        {
            SqlString = "CREATE TABLE IF NOT EXISTS ist_" + sorguNo + "(" +
                "Id INT PRIMARY KEY AUTO_INCREMENT,"
                + kurumBaslikIlce + " INT NULL,"
                + kurumBaslikGenelMudurluk + " INT NULL,"
                + kurumBaslikKurumTur + " INT NULL,"
                + kurumBaslikKurumKodu + " VARCHAR(9) NULL,"
                + kurumBaslikKurumAdi + " VARCHAR(100) NULL," +
                kurumDevam.TrimEnd(',') +
                ") COLLATE = UTF32_TURKISH_CI";

            Mebbis.dc(SqlString);
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
        private void SayacSayisiKullanma()
        {
            if (System.IO.File.Exists("sayac_values.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("sayac_values.txt");
                if (lines.Length >= 4)
                {
                    sayac1 = int.Parse(lines[0]);
                    sayac2 = int.Parse(lines[1]);
                    sayac3 = int.Parse(lines[2]);
                    sayac4 = int.Parse(lines[3]);
                }
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            webBrowser1.Navigate("https://mebbis.meb.gov.tr/index.aspx");
        }
    }
}