using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace malshinon
{
    internal class DAL2
    {
        DAL dal = new DAL();
        public void GetPersonByName(string firstName,string lastName)
        {
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
                            Console.WriteLine(reader.GetString("first_name"), reader.GetString("last_name"),
                                reader.GetString("secret_cade"), reader.GetString("type"),
                                reader.GetString("num_reports"), reader.GetString("num_mentions"));
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error faching data on {firstName} {lastName}: {ex.ToString()}");
            }

        }
        public void GetPersonBySecretCode(string secretCode)
        {
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
                            Console.WriteLine(reader.GetString("first_name"), reader.GetString("last_name"),
                                reader.GetString("secret_cade"), reader.GetString("type"),
                                reader.GetString("num_reports"), reader.GetString("num_mentions"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error faching data on {secretCode}: {ex.ToString()}");
            }

        }

        //InsertIntelReport()
        //CreateAlert()
        //GetAlerts()

    }
}
