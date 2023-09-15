using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace HW_02092023_05092023
{
    internal class Program
    {
        private static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        static async Task Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);

            DbProviderFactory factory = null;
            do
            {
                Console.WriteLine("Select Db:\n1 - ms sql\n2 - oracle\n0 - exit");
                string selection = Console.ReadLine();
                if (selection == "1")
                {

                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);

                    try
                    {
                        await Menu(factory);


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Connection error: {ex.Message}");
                    }
                }
                else if (selection == "2")
                {
                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["OracleDb"].ProviderName);

                }
                else if (selection == "0")
                {
                    return;

                }
                else
                {
                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);

                }
                Console.WriteLine("Disconnected from database.");

            } while (true);
            


        }

        static async Task Menu(DbProviderFactory factory)
        {
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                bool exit = false;

                await connection.OpenAsync();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Connected to database 'Vegetables and fruits'.");
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("To display all information        -  enter 1");
                    Console.WriteLine("To display names                  -  enter 2");
                    Console.WriteLine("To display colors                 -  enter 3");
                    Console.WriteLine("To display MaxCalories            -  enter 4");
                    Console.WriteLine("To display MinCalories            -  enter 5");
                    Console.WriteLine("To display AvCalories             -  enter 6");
                    Console.WriteLine("----------------- task 4 -------------------");
                    Console.WriteLine("To display number of vegetables   -  enter 7");
                    Console.WriteLine("To display number of fruits       -  enter 8");
                    Console.WriteLine("To display number of vegetables and fruits\n" +
                                      "   of given color                 -  enter 9");
                    Console.WriteLine("To display number of vegetables and fruits\n" +
                                      "   of each color                  -  enter 10");
                    Console.WriteLine("To display vegetables and fruits with the\n" +
                                      "   calorie below the indicated    -  enter 11");
                    Console.WriteLine("To display vegetables and fruits with more\n" +
                                      "   calories than indicated        -  enter 12");
                    Console.WriteLine("To display veget and fruits with calories in\n" +
                                      "    the specified ranges          -  enter 13");
                    Console.WriteLine("To display vegetables and fruits of\n" +
                                      "   yellow or red color            -  enter 14");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("To update product name            -  enter 15");
                    Console.WriteLine("To delete product                 -  enter 16");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("To change db                       -  enter 0");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await MeasureExecutionTime(() => AllInfo(connection, factory));
                            break;
                        case "2":
                            await MeasureExecutionTime(() => AllNames(connection, factory));
                            break;
                        case "3":
                            await MeasureExecutionTime(() => AllColors(connection, factory));
                            break;
                        case "4":
                            await MeasureExecutionTime(() => MaxCalories(connection, factory));
                            break;
                        case "5":
                            await MeasureExecutionTime(() => MinCalories(connection, factory));
                            break;
                        case "6":
                            await MeasureExecutionTime(() => AvCalories(connection, factory));
                            break;
                        case "7":
                            await MeasureExecutionTime(() => numVegetables(connection, factory));
                            break;
                        case "8":
                            await MeasureExecutionTime(() => numFruits(connection, factory));
                            break;
                        case "9":
                            await MeasureExecutionTime(() => numVegFruColor(connection, factory, "red"));
                            break;
                        case "10":
                            await MeasureExecutionTime(() => numVegFruAllColor(connection, factory));
                            break;
                        case "11":
                            await MeasureExecutionTime(() => calorieBelow(connection, factory, 20));
                            break;
                        case "12":
                            await MeasureExecutionTime(() => calorieMore(connection, factory, 20));
                            break;
                        case "13":
                            await MeasureExecutionTime(() => calorieInRange(connection, factory, 15, 20));
                            break;
                        case "14":
                            await MeasureExecutionTime(() => RedOrYellow(connection, factory));
                            break;
                        case "15":
                            await MeasureExecutionTime(() => UpdateStudentData(connection, factory));
                            break;
                        case "16":
                            await MeasureExecutionTime(() => DeleteStudentData(connection, factory));
                            break;
                        case "0":
                            exit = true;
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Try again!");
                            break;
                    }

                            Console.ReadKey();
                } while (!exit);
                
               
            }
           
        }
        static void Show(DbConnection connection, DbProviderFactory factory, string query, params string[] info)
        {
            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;

                command.CommandText = query;

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < info.Length; i++)
                        {
                            Console.WriteLine($"{reader[info[i]]}");
                        }
                        Console.WriteLine("-------------------------------------------------");
                    }

                }
            }
           
        }

        static async Task MeasureExecutionTime(Func<Task> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await action();
            stopwatch.Stop();
            Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalSeconds} seconds");
        }
        static async Task AllInfo(DbConnection connection, DbProviderFactory factory)
        {

            string query = "select * from Vegetables_fruits";

            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");


        }

        static async Task AllNames(DbConnection connection, DbProviderFactory factory)
        {
            string query = "select * from Vegetables_fruits";

            Show(connection, factory, query, "Name");
            
           
        }

        static async Task AllColors(DbConnection connection, DbProviderFactory factory)
        {
           string query  = "select * from Vegetables_fruits";

            Show(connection, factory, query, "Color");

        }

        static async Task MaxCalories(DbConnection connection, DbProviderFactory factory)
        {
             string query = "select top 1 * from Vegetables_fruits order by Caloric desc";
             Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }
        static async Task MinCalories(DbConnection connection, DbProviderFactory factory)
        {
            string query = "select top 1 * from Vegetables_fruits order by Caloric asc";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

            
        }
        static async Task AvCalories(DbConnection connection, DbProviderFactory factory)
        {
            string query = "select avg(Caloric) as AverageCalories from Vegetables_fruits";
            Show(connection, factory, query, "AverageCalories");

        }

        static async Task numVegetables(DbConnection connection, DbProviderFactory factory)
        {
            string query = "select *  from Vegetables_fruits where Type = 1";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }
        static async Task numFruits(DbConnection connection, DbProviderFactory factory)
        {
            string query = "select *  from Vegetables_fruits where Type = 0";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }
        static async Task numVegFruColor(DbConnection connection, DbProviderFactory factory, string color)
        {
            string query = $"select *  from Vegetables_fruits where Color = '{color}'";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }
        static async Task numVegFruAllColor(DbConnection connection, DbProviderFactory factory)
        {
            string query = "select Color, count(*) AS TotalCount from Vegetables_fruits group by Color";
            Show(connection, factory, query, "Color", "TotalCount");

        }

        static async Task calorieBelow(DbConnection connection, DbProviderFactory factory, int calorie)
        {
            string query = $"select *  from Vegetables_fruits where Caloric < {calorie}";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }
        static async Task calorieMore(DbConnection connection, DbProviderFactory factory, int calorie)
        {
            string query = $"select *  from Vegetables_fruits where Caloric > {calorie}";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }

        static async Task calorieInRange(DbConnection connection, DbProviderFactory factory, int calorieMin, int calorieMax)
        {
            string query = $"select *  from Vegetables_fruits where Caloric >= {calorieMin} and Caloric <= {calorieMax}";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }

        static async Task RedOrYellow(DbConnection connection, DbProviderFactory factory)
        {
            string query = $"select *  from Vegetables_fruits where  Color = 'red' or Color = 'yellow'";
            Show(connection, factory, query, "Name", "Type", "Color", "Caloric");

        }

        static async Task UpdateStudentData(DbConnection connection, DbProviderFactory factory)
        {
            Console.WriteLine("Enter product ID to update:");
            int productId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new data for the product:");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = $"UPDATE Vegetables_fruits SET Name = '{name}' WHERE Id = {productId};";

                int rowsAffected = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"{rowsAffected} rows updated.");
            }
        }

        static async Task DeleteStudentData(DbConnection connection, DbProviderFactory factory)
        {
            Console.WriteLine("Enter product ID to delete:");
            int productId = int.Parse(Console.ReadLine());

            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = $"DELETE FROM Vegetables_fruits WHERE Id = {productId};";

                int rowsAffected = await command.ExecuteNonQueryAsync();
                Console.WriteLine($"{rowsAffected} rows deleted.");
            }
        }
       
    }
}