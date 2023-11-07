using System;
using System.Globalization;
using MySql.Data.MySqlClient;

public class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "Server=127.0.0.1;Port=3306;Database=inventorymanagmentdb;User=root;Password=33610389;";

        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open(); 

        Console.WriteLine("Welcome to the Inventory Management System!");
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. View Inventory");
            Console.WriteLine("5. Exit");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    
                    Console.Write("Enter product name: ");
                    string productName = Console.ReadLine();
                    Console.Write("Enter product category: ");
                    string productCategory = Console.ReadLine();
                    Console.Write("Enter product price: ");
                    decimal productPrice = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter quantity on hand: ");
                    int productQuantity = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter supplier: ");
                    string productSupplier = Console.ReadLine();

                    
                    string insertQuery = "INSERT INTO Products (Name, Category, Price, QuantityOnHand, Supplier) " +
                        "VALUES (@Name, @Category, @Price, @QuantityOnHand, @Supplier)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Name", productName);
                    insertCommand.Parameters.AddWithValue("@Category", productCategory);
                    insertCommand.Parameters.AddWithValue("@Price", productPrice);
                    insertCommand.Parameters.AddWithValue("@QuantityOnHand", productQuantity);
                    insertCommand.Parameters.AddWithValue("@Supplier", productSupplier);

                    if (insertCommand.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Product added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add the product.");
                    }
                    break;

                case "2":
                    
                    Console.Write("Enter product ID to update: ");
                    int productIdToUpdate = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter new product name: ");
                    string updatedName = Console.ReadLine();
                    Console.Write("Enter new product category: ");
                    string updatedCategory = Console.ReadLine();
                    Console.Write("Enter new product price: ");
                    decimal updatedPrice = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter new quantity on hand: ");
                    int updatedQuantity = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter new supplier: ");
                    string updatedSupplier = Console.ReadLine();

                    
                    string updateQuery = "UPDATE Products SET Name = @Name, Category = @Category, " +
                        "Price = @Price, QuantityOnHand = @QuantityOnHand, Supplier = @Supplier " +
                        "WHERE Id = @ProductId";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Name", updatedName);
                    updateCommand.Parameters.AddWithValue("@Category", updatedCategory);
                    updateCommand.Parameters.AddWithValue("@Price", updatedPrice);
                    updateCommand.Parameters.AddWithValue("@QuantityOnHand", updatedQuantity);
                    updateCommand.Parameters.AddWithValue("@Supplier", updatedSupplier);
                    updateCommand.Parameters.AddWithValue("@ProductId", productIdToUpdate);

                    if (updateCommand.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Product updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to update the product.");
                    }
                    break;

                case "3":
                   
                    Console.Write("Enter product ID to delete: ");
                    int productIdToDelete = Convert.ToInt32(Console.ReadLine());

                    
                    string deleteQuery = "DELETE FROM Products WHERE Id = @ProductId";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@ProductId", productIdToDelete);

                    if (deleteCommand.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Product deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete the product.");
                    }
                    break;

                case "4":
                   
                    string selectQuery = "SELECT * FROM Products";
                    MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                    MySqlDataReader reader = selectCommand.ExecuteReader();

                    Console.WriteLine("Inventory:");
                    while (reader.Read())
                    {
                        decimal price = (decimal)reader["Price"];
                        string formattedPrice = price.ToString("C", CultureInfo.CreateSpecificCulture("sv-SE"));
                        Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Category: {reader["Category"]}, " +
                            $"Price: {formattedPrice}, QuantityOnHand: {reader["QuantityOnHand"]}, Supplier: {reader["Supplier"]}");
                    }
                    reader.Close();
                    break;

                case "5":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        connection.Close(); 

        Console.WriteLine("Goodbye!");
    }
}




