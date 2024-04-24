using API_ZAD6.Models;
using Microsoft.Data.SqlClient;

namespace API_ZAD6.Repositories
{
    public class AnimalsRepository
    {
        private readonly string _connectionString;

        public AnimalsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Description, Category, Area FROM Animals";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Animal animal = new Animal
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Category = reader["Category"].ToString(),
                            Area = reader["Area"].ToString()
                        };

                        animals.Add(animal);
                    }

                    reader.Close();
                }
            }

            return animals;
        }

        public void AddAnimal(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Animals (Name, Description, Category, Area) " +
                               "VALUES (@Name, @Description, @Category, @Area)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", animal.Description);
                    command.Parameters.AddWithValue("@Category", animal.Category);
                    command.Parameters.AddWithValue("@Area", animal.Area);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
