using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class AnalysisMenu
    {
        DAL dal = new DAL();
        //public void Manu()
        //{
        //    Console.WriteLine("Analysis manu:\nchoose your option from 1-4");
        //    while (true)
        //    {
        //        potential agents
        //    }
        //}


        public void GetPotnetailAgents()
        {
            string query = "SELECT * FROM people p WHERE p.type = 'potential_agent'";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("{0,-5} {1,-12} {2,-12} {3,-15} {4,-12} {5,-14}", 
                            "ID", "First Name", "Last Name", "Secret Code", "Reports", "Mentions");
                        Console.WriteLine(new string('-', 75));
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string firstName = reader.GetString("first_name");
                            string lastName = reader.GetString("last_name");
                            string secretCode = reader.GetString("secret_code");
                            string type = reader.GetString("type");
                            int numReports = reader.GetInt32("num_reports");
                            int numMention = reader.GetInt32("num_mentions");

                            Console.WriteLine("{0,-5} {1,-12} {2,-12} {3,-15} {4,-12} {5,-14}",
                                id, firstName, lastName, secretCode, numReports, numMention);
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
            string query = "SELECT * FROM people p WHERE p.num_mentions >= 20";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("{0,-5} {1,-12} {2,-12} {3,-15} {4,-12} {5,-14}",
                            "ID", "First Name", "Last Name", "Secret Code", "Reports", "Mentions");
                        Console.WriteLine(new string('-', 75));
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string firstName = reader.GetString("first_name");
                            string lastName = reader.GetString("last_name");
                            string secretCode = reader.GetString("secret_code");
                            string type = reader.GetString("type");
                            int numReports = reader.GetInt32("num_reports");
                            int numMention = reader.GetInt32("num_mentions");

                            Console.WriteLine("{0,-5} {1,-12} {2,-12} {3,-15} {4,-12} {5,-14}",
                                id, firstName, lastName, secretCode, numReports, numMention);
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error geting potnetial agents: {ex.Message}");
            }


        }
        //requerments not mach



    }
}
