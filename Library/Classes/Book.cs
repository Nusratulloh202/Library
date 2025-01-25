using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Classes
{
    internal class Book
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public List<string> CurrentReaders { get; set; } = new List<string>();
    }
}
