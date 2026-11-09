using System;
using System.Collections.Generic;

interface IPayment
{
    void Pay(double amount);
}

class CardPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата карткою: {amount} грн");
    }
}

class CashPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата готівкою: {amount} грн");
    }
}

class Order
{
    public int Number { get; set; }
    public string Description { get; set; }
    public double Weight { get; set; }
    public double Distance { get; set; }

    public Order(int number, string description, double weight, double distance)
    {
        Number = number;
        Description = description;
        Weight = weight;
        Distance = distance;
    }
}

abstract class Transport
{
    public string Name { get; set; }
    public bool IsBusy { get; set; }

    public Transport(string name)
    {
        Name = name;
        IsBusy = false;
    }

    public abstract double CalculateCost(Order order);

    public virtual bool CanTake(Order order)
    {
        return true;
    }
}

class Bike : Transport
{
    public Bike() : base("Велосипед") { }

    public override double CalculateCost(Order order)
    {
        return 20 + 5 * order.Distance;
    }

    public override bool CanTake(Order order)
    {
        return order.Distance <= 10 && order.Weight <= 10;
    }
}

class Car : Transport
{
    public Car() : base("Автомобіль") { }

    public override double CalculateCost(Order order)
    {
        return 50 + 8 * order.Distance + order.Weight;
    }
}

class Truck : Transport
{
    public Truck() : base("Вантажівка") { }

    public override double CalculateCost(Order order)
    {
        return 200 + 15 * order.Distance + 0.5 * order.Weight;
    }
}

class DeliveryService
{
    private List<Transport> transports = new List<Transport>();
    private List<Order> activeOrders = new List<Order>();
    private List<Order> history = new List<Order>();

    private Dictionary<Order, Transport> assignedOrders =
        new Dictionary<Order, Transport>();

    public void AddTransport(Transport transport)
    {
        transports.Add(transport);
    }

    public Order CreateOrder(int number, string description, double weight, double distance)
    {
        return new Order(number, description, weight, distance);
    }

    public void ShowCosts(Order order)
    {
        Console.WriteLine($"\nВартість доставки замовлення №{order.Number}:");

        foreach (Transport transport in transports)
        {
            if (transport.CanTake(order))
            {
                Console.WriteLine(
                    $"{transport.Name}: {transport.CalculateCost(order)} грн");
            }
        }
    }

    public void AssignOrder(Order order, Transport transport)
    {
        if (activeOrders.Contains(order) || history.Contains(order))
        {
            Console.WriteLine("Замовлення вже призначене або доставлене!");
            return;
        }

        if (transport.IsBusy)
        {
            Console.WriteLine($"{transport.Name} вже зайнятий!");
            return;
        }

        if (!transport.CanTake(order))
        {
            Console.WriteLine($"{transport.Name} не може взяти це замовлення!");
            return;
        }

        transport.IsBusy = true;
        activeOrders.Add(order);
        assignedOrders.Add(order, transport);

        Console.WriteLine(
            $"Замовлення №{order.Number} призначено: {transport.Name}");
    }

    public void DeliverOrder(Order order, IPayment payment)
    {
        if (!assignedOrders.ContainsKey(order))
        {
            Console.WriteLine("Замовлення не призначене на транспорт!");
            return;
        }

        Transport transport = assignedOrders[order];

        double cost = transport.CalculateCost(order);

        payment.Pay(cost);

        activeOrders.Remove(order);
        assignedOrders.Remove(order);
        history.Add(order);

        transport.IsBusy = false;

        Console.WriteLine($"Замовлення №{order.Number} доставлено!");
        Console.WriteLine($"{transport.Name} тепер вільний.");
    }

    public void ShowActiveOrders()
    {
        Console.WriteLine("\n- Активні замовлення -");

        foreach (Order order in activeOrders)
        {
            Console.WriteLine(
                $"№{order.Number} | {order.Description} | " +
                $"{assignedOrders[order].Name}");
        }

        if (activeOrders.Count == 0)
            Console.WriteLine("Активних замовлень немає.");
    }

    public void ShowHistory()
    {
        Console.WriteLine("\n- Історія доставок -");

        foreach (Order order in history)
        {
            Console.WriteLine($"№{order.Number} | {order.Description}");
        }

        if (history.Count == 0)
            Console.WriteLine("Історія порожня.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        DeliveryService service = new DeliveryService();

        Bike bike = new Bike();
        Car car = new Car();
        Truck truck = new Truck();

        service.AddTransport(bike);
        service.AddTransport(car);
        service.AddTransport(truck);

        Order order1 = service.CreateOrder(1, "Посилка з документами", 2, 5);
        Order order2 = service.CreateOrder(2, "Побутова техніка", 15, 20);
        Order order3 = service.CreateOrder(3, "Великі меблі", 100, 30);
        Order order4 = service.CreateOrder(4, "Маленька посилка", 1, 15);

        service.ShowCosts(order1);
        service.ShowCosts(order2);
        service.ShowCosts(order3);

        Console.WriteLine("\n- Призначення замовлень -");

        service.AssignOrder(order1, bike);
        service.AssignOrder(order2, car);
        service.AssignOrder(order3, truck);

        service.AssignOrder(order4, bike);

        service.AssignOrder(order1, car);

        service.ShowActiveOrders();

        Console.WriteLine("\n- Доставка замовлень -");

        service.DeliverOrder(order1, new CardPayment());
        service.DeliverOrder(order2, new CashPayment());
        service.DeliverOrder(order3, new CardPayment());

        service.DeliverOrder(order1, new CashPayment());

        Console.WriteLine("\n- Повторне призначення -");
        service.AssignOrder(order4, bike);

        service.ShowActiveOrders();
        service.ShowHistory();

        Console.ReadKey();
    }
}
