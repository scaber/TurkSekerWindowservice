using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VeriAktarimi
{
    public class TurkSekerVeriAktar
    {
        Timer timer = new Timer();
        public void Start()
        {
            WriteToFile("Service Başladı " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; // every 5  seconds
            timer.Enabled = true;
        }
        public void Stop()
        {
            WriteToFile("Service Durduruldu " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service Message ! tayfun.com : Malatya    " + DateTime.Now);
            try
            {

                MalatyaInsertData();

                WriteToFile("Connection Open  ! " + DateTime.Now);

            

                WriteToFile("Connection Close  ! " + DateTime.Now);
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message.ToString() + DateTime.Now);
                throw;
            }

        }
        private static void MalatyaInsertData()
        {
            SqlConnection cnn = new SqlConnection(@"Server=.;Database=TurkSeker;User Id=sa;Password=123;");

            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            } 
            SqlCommand command = new SqlCommand("SELECT TOP 1 ObjectId FROM dbo.genelmd where FabrikaId=1 ORDER BY ID DESC ", cnn);
            int ObjectId = 0;
            using (SqlDataReader reader = command.ExecuteReader())
            { 
                if (reader.Read())
                {
                    ObjectId = (int)reader["ObjectId"];
                }
            } 
            List<ListGenelmd> test = MalatyaVeriAktar(ObjectId);
            foreach (var item in test)
            {
                try
                {
                    string sb = "INSERT INTO [dbo].[genelmd]" +
              "([FabrikaId]" +
              ",[Kayit_tar_saat]" +
              ",[Kiyim1_anlik_ton]" +
              ",[Kiyim2_anlik_ton]" +
              ",[Kiyim3_anlik_ton]" +
              ",[Ham_bek_sek]" +
              ",[Raf_bek_sek]" +
              ",[Top_bek_sek]" +
              ",[Bugun_cik_sek]" +
              ",[Dun_cik_sek]" +
              ",[Bugun_kes_pan]" +
              ",[Dun_kes_pan]" +
              ",[Kiyim_polu]" +
              ",[ToplamPancar]" +
              ",[BugunGelenPancar]" +
              ",[KantardakiPancar]" +
              ",[ObjectId])" +
              "VALUES" +
              "(@FabrikaId, " +
              " @Kayit_tar_saat, " +
              " @Kiyim1_anlik_ton,  " +
              " @Kiyim2_anlik_ton, " +
              " @Kiyim3_anlik_ton, " +
              " @Ham_bek_sek, " +
              " @Raf_bek_sek,  " +
              " @Top_bek_sek, " +
              " @Bugun_cik_sek, " +
              " @Dun_cik_sek, " +
              " @Bugun_kes_pan, " +
              " @Dun_kes_pan," +
              " @Kiyim_polu," +
              " @ToplamPancar," +
              " @BugunGelenPancar," +
              " @KantardakiPancar," +
              " @ObjectId)";

                    using (SqlCommand cmd = new SqlCommand(sb, cnn))
                    {
                        cmd.Parameters.AddWithValue("@FabrikaId", item.FabrikaId);
                        cmd.Parameters.AddWithValue("@Kayit_tar_saat", item.Kayit_tar_saat);
                        cmd.Parameters.AddWithValue("@Kiyim1_anlik_ton", item.Kiyim1_anlik_ton);
                        cmd.Parameters.AddWithValue("@Kiyim2_anlik_ton", item.Kiyim2_anlik_ton);
                        cmd.Parameters.AddWithValue("@Kiyim3_anlik_ton", item.Kiyim3_anlik_ton);
                        cmd.Parameters.AddWithValue("@Ham_bek_sek", item.Ham_bek_sek);
                        cmd.Parameters.AddWithValue("@Raf_bek_sek", item.Raf_bek_sek);
                        cmd.Parameters.AddWithValue("@Top_bek_sek", item.Top_bek_sek);
                        cmd.Parameters.AddWithValue("@Bugun_cik_sek", item.Bugun_cik_sek);
                        cmd.Parameters.AddWithValue("@Dun_cik_sek", item.Dun_cik_sek);
                        cmd.Parameters.AddWithValue("@Bugun_kes_pan", item.Bugun_kes_pan);
                        cmd.Parameters.AddWithValue("@Kiyim_polu", item.Kiyim_polu);
                        cmd.Parameters.AddWithValue("@ToplamPancar", item.ToplamPancar);
                        cmd.Parameters.AddWithValue("@BugunGelenPancar", item.BugunGelenPancar);
                        cmd.Parameters.AddWithValue("@KantardakiPancar", item.KantardakiPancar);
                        cmd.Parameters.AddWithValue("@Dun_kes_pan", item.Dun_kes_pan);
                        cmd.Parameters.AddWithValue("@ObjectId", item.ObjectId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message.ToString());
                }

            } 
            cnn.Close(); 
        }

        private static List<ListGenelmd> MalatyaVeriAktar(int objectId)
        {
            SqlConnection cnnMalatya = new SqlConnection(@"Server=.;Database=TurkSeker;User Id=sa;Password=123;");

            SqlCommand comm = new SqlCommand("SELECT TOP 10500  * FROM GenelmdEmre where id>" + objectId, cnnMalatya);
            if (cnnMalatya.State == ConnectionState.Closed)
            {
                cnnMalatya.Open();

            }

            SqlDataReader reader = comm.ExecuteReader();
            List<ListGenelmd> list = new List<ListGenelmd>();

            while (reader.Read())
            {
                ListGenelmd prm = new ListGenelmd();
                prm.FabrikaId = Convert.ToInt32(1);
                prm.ObjectId = reader["id"] is DBNull ? 0 : Convert.ToInt32(reader["id"]);
                prm.Kayit_tar_saat = Convert.ToDateTime(reader["Kayit_tar_saat"]);
                prm.Kiyim1_anlik_ton = reader["Kiyim1_anlik_ton"] is DBNull ? 0 : Convert.ToDouble(reader["Kiyim1_anlik_ton"]);
                prm.Kiyim2_anlik_ton = reader["Kiyim2_anlik_ton"] is DBNull ? 0 : Convert.ToDouble(reader["Kiyim2_anlik_ton"]);
                //Heryerde olmayacak
                prm.Kiyim3_anlik_ton = Convert.ToDouble(0);
                prm.Ham_bek_sek = reader["Ham_bek_sek"] is DBNull ? 0 : Convert.ToDouble(reader["Ham_bek_sek"]);
                prm.Raf_bek_sek = reader["Raf_bek_sek"] is DBNull ? 0 : Convert.ToDouble(reader["Raf_bek_sek"]);
                prm.Top_bek_sek = reader["Top_bek_sek"] is DBNull ? 0 : Convert.ToDouble(reader["Top_bek_sek"]);
                prm.Bugun_cik_sek = reader["Bugun_cik_sek"] is DBNull ? 0 : Convert.ToDouble(reader["Bugun_cik_sek"]);
                prm.Dun_cik_sek = reader["Dun_cik_sek"] is DBNull ? 0 : Convert.ToDouble(reader["Dun_cik_sek"]);
                prm.Bugun_kes_pan = reader["Bugun_kes_pan"] is DBNull ? 0 : Convert.ToDouble(reader["Bugun_kes_pan"]);
                prm.Dun_kes_pan = reader["Dun_kes_pan"] is DBNull ? 0 : Convert.ToDouble(reader["Dun_kes_pan"]);
                prm.Kiyim_polu = reader["Kiyim_polu"] is DBNull ? 0 : Convert.ToDouble(reader["Kiyim_polu"]);
                //Heryerde olmayacak
                prm.ToplamPancar = Convert.ToDouble(0);
                prm.BugunGelenPancar = Convert.ToDouble(0);
                prm.KantardakiPancar = Convert.ToDouble(0);

                Console.WriteLine(prm.Kiyim1_anlik_ton + "-     " + prm.ObjectId + " - " + prm.Kayit_tar_saat);
                list.Add(prm);
            }
            reader.Close();
            cnnMalatya.Close();
            return list;
        } 
    }
}