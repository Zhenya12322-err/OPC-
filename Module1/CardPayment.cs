using System;

class CardPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата карткою: {amount} грн");
    }
}
