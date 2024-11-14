using ConsoleApp2;
using Microsoft.Data.SqlClient;
using static ConsoleApp2.Library;

class Program
{
    private static string connectionString;

    static void Main(string[] args)
    {
        Library library = new Library();
        bool continueProgram = true;

        while (continueProgram)
        {
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add E-Book");
            Console.WriteLine("3. Add Audio Book");
            Console.WriteLine("4. List All Items");
            Console.WriteLine("5. Borrow Item");
            Console.WriteLine("6. Return Item");
            Console.WriteLine("7. Remove Item");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Book Title: ");
                    string bookTitle = Console.ReadLine();
                    Console.Write("Enter Book Author: ");
                    string bookAuthor = Console.ReadLine();
                    library.AddItem(new Book(bookTitle, bookAuthor));
                    break;

                case "2":
                    Console.Write("Enter E-Book Title: ");
                    string eBookTitle = Console.ReadLine();
                    Console.Write("Enter E-Book Author: ");
                    string eBookAuthor = Console.ReadLine();
                    library.AddItem(new EBook(eBookTitle, eBookAuthor));
                    break;

                case "3":
                    Console.Write("Enter Audio Book Title: ");
                    string audioBookTitle = Console.ReadLine();
                    Console.Write("Enter Audio Book Author: ");
                    string audioBookAuthor = Console.ReadLine();
                    library.AddItem(new AudioBook(audioBookTitle, audioBookAuthor));
                    break;

                case "4":
                    library.ListItems();
                    break;

                case "5":
                    Console.Write("Enter Title of Item to Borrow: ");
                    string borrowTitle = Console.ReadLine();
                    Console.Write("Enter Your Name: ");
                    string borrower = Console.ReadLine();
                    library.BorrowItem(borrowTitle, borrower);
                    break;

                case "6":
                    Console.Write("Enter Title of Item to Return: ");
                    string returnTitle = Console.ReadLine();
                    library.ReturnItem(returnTitle);
                    break;

                case "7":
                    Console.Write("Enter Title of Item to Remove: ");
                    string removeTitle = Console.ReadLine();
                    library.RemoveItem(removeTitle);
                    break;

                case "8":
                    continueProgram = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }

            if (continueProgram)
            {
                Console.Write("\nWould you like to choose another option? (y/n): ");
                string response = Console.ReadLine();
                if (response.ToLower() != "y")
                {
                    continueProgram = false;
                }
            }
        }


    }
}