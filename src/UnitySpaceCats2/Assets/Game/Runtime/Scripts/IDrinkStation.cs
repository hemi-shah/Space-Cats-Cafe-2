namespace Game.Runtime
{
    public interface IDrinkStation
    {
        string StationName { get; }
        void ProcessDrink(IDrink drink);
        bool IsDoneAtStation(IDrink drink);
    }
}