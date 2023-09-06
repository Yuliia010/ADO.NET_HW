using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Pract_30082023
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
                Console.WriteLine("Connected to database 'Students Marks'.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Display information");
                Console.WriteLine("2. Add a new");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Update");
                Console.WriteLine("5. Task 4");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MenuDisplay(connection);
                        break;
                    case "2":
                        MenuAdd(connection);
                        break;
                    case "3":
                        MenuDelete(connection);
                        break;
                    case "4":
                        MenuUpdate(connection);
                        break;
                    case "5":
                        MenuTask4(connection); 
                        break;
                    case "0":
                        return;
                }
                Console.ReadKey();
            } while (true);

        }


        static void MenuDisplay(SqlConnection connection)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Connected to database 'Students Marks'.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Display all information");
                Console.WriteLine("2. Display types");
                Console.WriteLine("3. Display providers");
                Console.WriteLine("4. Display MaxQuan");
                Console.WriteLine("5. Display MinQuant");
                Console.WriteLine("6. Display MinCost");
                Console.WriteLine("7. Display MaxCost");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AllInfo(connection);
                        break;
                    case "2":
                        AllTypes(connection);
                        break;
                    case "3":
                        AllProvider(connection);
                        break;
                    case "4":
                        MaxQuant(connection);
                        break;
                    case "5":
                        MinQuant(connection);
                        break;
                    case "6":
                        MinCost(connection);
                        break;
                    case "7":
                        MaxCost(connection);
                        break;
                    case "0":
                        return;
                }
                Console.ReadKey();
            } while (true);

        }

        static void MenuAdd(SqlConnection connection)
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Connected to database 'Students Marks'.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Add a new product");
                Console.WriteLine("2. Add a new product type");
                Console.WriteLine("3. Add a new provider");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(connection);
                        break;
                    case "2":
                        AddType(connection);
                        break;
                    case "3":
                        AddProvider(connection);
                        break;
                    case "0":
                        exit = true;
                        break;
                }

                Console.ReadKey();

            } while (!exit);

        }

        static void MenuDelete(SqlConnection connection)
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Connected to database 'Students Marks'.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Delete a product");
                Console.WriteLine("2. Delete a provider");
                Console.WriteLine("3. Delete a product type");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DeleteProduct(connection);
                        break;
                    case "2":
                        DeleteProvider(connection);
                        break;
                    case "3":
                        DeleteType(connection);
                        break;
                    case "0":
                        exit = true;
                        break;
                }

                Console.ReadKey();

            } while (!exit);

        }

        static void MenuUpdate(SqlConnection connection)
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Connected to database 'Students Marks'.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Update product");
                Console.WriteLine("2. Update product type");
                Console.WriteLine("3. Update provider");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UpdateProduct(connection);
                        break;
                    case "2":
                        UpdateType(connection);
                        break;
                    case "3":
                        UpdateProvider(connection);
                        break;
                    case "0":
                        exit = true;
                        break;
                }

                Console.ReadKey();

            } while (!exit);

        }

        static void MenuTask4(SqlConnection connection)
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Connected to database 'Students Marks'.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Provider with most products");
                Console.WriteLine("2. Provider with least products");
                Console.WriteLine("3. Type with most quantity on hand");
                Console.WriteLine("4. Type with least quantity on hand");
                Console.WriteLine("5. Products with delivery date older than");
                Console.WriteLine("0. Exit");
                Console.WriteLine("-------------------------------------------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProviderWithMostProducts(connection);
                        break;
                    case "2":
                        ProviderWithLeastProducts(connection);
                        break;
                    case "3":
                        TypeWithMostQuantityOnHand(connection);
                        break;
                    case "4":
                        TypeWithLeastQuantityOnHand(connection);
                        break;
                    case "5":
                        ProductsWithDeliveryDateOlderThan(connection, 10);
                        break;
                    case "0":
                        exit = true;
                        break;
                }

                Console.ReadKey();

            } while (!exit);

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
            string query =   @"SELECT P.Id,
                              P.Name AS ProductName,
                              T.Name AS TypeName,
                              PR.Name AS ProviderName,
                              P.Quantity,
                              P.Cost,
                              P.DeliveryDate
                              FROM Product AS P
                              JOIN Type AS T ON P.TypeId = T.Id
                              JOIN Provider AS PR ON P.ProviderId = PR.Id";
           

            Show(connection, query,"Id", "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }


        static void AllTypes(SqlConnection connection)
        {
            string query = @" SELECT Id, Name FROM Type ";

            Show(connection, query, "Id", "Name");
            
        }


        static void AllProvider(SqlConnection connection)
        {
            string query = @" SELECT Id, Name FROM Provider ";

            Show(connection, query, "Id", "Name");
        }

        static void MaxQuant(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Quantity DESC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void MinQuant(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Quantity ASC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void MaxCost(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Cost DESC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void MinCost(SqlConnection connection)
        {
            string query = @"
                            SELECT TOP 1
                                P.Id,
                                P.Name AS ProductName,
                                T.Name AS TypeName,
                                PR.Name AS ProviderName,
                                P.Quantity,
                                P.Cost,
                                P.DeliveryDate
                            FROM Product AS P
                            JOIN Type AS T ON P.TypeId = T.Id
                            JOIN Provider AS PR ON P.ProviderId = PR.Id
                            ORDER BY P.Cost ASC";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }

        static void AddProduct(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter the details of the new product:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Type ID: ");
            int typeId = int.Parse(Console.ReadLine());

            Console.Write("Provider ID: ");
            int providerId = int.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Cost: ");
            decimal cost = decimal.Parse(Console.ReadLine());

            Console.Write("Delivery Date (YYYY-MM-DD): ");
            string deliveryDate = Console.ReadLine();

            string query = $"INSERT INTO Product (Name, TypeId, ProviderId, Quantity, Cost, DeliveryDate)"+
                           $"VALUES ('{name}', {typeId}, {providerId}, {quantity}, {cost}, '{deliveryDate}')";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product added successfully!");
            }
        }

        static void AddType(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the new product type:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            string query = $"INSERT INTO Type (Name) VALUES ('{name}')";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
               
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product type added successfully!");
            }
        }

        static void AddProvider(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the new provider:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            string query = $"INSERT INTO Provider (Name) VALUES ('{name}')";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();

                Console.WriteLine("Provider added successfully!");
            }
        }

        static void UpdateProduct(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter the ID of the product to update: ");
            int productId = int.Parse(Console.ReadLine());

            string selectQuery = $"SELECT * FROM Product WHERE Id = {productId}";

            using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
            {
                using (var reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Current Product Name: {reader["Name"]}");
                        Console.Write("Enter new Product Name: ");
                        string newName = Console.ReadLine();

                        Console.WriteLine($"Current Type ID: {reader["TypeId"]}");
                        Console.Write("Enter new Type ID: ");
                        int newTypeId = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Current Provider ID: {reader["ProviderId"]}");
                        Console.Write("Enter new Provider ID: ");
                        int newProviderId = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Current Quantity: {reader["Quantity"]}");
                        Console.Write("Enter new Quantity: ");
                        int newQuantity = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Current Cost: {reader["Cost"]}");
                        Console.Write("Enter new Cost: ");
                        decimal newCost = decimal.Parse(Console.ReadLine());

                        Console.WriteLine($"Current Delivery Date: {reader["DeliveryDate"]}");
                        Console.Write("Enter new Delivery Date (YYYY-MM-DD): ");
                        string newDeliveryDate = Console.ReadLine();

                        reader.Close();

                        string updateQuery = $"UPDATE Product " +
                            $"SET Name = '{newName}', TypeId = {newTypeId}, ProviderId = {newProviderId}, Quantity = {newQuantity}, Cost = {newCost}, DeliveryDate = '{newDeliveryDate}'" +
                            $"WHERE Id = {productId}";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Product updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Product update failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No product found with the specified ID.");
                    }
                }
            }
        }

        static void UpdateProvider(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter the ID of the provider to update: ");
            int providerId = int.Parse(Console.ReadLine());
           
            string selectQuery = $"SELECT * FROM Provider WHERE Id = {providerId}";

            using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
            {

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Current Provider Name: {reader["Name"]}");
                        Console.Write("Enter new Provider Name: ");
                        string newName = Console.ReadLine();

                        reader.Close();

                        string updateQuery = $"UPDATE Provider SET Name = '{newName}' WHERE Id = {providerId}";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Provider updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Provider update failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No provider found with the specified ID.");
                    }
                }
            }
        }

        static void UpdateType(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("Enter the ID of the product type to update: ");
            int typeId = int.Parse(Console.ReadLine());

            string selectQuery = $"SELECT * FROM Type WHERE Id = {typeId}";

            using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
            {
               
                using (var reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Current Type Name: {reader["Name"]}");
                        Console.Write("Enter new Type Name: ");
                        string newName = Console.ReadLine();

                        reader.Close();

                        string updateQuery = $"UPDATE Type SET Name = '{newName}' WHERE Id = {typeId}";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Product type updated successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Product type update failed.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No product type found with the specified ID.");
                    }
                }
            }
        }


        static void DeleteProduct(SqlConnection connection)
        {
            Console.Clear();
            Console.Write("Enter the ID of the product to delete: ");
            int productId = int.Parse(Console.ReadLine());

            string query = $"DELETE FROM Product WHERE Id = {productId}";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product deleted successfully!");
                }
                else
                {
                    Console.WriteLine("No product found with the specified ID.");
                }
            }
        }

        static void DeleteProvider(SqlConnection connection)
        {
            Console.Clear();
            Console.Write("Enter the ID of the provider to delete: ");
            int providerId = int.Parse(Console.ReadLine());

            string query = $"DELETE FROM Provider WHERE Id = {providerId}";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Provider deleted successfully!");
                }
                else
                {
                    Console.WriteLine("No provider found with the specified ID.");
                }
            }
        }

        static void DeleteType(SqlConnection connection)
        {
            Console.Clear();
            Console.Write("Enter the ID of the product type to delete: ");
            int typeId = int.Parse(Console.ReadLine());

            string query = $"DELETE FROM Type WHERE Id = {typeId}";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product type deleted successfully!");
                }
                else
                {
                    Console.WriteLine("No product type found with the specified ID.");
                }
            }
        }

        static void ProviderWithMostProducts(SqlConnection connection)
        {
            string query = @"
            SELECT TOP 1
                PR.Id,
                PR.Name AS ProviderName,
                SUM(P.Quantity) AS TotalQuantity
            FROM Product AS P
            JOIN Provider AS PR ON P.ProviderId = PR.Id
            GROUP BY PR.Id, PR.Name
            ORDER BY TotalQuantity DESC";

            Show(connection, query, "ProviderName", "TotalQuantity");
        }

        static void ProviderWithLeastProducts(SqlConnection connection)
        {
            string query = @"
            SELECT TOP 1
                PR.Id,
                PR.Name AS ProviderName,
                SUM(P.Quantity) AS TotalQuantity
            FROM Product AS P
            JOIN Provider AS PR ON P.ProviderId = PR.Id
            GROUP BY PR.Id, PR.Name
            ORDER BY TotalQuantity ASC";

            Show(connection, query, "ProviderName", "TotalQuantity");
        }

        static void TypeWithMostQuantityOnHand(SqlConnection connection)
        {
            string query = @"
        SELECT TOP 1
            T.Id,
            T.Name AS TypeName,
            SUM(P.Quantity) AS TotalQuantity
        FROM Product AS P
        JOIN Type AS T ON P.TypeId = T.Id
        GROUP BY T.Id, T.Name
        ORDER BY TotalQuantity DESC";

            Show(connection, query, "TypeName", "TotalQuantity");
        }

        static void TypeWithLeastQuantityOnHand(SqlConnection connection)
        {
            string query = @"
            SELECT TOP 1
                T.Id,
                T.Name AS TypeName,
                SUM(P.Quantity) AS TotalQuantity
            FROM Product AS P
            JOIN Type AS T ON P.TypeId = T.Id
            GROUP BY T.Id, T.Name
            ORDER BY TotalQuantity ASC";

            Show(connection, query, "TypeName", "TotalQuantity");
        }

        static void ProductsWithDeliveryDateOlderThan(SqlConnection connection, int days)
        {
            string query = $@"
            SELECT P.Id,
                P.Name AS ProductName,
                T.Name AS TypeName,
                PR.Name AS ProviderName,
                P.Quantity,
                P.Cost,
                P.DeliveryDate
            FROM Product AS P
            JOIN Type AS T ON P.TypeId = T.Id
            JOIN Provider AS PR ON P.ProviderId = PR.Id
            WHERE DATEDIFF(DAY, P.DeliveryDate, GETDATE()) > {days}";

            Show(connection, query, "ProductName", "TypeName", "ProviderName", "Quantity", "Cost", "DeliveryDate");
        }
    }

}