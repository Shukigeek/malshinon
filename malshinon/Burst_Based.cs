using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace malshinon
{
    internal class Burst_Based
    {
        DAL dal = new DAL();
        public void BurstAlerts(int id)
        {
            string query = "SELECT MIN(timestamp) AS first_report_time, " +
                "MAX(timestamp) AS last_report_time " +
                "FROM intelreports i " +
                "WHERE i.target_id = @id " +
                "AND timestamp BETWEEN NOW() -INTERVAL 15 MINUTE AND NOW() " +
                "GROUP BY i.target_id " +
                "HAVING COUNT(*) >= 3; ";
            MySqlDataReader reader = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime firstReportTime = reader.GetDateTime("first_report_time");
                            DateTime lastReportTime = reader.GetDateTime("last_report_time");

                            reader.Close();
                            AddAlert(id, firstReportTime,
                                lastReportTime, "Rapid reports detected");
                            Console.WriteLine("alert creadet from burst_alert");
                        }
                        else
                        {
                            Console.WriteLine("no no no no no no no no =========");
                        }
                    }
                }
            } 
            catch (Exception ex) 
            {
                Console.WriteLine($"Error finding out 3 alerts from the past 15 min.:{ex.ToString()}");
            }
        }
        public void AddAlert(int id,DateTime start_time,DateTime end_time, string reason) 
        {
            
            string query = "INSERT INTO alerts(target_id, " +
                "created_at, start_time, end_time, reason) " +
                "VALUES(@id, NOW(), @start_time, @end_time, @reason)";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@start_time", start_time);
                    cmd.Parameters.AddWithValue("@end_time", end_time);
                    cmd.Parameters.AddWithValue("@reason", reason);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("alert added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No alert was added.");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error adding alert: {ex.Message}");
            }
        }
    }
}
