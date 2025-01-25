using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Brokers;
using Library.Classes;

namespace Library.Service
{
    internal class LebraryService
    {
        private readonly FileBroker _fileBroker;
        private readonly LoggingBroker _loggingBroker;


        //Lists
        private List<Book> _books;
        private List<User> _users;


        public LebraryService()
        {
            _fileBroker = new FileBroker();
            _loggingBroker = new LoggingBroker();
            _books = _fileBroker.LoadData<Book>("books.json");
            _users = _fileBroker.LoadData<User>("users.json");
        }

        //Kitoblarni ko'rish.
        public void ViewBooks()
        {
            Console.WriteLine("\nKitoblar ro'yxati:");
            for (int i = 0; i < _books.Count; i++)
            {
                Console.WriteLine($"{i+1}.{_books[i].Title} ({_books[i].AvailableCopies}/{_books[i].TotalCopies} mavjud)");
            }
        }
        public void ViewBookDetails()
        {
            ViewBooks();
            Console.Write("Ma'lumot olish uchun kitob raqamini kiriting:");
            int choise = int.Parse(Console.ReadLine()) - 1;

            if(choise>=0 && choise < _books.Count)
            {
                var book = _books[choise];
                Console.WriteLine($"\nKitob haqida ma'lumot:");
                Console.WriteLine($"Nomi: {book.Title}");
                Console.WriteLine($"Janri: {book.Genre}");
                Console.WriteLine($"Muallifi: {book.Author}");
                Console.WriteLine($"Jami nusxalar: {book.TotalCopies}");
                Console.WriteLine($"Mavjud nusxalar: {book.AvailableCopies}");
                Console.WriteLine("O'qiyotganlar: " + string.Join(", ", book.CurrentReaders));
            } 
            else
                Console.WriteLine("Noto'g'ri tanlov.");
        }

        //Kitobni ijaraga olish
        public void BorrowBook()
        {
            ViewBooks();
            Console.Write("Olish uchun kitob raqamini kiriting:");
            int choice = int.Parse(Console.ReadLine())-1;
            if(choice>=0 && choice < _books.Count)
            {
                var book = _books[choice];
                if (book.AvailableCopies>0)
                {
                    Console.Write("Foydalanuvchi ismini kiriting:");
                    string userName = Console.ReadLine();
                    var user = _users.Find(u => u.Name == userName);
                    if (user == null)
                    {
                        Console.WriteLine("Foydalanuvchi topilmadi. Ro'yxatdan o'tishingiz kerak.");
                        return;
                    }
                    book.AvailableCopies--;
                    book.CurrentReaders.Add(userName);
                    user.BorrovedBook.Add(book.Title);
                    user.BorrovedBookCount++;

                    _fileBroker.SaveData("books.json", _books);
                    _fileBroker.SaveData("users.json", _users);
                    Console.WriteLine($"{book.Title} kitob muaffaqiyatli olindi.");
                }
                else
                {
                    Console.WriteLine("Bu kitob hozirda mavjud emas.");
                }
            }
            else
            {
                Console.WriteLine("Noto'g'ri tanlov.");
            }
        }

        //Kitobni qaytarish
        public void ReturnBook()
        {
            Console.Write("Foydalanuvchi ismini kiriting:");
            string userName = Console.ReadLine();
            var user = _users.Find(u => u.Name == userName);
            if (user==null || user.BorrovedBookCount==0)
            {
                Console.WriteLine("Foydalanuvchi topilmadi yoki kitob qaytarilishi kerak emas.");
                return;
            }
            Console.WriteLine("Olingan kitoblar ro'yxati:");
            for (int i = 0; i < user.BorrovedBookCount; i++)
            {
                Console.WriteLine($"{i + 1}.{user.BorrovedBook[i]}");
            }
            Console.Write("Qaytarish uchun kitob raqamini kiriting:");
            int choice = int.Parse(Console.ReadLine())-1;
            if (choice>=0 && choice<user.BorrovedBookCount)
            {
                var bookTitle = user.BorrovedBook[choice];
                var book = _books.Find(e => e.Title == bookTitle);
                
                if (book != null)
                {
                    book.AvailableCopies++;
                    book.CurrentReaders.Remove(user.Name);
                    user.BorrovedBook.Remove(bookTitle);
                    user.BorrovedBookCount--;

                    _fileBroker.SaveData("books.json", _books);
                    _fileBroker.SaveData("users.json", _users);
                    Console.WriteLine($"{bookTitle} kitob muaffaqiyatli qaytarildi.");
                }
            }
            else
            {
                Console.WriteLine("Noto'g'ri tanlov.");
            }

        }
    }
}
