using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace HW_30082023
{
    internal class Program
    {
        private static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        static void Main(string[] args)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Menu(connection);

                    connection.Close();
                    Console.WriteLine("Disconnected from database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection error: {ex.Message}");
            }
        }

        static void Menu(SqlConnection connection)
        {
            
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

                string choice = Console.ReadLine();

                var products = new Dictionary<string, List<string>>();

                switch (choice)
                {
                    case "1":
                        AllInfo(connection);
                        break;
                    case "2":
                        AllNames(connection);
                        break;
                    case "3":
                        AllColors(connection);
                        break;
                    case "4":
                        MaxCalories(connection);
                        break;
                    case "5":
                        MinCalories(connection);
                        break;
                    case "6":
                        AvCalories(connection);
                        break;
                    case "7":
                        numVegetables(connection);
                        break;
                    case "8":
                       numFruits(connection);
                        break;
                    case "9":
                        numVegFruColor(connection, "red");
                        break;
                    case "10":
                        numVegFruAllColor(connection);
                        break;
                    case "11":
                        calorieBelow(connection, 20);
                        break;
                    case "12":
                        calorieMore(connection, 20);
                        break;
                    case "13":
                        calorieInRange(connection, 15, 20);
                        break;
                    case "14":
                        RedOrYellow(connection);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again!");
                        break;
                }
                Console.ReadKey();
            } while (true);


        }
        static void Show(SqlConnection connection, string query, params string[] info)
        { 
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("-----------------------------");
                        for (int i = 0; i < info.Length; i++)
                        {
                            Console.WriteLine($"{reader[info[i]]}");
                        }
                        Console.WriteLine("-----------------------------");
                    }
                }

            }
        }
        static void AllInfo(SqlConnection connection)
        {
            string query = "select * from Vegetables_fruits";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }

        static void AllNames(SqlConnection connection)
        {
            string query = "select * from Vegetables_fruits";

            Show(connection, query, "Name");
        }

        static void AllColors(SqlConnection connection)
        {
            string query = "select * from Vegetables_fruits";

            Show(connection, query, "Color");
        }



        static void MaxCalories(SqlConnection connection)
        {
            string query = "select top 1 * from Vegetables_fruits order by Caloric desc";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }
        static void MinCalories(SqlConnection connection)
        {
            string query = "select top 1 * from Vegetables_fruits order by Caloric asc";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }
        static void AvCalories(SqlConnection connection)
        {
            string query = "select avg(Caloric) as AverageCalories from Vegetables_fruits";

            Show(connection, query, "AverageCalories");
        }
       
        /// ///////
        static void numVegetables(SqlConnection connection)
        {
            string query = "select *  from Vegetables_fruits where Type = 1";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }
        static void numFruits(SqlConnection connection)
        {
            string query = "select *  from Vegetables_fruits where Type = 0";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }
        static void numVegFruColor(SqlConnection connection, string color)
        {
            string query = $"select *  from Vegetables_fruits where Color = '{color}'";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }
        static void numVegFruAllColor(SqlConnection connection)
        {
            string query = "select Color, count(*) AS TotalCount from Vegetables_fruits group by Color";

            Show(connection, query, "Color", "TotalCount");
        }

        static void calorieBelow(SqlConnection connection, int calorie)
        {
            string query = $"select *  from Vegetables_fruits where Caloric < {calorie}";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }
        static void calorieMore(SqlConnection connection, int calorie)
        {
            string query = $"select *  from Vegetables_fruits where Caloric > {calorie}";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }

        static void calorieInRange(SqlConnection connection, int calorieMin, int calorieMax)
        {
            string query = $"select *  from Vegetables_fruits where Caloric >= {calorieMin} and Caloric <= {calorieMax}";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }

        static void RedOrYellow(SqlConnection connection)
        {
            string query = $"select *  from Vegetables_fruits where  Color = 'red' or Color = 'yellow'";

            Show(connection, query, "Name", "Type", "Color", "Caloric");
        }

    }
}