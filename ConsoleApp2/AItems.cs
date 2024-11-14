using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSys
{
    public abstract class AItems : IBorrowable
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ItemType { get; set; } 
        public bool IsBorrowed { get; private set; }
        public string Borrower { get; private set; }

        public void Borrow(string borrower)
        {
            IsBorrowed = true;
            Borrower = borrower;
        }

        public void Return()
        {
            IsBorrowed = false;
            Borrower = null;
        }

        public string GetDetails() => $"{Title} by {Author} ({ItemType}) - {(IsBorrowed ? $"Borrowed by {Borrower}" : "Available")}";
    }

}

