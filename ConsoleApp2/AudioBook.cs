using LibraryManagmentSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class AudioBook : AItems
    {
        public AudioBook(string title, string author)
        {
            Title = title;
            Author = author;
            ItemType = "AudioBook";
        }
    }
}


