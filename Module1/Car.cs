class Car : Transport
{
    public Car() : base("Автомобіль") { }

    public override double CalculateCost(Order order)
    {
        return 50 + 8 * order.Distance + order.Weight;
    }
}