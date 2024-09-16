using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Data.Common;

namespace MebbisStaj
{
    class Mebbis
    {

        //private static OdbcConnection conn = new OdbcConnection("DRIVER={MySQL ODBC 5.1 Driver}; SERVER=localhost; DATABASE=adabis; UID=root; PASSWORD=1353; providerName='System.Data.Odbc'");
        //private static OdbcConnection conn = new OdbcConnection("DRIVER={MySQL ODBC 5.1 Driver}; DSN=CAN; UID=root; PWD=1353;");
        //private static OdbcConnection conn = new OdbcConnection("DRIVER={MySQL ODBC 5.1 Driver}; SERVER=127.0.0.1; DATABASE=adabis_kontrol; UID=root; PASSWORD=1353; Port=3306;");
          private static OdbcConnection conn=new OdbcConnection("DRIVER={MySQL ODBC 8.0 Unicode Driver}; SERVER=localhost; UID=root; PASSWORD=123456; Port=3306;");


        public static void Baglanti(string veriTabaniIsmi)
        {
            string veriTabanliBaglanti = $"DRIVER={{MySQL ODBC 8.0 Unicode Driver}}; SERVER=localhost; DATABASE={veriTabaniIsmi}; UID=root; PASSWORD=123456; Port=3306;";
            conn = new OdbcConnection(veriTabanliBaglanti);

      }
        
        public static bool exc = false;


    public static DataSet ds(string SqlString)
        {
            OdbcCommand cmdDataSet = conn.CreateCommand();
            cmdDataSet.CommandText = SqlString;

            OdbcDataAdapter daDataSet = new OdbcDataAdapter(cmdDataSet);
            DataSet dsDataSet = new DataSet();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            daDataSet.Fill(dsDataSet);
            conn.Close();
            exc = true;
            return dsDataSet;

        }

        public static void dc(string SqlString)
        {
            // try
            // {
            OdbcCommand cmdDataCommand = conn.CreateCommand();

            cmdDataCommand.CommandText = SqlString;
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            cmdDataCommand.ExecuteNonQuery();
            conn.Close();
            exc = true;
            /*}
            catch (OdbcException ex)
            {
                exc = false;
                MessageBox.Show("Hata oluştu! Hata kodu=" + ex.Errors[0].NativeError.ToString());
            }*/
        }

    }
}
