using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace malshinon.DAL
{
    internal class ThersHolds
    {
        DALconnction dal = new DALconnction();
        public bool IsPotentialAgent(int id)
        {
            string query = "SELECT CONCAT(p.first_name, ' ', p.last_name) AS 'full name' " +
                "FROM people p " +
                "JOIN intelreports i ON p.id = i.reportr_id WHERE p.id = @id " +
                "GROUP BY p.id, p.first_name, p.last_name, p.num_reports " +
                "HAVING COUNT(i.id) >= 10 AND AVG(CHAR_LENGTH(i.text)) >= 100 " +
                "AND p.num_reports >= 10;";
            MySqlDataReader reader = null;
            try
            {
                //dal.openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking potentail agent: {ex.Message}");
            }
            return false;
        }
    }
}
