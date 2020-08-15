using System;
using System.Collections.Generic;
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
            WriteToFile("Service Message ! tayfun.com :    " + DateTime.Now);
            try
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Server=.;Database=TurkSeker;User Id=sa;Password=123;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                SqlCommand command = new SqlCommand("Select Kayit_tar_saat from dbo.genelmd where Id=1577", cnn);
                
                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        WriteToFile(reader["Kayit_tar_saat"].ToString()+"   ! " + DateTime.Now);
                       // Console.WriteLine(String.Format("{0}", reader["id"]));
                    }
                }
                WriteToFile("Connection Open  ! " + DateTime.Now);

                cnn.Close();

                WriteToFile("Connection Close  ! " + DateTime.Now);
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message.ToString() + DateTime.Now);
                throw;
            }

        }
    }
}