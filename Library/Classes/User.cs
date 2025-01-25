using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Classes
{
    internal class User
    {
        public string Login { get; set; }
        public string Parol { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public int BorrovedBookCount { get; set; }
        public List<string> BorrovedBook { get; set; } = new List<string>();
    }
}
