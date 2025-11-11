using System.Collections.Generic;
using Game.Runtime;

public class DrinkServices
{
    private readonly List<IDrinkStation> stations = new();
    public IDrink CurrentDrink { get; private set; }

    public DrinkServices(List<IDrinkStation> stationPipeline)
    {
        stations = stationPipeline;
    }

    public void CreateNewDrink()
    {
        CurrentDrink = new Drink();
    }

    public IDrinkStation GetCurrentStation()
    {
        if (CurrentDrink == null) return null;
        if (CurrentDrink.CurrentStationIndex >= stations.Count) return null;
        return stations[CurrentDrink.CurrentStationIndex];
    }

    public bool ProcessAtCurrentStation()
    {
        var station = GetCurrentStation();
        if (station == null) return false;

        station.ProcessDrink(CurrentDrink);
        return station.IsDoneAtStation(CurrentDrink);
    }

    public bool GoToNextStation()
    {
        var station = GetCurrentStation();
        if (station == null) return false;

        if (!station.IsDoneAtStation(CurrentDrink))
            return false;

        CurrentDrink.CurrentStationIndex++;
        return true;
    }

    public void CompleteDrink()
    {
        CurrentDrink.IsComplete = true;
    }

    // don't need to use in case someone else implements, but keeping it here for now
    public int ReviewDrink(IDrink order)
    {
        int score = 5;

        if (CurrentDrink.Temp != order.Temp) score--;
        if (CurrentDrink.Type != order.Type) score--;
        if (CurrentDrink.Milk != order.Milk) score--;
        if (CurrentDrink.IceLevel != order.IceLevel) score--;

        foreach (var topping in order.Toppings)
            if (!CurrentDrink.Toppings.Contains(topping)) score--;

        return score < 0 ? 0 : score;
    }
}