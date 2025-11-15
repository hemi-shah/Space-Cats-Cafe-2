using System.Collections.Generic;
using Game.Runtime;
using UnityEngine;

public class DrinkServices
{
    private readonly List<IDrinkStation> stations = new();
    public IDrink CurrentDrink { get; private set; }
    
    public GameObject CurrentDrinkObject { get; private set; }

    public DrinkServices(List<IDrinkStation> stationPipeline)
    {
        stations = stationPipeline;
    }

    public IDrink CreateNewDrink()
    {
        CurrentDrink = new Drink();
        return CurrentDrink;
    }

    public void SetCurrentDrink(GameObject drinkObject)
    {
        CurrentDrinkObject = drinkObject;
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
}