using MySql.Data.MySqlClient;
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
        public void SearchInPeopleTable(string firstName, string lastName, string type)
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
                        if (!reader.Read())
                        {
                            reader.Close();
                            AddPeople(firstName, lastName, type);
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
            }

        }
        public void AddPeople(string firstName, string lastName, string type)
        {
            string secretCode = new Generate_secret_code().Generate();
            string query = @"INSERT INTO people (first_name, last_name, secret_code, type) " +
                $"VALUES (@firstName, @lastName, @secretCode, @type)";
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@secretCode", secretCode);
                    cmd.Parameters.AddWithValue("@type", type);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("person added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No person was added.");
                    }

                    //Console.WriteLine("Agent added successfully.");
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
        public void InsertReport(int reporter_id, int target_id, string full_report)
        {
            DateTime timeStamp = DateTime.Now;
            string query = @"INSERT INTO IntelReports (reportr_id,target_id,text,timestamp)" +
                "VALUES (@reporterID,@targetID,@fullReport,@timeStamp)";
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@reporterID", reporter_id);
                    cmd.Parameters.AddWithValue("@targetID", target_id);
                    cmd.Parameters.AddWithValue("@fullReport", full_report);
                    cmd.Parameters.AddWithValue("@timeStamp", timeStamp);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("report added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No report was added.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding report: {ex.Message}");
            }
            finally
            {

                closeConnection();
            }
        }
        public void IncrementColumn(string columnName ,int id)
        {
            string query = @"UPDATE people SET @num_mentions = @num_mentions + 1 WHERE people.id = @id;";
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@num_mentions", columnName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("increment report added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No increment was added.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating person: {ex.Message}");
            }
            finally
            {

                closeConnection();
            }
        }
        public void IncrementNumMentions(int id) => IncrementColumn("num_mentions", id);
        public void IncrementNumReports(int id) => IncrementColumn("num_mentions", id);
    }
}
