class Bike : Transport
{
    public Bike() : base("Велосипед") { }

    public override double CalculateCost(Order order)
    {
        return 20 + 5 * order.Distance;
    }

    public override bool CanTake(Order order)
    {
        return order.Distance <= 10;
    }
}
