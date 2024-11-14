using LibraryManagmentSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp2
{
    public class Library
    {
        string connectionString = "Server=JELLO;Database=LibraryDB;Trusted_Connection=True;";

        public void AddItem(AItems item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO LibraryItems (Title, Author, ItemType, IsBorrowed, Borrower) VALUES (@Title, @Author, @ItemType, @IsBorrowed, @Borrower)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", item.Title);
                    command.Parameters.AddWithValue("@Author", item.Author);
                    command.Parameters.AddWithValue("@ItemType", item.ItemType);
                    command.Parameters.AddWithValue("@IsBorrowed", item.IsBorrowed);
                    command.Parameters.AddWithValue("@Borrower", (object)item.Borrower ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine($"{item.ItemType} added successfully.");
        }
        public void ListItems()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ItemID, Title, Author, ItemType, IsBorrowed, Borrower FROM LibraryItems";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nLibrary Items:");
                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        string author = reader["Author"].ToString();
                        string itemType = reader["ItemType"].ToString();
                        bool isBorrowed = (bool)reader["IsBorrowed"];
                        string borrower = reader["Borrower"]?.ToString();

                        Console.WriteLine($"{title} by {author} ({itemType}) - {(isBorrowed ? $"Borrowed by {borrower}" : "Available")}");
                    }
                }
            }
        }
        public void BorrowItem(string title, string borrower)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT ItemID, IsBorrowed FROM LibraryItems WHERE Title = @Title";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Title", title);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isBorrowed = (bool)reader["IsBorrowed"];
                            int itemId = (int)reader["ItemID"];

                            if (isBorrowed)
                            {
                                Console.WriteLine($"{title} is already borrowed.");
                                return;
                            }

                            reader.Close();

                            string updateQuery = "UPDATE LibraryItems SET IsBorrowed = 1, Borrower = @Borrower WHERE ItemID = @ItemID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Borrower", borrower);
                                updateCommand.Parameters.AddWithValue("@ItemID", itemId);
                                updateCommand.ExecuteNonQuery();
                                Console.WriteLine($"{title} has been borrowed by {borrower}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{title} not found.");
                        }
                    }
                }
            }
        }

        public void ReturnItem(string title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT ItemID, IsBorrowed FROM LibraryItems WHERE Title = @Title";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Title", title);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isBorrowed = (bool)reader["IsBorrowed"];
                            int itemId = (int)reader["ItemID"];

                            if (!isBorrowed)
                            {
                                Console.WriteLine($"{title} is not currently borrowed.");
                                return;
                            }

                            reader.Close();

                            string updateQuery = "UPDATE LibraryItems SET IsBorrowed = 0, Borrower = NULL WHERE ItemID = @ItemID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@ItemID", itemId);
                                updateCommand.ExecuteNonQuery();
                                Console.WriteLine($"{title} has been returned.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{title} not found.");
                        }
                    }
                }
            }
        }

        public void RemoveItem(string title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM LibraryItems WHERE Title = @Title";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine($"{title} has been removed from the library.");
                    else
                        Console.WriteLine($"{title} not found in the library.");
                }
            }
        }




    }

}