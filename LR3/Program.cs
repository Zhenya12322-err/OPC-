using System;

class Hunter
{
    private string name;
    private int health;
    private int gold;
    private int arrows;
    private int prey;

    public Hunter(string name, int health, int gold, int arrows, int prey)
    {
        this.name = name;
        this.health = health;
        this.gold = gold;
        this.arrows = arrows;
        this.prey = prey;
    }

    public void ShowInfo()
    {
        Console.WriteLine("\n--- Інформація про мисливця ---");
        Console.WriteLine($"Ім'я: {name}");
        Console.WriteLine($"Здоров'я: {health}");
        Console.WriteLine($"Золото: {gold}");
        Console.WriteLine($"Стріли: {arrows}");
        Console.WriteLine($"Здобич: {prey}");
    }

    public void Hunt()
    {
        if (health <= 0)
        {
            Console.WriteLine("Мисливець не може полювати без здоров'я!");
            return;
        }

        if (arrows <= 0)
        {
            Console.WriteLine("Немає стріл! Спочатку купіть стріли.");
            return;
        }

        Random random = new Random();

        arrows--;
        health -= 10;

        if (random.Next(0, 2) == 1)
        {
            prey++;
            gold += 20;
            Console.WriteLine("Полювання успішне! Ви отримали здобич і 20 золота.");
        }
        else
        {
            Console.WriteLine("Полювання невдале. Здобич не знайдено.");
        }

        if (health < 0)
            health = 0;

        Console.WriteLine($"Здоров'я: {health}, стріл залишилося: {arrows}");
    }

    public void BuyArrows()
    {
        const int price = 10;

        if (gold < price)
        {
            Console.WriteLine("Недостатньо золота для купівлі стріли!");
            return;
        }

        gold -= price;
        arrows++;

        Console.WriteLine("Ви купили одну стрілу за 10 золота.");
    }
}

class Program
{
    static void Main()
    {
        Hunter hunter = new Hunter("Лука", 100, 50, 5, 0);

        Console.WriteLine("=== ГРА «МИСЛИВЕЦЬ» ===");
        hunter.ShowInfo();

        while (true)
        {
            Console.WriteLine("\n--- Меню ---");
            Console.WriteLine("1. Полювати");
            Console.WriteLine("2. Купити стрілу");
            Console.WriteLine("3. Переглянути інформацію");
            Console.WriteLine("0. Вийти з гри");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    hunter.Hunt();
                    break;

                case "2":
                    hunter.BuyArrows();
                    break;

                case "3":
                    hunter.ShowInfo();
                    break;

                case "0":
                    Console.WriteLine("Гру завершено!");
                    return;

                default:
                    Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}