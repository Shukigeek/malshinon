using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class DALreport
    {
        DALconnction dal = new DALconnction();
        public Report InsertReport(int reporter_id, int target_id, string full_report)
        {
            DateTime timeStamp = DateTime.Now;
            string query = @"INSERT INTO IntelReports (reportr_id,target_id,text,timestamp)" +
                "VALUES (@reporterID,@targetID,@fullReport,@timeStamp)";
            Report report;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@reporterID", reporter_id);
                    cmd.Parameters.AddWithValue("@targetID", target_id);
                    cmd.Parameters.AddWithValue("@fullReport", full_report);
                    cmd.Parameters.AddWithValue("@timeStamp", timeStamp);

                    object result = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(result);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("report added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No report was added.");
                    }
                    return report = new Report(newId, reporter_id, target_id, full_report, timeStamp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding report: {ex.Message}");
            }
            return null;
        }
    }
}
