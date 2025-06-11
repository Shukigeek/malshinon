using Google.Protobuf.Compiler;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class DAL
    {
        private string connStr = "server=localhost;user=root;password=;database=malshinon";
        private MySqlConnection _conn;
        public MySqlConnection openConnection()
        {
            if (_conn == null)
            {
                _conn = new MySqlConnection(connStr);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _conn.Open();
                Console.WriteLine("Connection successful.");
            }

            return _conn;
        }
        public void closeConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
            }
        }
        public DAL()
        {
            try
            {
                openConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching employees: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                closeConnection();
            }return person;

        }
        public People AddPeople(string firstName, string lastName, string type)
        {
            string secretCode = new Generate_secret_code().Generate();
            string query = @"INSERT INTO people (first_name, last_name, secret_code, type) " +
                $"VALUES (@firstName, @lastName, @secretCode, @type);" +
                $"SELECT LAST_INSERT_ID();";
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@secretCode", secretCode);
                    cmd.Parameters.AddWithValue("@type", type);

                    object result = cmd.ExecuteScalar();
                    int newId = Convert.ToInt32(result);

                    Console.WriteLine("Person added successfully.");

                    return new People(
                        newId,
                        firstName,
                        lastName,
                        secretCode,
                        type,
                        0,  
                        0
                    );
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding person: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
            return null;
        }
        public int FindId(string firstName, string lastName)
        {
            string query = @"SELECT p.id FROM People p WHERE p.first_name = @firstName AND p.last_name = @lastName";
            MySqlDataReader reader = null;
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int num = reader.GetInt32("id");
                            return num;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching people: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                closeConnection();
            }
            return 0;
        }
                   

                closeConnection();
            }
        }
        public void IncrementNumMentions(int id) => IncrementColumn("num_mentions", id);
        public void IncrementNumReports(int id) => IncrementColumn("num_reports", id);
        public bool IsType(int id,string type)
        {
            string query = "SELECT p.type FROM people p WHERE p.id = @id";
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    { 
                        return type == reader.GetString("type");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error geting type: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
            return false;
        }

        }

    }
}
