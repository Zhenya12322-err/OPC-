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