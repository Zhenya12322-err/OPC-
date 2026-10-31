using System;
using System.Collections.Generic;

class Book
{
    public int Id { get; }
    public string Title { get; }
    public string Author { get; }

    public bool IsAvailable { get; private set; }

    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
        IsAvailable = true;
    }

    public void SetAvailability(bool available)
    {
        IsAvailable = available;
    }

    public override string ToString()
    {
        string status = IsAvailable ? "Доступна" : "В оренді";

        return $"ID: {Id}, Назва: {Title}, Автор: {Author}, Статус: {status}";
    }
}

class User
{
    public int Id { get; }
    public string Name { get; }

    private List<Book> rentedBooks = new List<Book>();

    public IReadOnlyList<Book> RentedBooks => rentedBooks.AsReadOnly();

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddBook(Book book)
    {
        rentedBooks.Add(book);
    }

    public bool HasBook(Book book)
    {
        return rentedBooks.Contains(book);
    }

    public void RemoveBook(Book book)
    {
        rentedBooks.Remove(book);
    }

    public override string ToString()
    {
        return $"ID: {Id}, Ім'я: {Name}";
    }
}

class Library
{
    private Dictionary<int, Book> books = new Dictionary<int, Book>();
    private Dictionary<int, User> users = new Dictionary<int, User>();

    public void AddBook(Book book)
    {
        if (books.ContainsKey(book.Id))
        {
            Console.WriteLine($"Помилка: книга з ID {book.Id} вже існує.");
            return;
        }

        books.Add(book.Id, book);

        Console.WriteLine($"Книгу \"{book.Title}\" додано до бібліотеки.");
    }

    public void AddUser(User user)
    {
        if (users.ContainsKey(user.Id))
        {
            Console.WriteLine($"Помилка: користувач з ID {user.Id} вже існує.");
            return;
        }

        users.Add(user.Id, user);

        Console.WriteLine($"Користувача {user.Name} додано до бібліотеки.");
    }

    public void RentBook(int userId, int bookId)
    {
        if (!users.TryGetValue(userId, out User user))
        {
            Console.WriteLine("Помилка: користувача не знайдено.");
            return;
        }

        if (!books.TryGetValue(bookId, out Book book))
        {
            Console.WriteLine("Помилка: книгу не знайдено.");
            return;
        }

        if (!book.IsAvailable)
        {
            Console.WriteLine($"Помилка: книга \"{book.Title}\" вже в оренді.");
            return;
        }

        book.SetAvailability(false);
        user.AddBook(book);

        Console.WriteLine(
            $"Користувач {user.Name} взяв книгу \"{book.Title}\" в оренду."
        );
    }

    public void ReturnBook(int userId, int bookId)
    {
        if (!users.TryGetValue(userId, out User user))
        {
            Console.WriteLine("Помилка: користувача не знайдено.");
            return;
        }

        if (!books.TryGetValue(bookId, out Book book))
        {
            Console.WriteLine("Помилка: книгу не знайдено.");
            return;
        }

        if (!user.HasBook(book))
        {
            Console.WriteLine(
                $"Помилка: користувач {user.Name} не орендував цю книгу."
            );
            return;
        }

        user.RemoveBook(book);
        book.SetAvailability(true);

        Console.WriteLine(
            $"Користувач {user.Name} повернув книгу \"{book.Title}\"."
        );
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("\n--- Усі книги бібліотеки ---");

        foreach (Book book in books.Values)
        {
            Console.WriteLine(book);
        }
    }

    public void ShowAllUsers()
    {
        Console.WriteLine("\n--- Усі користувачі ---");

        foreach (User user in users.Values)
        {
            Console.WriteLine(user);

            if (user.RentedBooks.Count == 0)
            {
                Console.WriteLine("  Користувач не орендував книг.");
            }
            else
            {
                Console.WriteLine("  Орендовані книги:");

                foreach (Book book in user.RentedBooks)
                {
                    Console.WriteLine($"  - {book.Title}");
                }
            }
        }
    }
    public void ShowAvailableBooks()
    {
        Console.WriteLine("\n--- Доступні книги ---");

        bool found = false;

        foreach (Book book in books.Values)
        {
            if (book.IsAvailable)
            {
                Console.WriteLine(book);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Немає доступних книг.");
        }
    }

    public void ShowRentedBooks()
    {
        Console.WriteLine("\n--- Книги в оренді ---");

        bool found = false;

        foreach (Book book in books.Values)
        {
            if (!book.IsAvailable)
            {
                Console.WriteLine(book);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Немає книг в оренді.");
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        Book book1 = new Book(1, "Кобзар", "Тарас Шевченко");
        Book book2 = new Book(2, "Захар Беркут", "Іван Франко");
        Book book3 = new Book(3, "Тигролови", "Іван Багряний");
        Book book4 = new Book(4, "Майстер і Маргарита", "Михайло Булгаков");

        User user1 = new User(1, "Євгеній");
        User user2 = new User(2, "Андрій");
        User user3 = new User(3, "Олександр");

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);

        library.AddUser(user1);
        library.AddUser(user2);
        library.AddUser(user3);

        library.ShowAvailableBooks();

        Console.WriteLine("\n=== Видача книги ===");
        library.RentBook(1, 1);

        Console.WriteLine("\n=== Повторна видача тієї самої книги ===");
        library.RentBook(2, 1);

        Console.WriteLine("\n=== Перегляд книг в оренді ===");
        library.ShowRentedBooks();

        Console.WriteLine("\n=== Повернення книги ===");
        library.ReturnBook(1, 1);

        Console.WriteLine("\n=== Повторна перевірка доступності ===");
        library.ShowAvailableBooks();

        Console.WriteLine("\n=== Перегляд усіх користувачів ===");
        library.ShowAllUsers();

        Console.WriteLine("\n=== Перегляд усіх книг ===");
        library.ShowAllBooks();
    }
}
