using LibraryManagmentSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public interface IBorrowable
    {
        void Borrow(string borrower);
        void Return();
        bool IsBorrowed { get; }
        string GetDetails();
    }
    public class Book : AItems
    {
        public Book(string title,string author)
        {
            Title = title;
            Author = author;
            ItemType = "Book";
        }
    }

}
