using System;

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