using LibraryManagmentSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class EBook : AItems
    {
        public EBook(string title, string author)
        {
            Title = title;
            Author = author;
            ItemType = "E-Book";
        }
    }
}
