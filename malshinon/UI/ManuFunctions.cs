using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon.UI
{
    internal class ManuFunctions
    {
        DALconnction dal = new DALconnction();
        public void GetPotnetailAgents()
        {
            string query = "SELECT CONCAT(p.first_name, ' ', p.last_name) AS 'full name',p.num_reports,AVG(CHAR_LENGTH(i.text)) AS 'avrege length' FROM people p JOIN intelreports i WHERE p.type = 'potential_agent' GROUP BY p.id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool headLine = true;

                        while (reader.Read())
                        {
                            if (headLine)
                            {
                                Console.WriteLine("{0,-15} {1,10} {2,17}",
                                "Full Name", "Reports Count", "Average Length");
                                Console.WriteLine(new string('-', 47));
                            }
                            headLine = false;
                            string fullName = reader.GetString("full name");
                            int numReports = reader.GetInt32("num_reports");
                            double avg = reader.GetInt32("avrege length");

                            Console.WriteLine("{0,-15} {1,10:D} {2,17:F2}",
                                fullName, numReports, avg);
                            Console.WriteLine();
                        }
                        if (headLine)
                        {
                            Console.WriteLine("there are no potnetail agent");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error geting potnetial agents: {ex.Message}");
            }


        }
        public void GetDangerousTargets()
        {
            string query = "SELECT CONCAT(p.first_name, ' ', p.last_name) AS 'full name',p.num_mentions FROM people p WHERE p.num_mentions >= 20";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool headLine = true;
                        while (reader.Read())
                        {
                            if (headLine)
                            {
                                Console.WriteLine("{0,-15} {1,10}", "Full Name", "Mentions");
                                Console.WriteLine(new string('-', 26));
                            }
                            headLine = false;
                            string fullName = reader.GetString("full name");
                            int numMention = reader.GetInt32("num_mentions");

                            Console.WriteLine("{0,-15} {1,10}", fullName, numMention);
                            Console.WriteLine();
                        }
                        if (headLine)
                        {
                            Console.WriteLine("There are no dangerous targets");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error geting potnetial agents: {ex.Message}");
            }


        }
        public void GetActiveAlert()
        {
            string query = "SELECT CONCAT(p.first_name,' ',p.last_name) AS 'target name' " +
                ",a.created_at AS 'time stamp', a.reason AS description FROM alerts a " +
                "JOIN people p ON a.target_id = p.id " +
                "WHERE 'time stamp' BETWEEN NOW() -INTERVAL 15 MINUTE AND NOW(); ";
            MySqlDataReader reader = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        bool headLine = true;
                        while (reader.Read())
                        {
                            if (headLine)
                            {
                                Console.WriteLine("{0,-15} {1,10} {2,17}",
                                "target Name", "time stamp", "descrition");
                                Console.WriteLine(new string('-', 47));
                            }
                            headLine = false;
                            string targetName = reader.GetString("target name");
                            DateTime timeStamp = reader.GetDateTime("time stamp");
                            string description = reader.GetString("description");
                            Console.WriteLine("{0,-15} {1,10} {2,17}",
                                targetName, timeStamp, description);
                            Console.WriteLine();
                        }
                        if (headLine)
                        {
                            Console.WriteLine("there are no active aleters from the past 15 nimute");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error geting active alerts: {ex.Message}");
            }
        }
        public void GetPersonByName()
        {
            Console.WriteLine("enter first name:");
            string firstName = Console.ReadLine(); 
            Console.WriteLine("enter last name:");
            string lastName = Console.ReadLine(); 
            string query = "SELECT * FROM people p WHERE p.first_name = @firstName AND p.last_name = @lastName";
            MySqlDataReader reader = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"name: {reader.GetString("first_name")} {reader.GetString("last_name")} " +
                                $"secret code: {reader.GetString("secret_code")} type: {reader.GetString("type")} " +
                                $"num reporets: {reader.GetInt32("num_reports")} num mentions: {reader.GetInt32("num_mentions")}");
                        }
                        else
                        {
                            Console.WriteLine("no name found!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error faching data on {firstName} {lastName}: {ex.ToString()}");
            }

        }
        public void GetPersonBySecretCode()
        {
            Console.WriteLine("enter the secret code");
            string secretCode = Console.ReadLine();
            string query = "SELECT * FROM people p WHERE p.secret_code = @secretCode";
            MySqlDataReader reader = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@secretCode", secretCode);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"name: {reader.GetString("first_name")} {reader.GetString("last_name")}" +
                                $"secret code: {reader.GetString("secret_cade")} type: {reader.GetString("type")} " +
                                $"numreporets: {reader.GetInt32("num_reports")} nummentions: {reader.GetInt32("num_mentions")}");
                        }
                        else 
                        {
                            Console.WriteLine("secter code not found!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error faching data on {secretCode}: {ex.ToString()}");
            }

        }
    }
}
