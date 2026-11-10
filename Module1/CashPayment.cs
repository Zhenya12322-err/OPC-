using System;

class CashPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата готівкою: {amount} грн");
    }
}