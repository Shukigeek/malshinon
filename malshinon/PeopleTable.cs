using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class PeopleTable
    {
        DAL dal = new DAL();
        public People SearchInPeopleTable(string firstName, string lastName, string type)
        {
            string query = @"SELECT * FROM People p WHERE p.first_name = @firstName AND p.last_name = @lastName";
            People person = null;
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
                            person = new People(reader.GetInt32("id"), firstName, lastName,
                                reader.GetString("secret_code"),
                                reader.GetString("type"), reader.GetInt32("num_reports"), reader.GetInt32("num_mentions"));
                        }
                        if (person == null)
                        {
                            reader.Close();
                            person = AddPeople(firstName, lastName, type);
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
            }
            return person;

        }
        public People AddPeople(string firstName, string lastName, string type)
        {
            string secretCode = new Generate_secret_code().Generate();
            string query = @"INSERT INTO people (first_name, last_name, secret_code, type) " +
                $"VALUES (@firstName, @lastName, @secretCode, @type);" +
                $"SELECT LAST_INSERT_ID();";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
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
            return null;
        }
        public void IncrementColumn(string columnName, int id, People person)
        {
            string query = $"UPDATE people SET {columnName} = {columnName} + 1 WHERE people.id = @id;";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        if (columnName == "num_reports")
                            person.NumReports += 1;
                        if (columnName == "num_mentions")
                            person.NumMentions += 1;
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
        }

        public void IncrementNumMentions(int id, People person) => IncrementColumn("num_mentions", id, person);
        public void IncrementNumReports(int id, People person) => IncrementColumn("num_reports", id, person);
        public void UpdateType(int id, string type, People person)
        {
            string query = @"UPDATE people SET type = @type WHERE people.id = @id;";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, dal.openConnection()))
                {
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("update type");
                        person.Type = type;
                    }
                    else
                    {
                        Console.WriteLine("type didnet update.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating person type: {ex.Message}");
            }
        }
    }
}
