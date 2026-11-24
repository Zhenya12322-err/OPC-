abstract class Transport
{
    public string Name;
    public bool IsBusy;

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
