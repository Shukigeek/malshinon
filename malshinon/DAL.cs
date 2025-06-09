using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public void SearchInPeopleTable(string firstName, string lastName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string query = $"SELECT p.id FROM People p WHERE p.first_name = {firstName} AND p.last_name = {lastName}";

            try
            {
                openConnection();
                cmd = new MySqlCommand(query, _conn);
                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    AddPeople(firstName, lastName);
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
        public void AddPeople(string firstName,string lastName)
        {
            string secretCode = new Generate_secret_code().Generate();
            string query = @"INSERT INTO people (first_name, last_name, secret_cade, type) " +
                $"VALUES (@firstName, @lastName, @secretCode, 'reporter')";
            try
            {
                openConnection();
                using (MySqlCommand cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@secretCode", secretCode);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Agent added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No agent was added.");
                    }

                    Console.WriteLine("Agent added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding agent: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
        }
        
    }
}
