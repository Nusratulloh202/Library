using Library.Service;

var libraryService = new LebraryService();

while (true)
{
    Console.WriteLine("\nKutubxona tizimi:");
    Console.WriteLine("1. Kitoblar ro'yxatini ko'rish");
    Console.WriteLine("2. Kitob haqida ma'lumot olish");
    Console.WriteLine("3. Kitob olish");
    Console.WriteLine("4. Kitob qaytarish");
    Console.WriteLine("5. Chiqish");
    Console.Write("Tanlovingizni kiriting: ");

    try
    {
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                libraryService.ViewBooks();
                break;
            case 2:
                libraryService.ViewBookDetails();
                break;
            case 3:
                libraryService.BorrowBook();
                break;
            case 4:
                libraryService.ReturnBook();
                break;
            case 5:
                Console.WriteLine("Chiqish...");
                return;
            default:
                Console.WriteLine("Noto'g'ri tanlov. Qaytadan kiriting.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[XATO]: {ex.Message}");
    }
}
       
