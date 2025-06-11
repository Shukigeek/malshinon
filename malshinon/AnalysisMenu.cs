using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
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
        Reporter_flow reporter_Flow = new Reporter_flow();
        public void Manu()
        {
            bool online = true;
            int num = 0;
            while (online) {
                Console.WriteLine("Analysis manu:\nchoose your option from 1-5");
                Console.WriteLine("1: add new report\n" +
                    "2: get potnetail agents\n" +
                    "3: get dangerous target\n" +
                    "4: get acvit alerts\n" +
                    "5: exit program");
                bool check = int.TryParse(Console.ReadLine(), out num);
                if (num < 1 || num > 5) Console.WriteLine("enter number between 1 - 5");
                if (check && num > 0 && num < 6) 
                {
                    Console.Clear();
                    switch (num)
                    {
                        case 1:
                            reporter_Flow.PogramFlow();
                            Console.ReadKey();
                            break;
                        case 2:
                            GetPotnetailAgents();
                            Console.ReadKey();
                            break;
                        case 3:
                            GetDangerousTargets();
                            Console.ReadKey();
                            break;
                        case 4:
                            GetActiveAlert();
                            Console.ReadKey();
                            break;
                        case 5:
                            online = false;
                            Console.WriteLine("by!");
                            break;
                    }
                }
             }
        }


        public void GetPotnetailAgents()
        {
            string query = "SELECT CONCAT(p.first_name, ' ', p.last_name) AS 'full name',p.num_reports,AVG(CHAR_LENGTH(i.text)) AS 'avrege length' FROM people p JOIN intelreports i WHERE p.type = 'potential_agent'";
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
    }
}
