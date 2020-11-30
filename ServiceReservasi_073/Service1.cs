using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi_073
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        string constring = "Data Source=MSI;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=123";
        SqlConnection connection;
        SqlCommand con;

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi = '"+IDPemesanan+"'";
                connection = new SqlConnection(constring);
                con = new SqlCommand(sql, connection);
                connection.Open();
                con.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "select ID_lokasi, Nama_Lokasi, Deskripsi_full, Kuota from dbo.Lokasi";
                connection = new SqlConnection(constring);
                con = new SqlCommand(sql, connection);
                connection.Open();

                SqlDataReader reader = con.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.IDLokasi = reader.GetString(0);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_Customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "'" + "where ID_Reservasi = '"+IDPemesanan+"'";
                connection = new SqlConnection(constring);
                con = new SqlCommand(sql, connection);
                connection.Open();
                con.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }


        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into Pemesanan values ('" + IDPemesanan + "','" + NamaCustomer + "','" + IDPemesanan + "','" + NoTelpon + "','" + JumlahPesanan + "','" + IDLokasi + "')";
                connection = new SqlConnection(constring);
                con = new SqlCommand(sql, connection);
                connection.Open();
                con.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set Kuota = Kuota - "+JumlahPesanan+" where ID_lokasi = '"+IDLokasi+"'";
                connection = new SqlConnection(constring);
                con = new SqlCommand(sql, connection);
                connection.Open();
                con.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "select ID_reservasi, Nama_customer, No_telpon, " + "Jumlah_pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi 1 on p.ID_lokasi = 1.ID_lokasi";
                connection = new SqlConnection(constring);
                con = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = con.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetString(0);
                    data.Lokasi = reader.GetInt32(3);
                    pemesanans.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            
            return pemesanans;
        }
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }
    }
}
