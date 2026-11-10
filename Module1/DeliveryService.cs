using System;
using System.Collections.Generic;

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