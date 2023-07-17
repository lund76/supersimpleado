using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace SuperSimpleADO
{
    internal class Program
    {
        static void Main(string[] args)
        {


            DoItWithEfCore();
            Console.ReadKey();

            return;
            var connectionString = "Data Source=.\\sqlexpress;Initial Catalog=opgaveDB;Integrated Security=True";
            var query = "SELECT top 100 * from P1 where Fornavn = 'Niels' ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@nameParameter", "Niels");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[1] + " " + reader[2]); // replace 0 with the index of the column you want to read
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        public static void DoItWithEfCore()
        {
            using (var db = new PersonDatabaseContext())
            {
                var query =
                    from row in db.Persons
                    where row.Fornavn == "Niels"
                    select row;

                foreach (var row in query)
                {
                    Console.WriteLine(row.Efternavn + " " + row.PersonId.ToString());
                }
            }

            Console.ReadKey();
        }
    }


    public class PersonDatabaseContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress;Initial Catalog=opgaveDB;Integrated Security=True;Encrypt=false");
        }
    }

    [Table("Person")]
    public class Person
    {
        public int PersonId { get; set; } 
        public string Fornavn { get; set; } 
        public string Efternavn { get; set; }
        public short Postnr { get; set; }
        
    }
}