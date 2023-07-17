using System.Data.SqlClient;

namespace SuperSimpleADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=.\\sqlexpress;Initial Catalog=opgaveDB;Integrated Security=True";
            var query = "SELECT Efternavn from P1 where Fornavn = @nameParameter";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nameParameter", "Niels");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0]); // replace 0 with the index of the column you want to read
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}