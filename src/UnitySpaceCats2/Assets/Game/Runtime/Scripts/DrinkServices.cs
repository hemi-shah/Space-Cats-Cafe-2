using System.Collections.Generic;
using Game.Runtime;
using UnityEngine;

public class DrinkServices
{
    private readonly List<IDrinkStation> stations = new();
    public IDrink CurrentDrink { get; private set; }
    public DrinkObjectUI CurrentDrinkUI { get; private set; }
    
    public GameObject CurrentDrinkObject { get; private set; }

    private IGameLogger logger;

    public DrinkServices(List<IDrinkStation> stationPipeline)
    {
        stations = stationPipeline;
        //logger = ServiceResolver.Resolve<IGameLogger>();
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

    public void RegisterCurrentDrinkUI(DrinkObjectUI drinkObjectUI)
    {
        CurrentDrinkUI = drinkObjectUI;
    }

    public void UpdateCurrentToppingOverlay()
    {
        CurrentDrinkUI?.UpdateToppingOverlay();
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

    public void ResetCurrentDrink()
    {
        // deactivate visual
        if (CurrentDrinkObject != null)
        {
            CurrentDrinkObject.SetActive(false);
            CurrentDrinkObject = null;
        }

        if (CurrentDrinkUI != null)
        {
            if (CurrentDrinkUI.gameObject != null)
            {
                CurrentDrinkUI.ResetUI();
            }
            CurrentDrinkUI = null;
        }

        //logger.Log("(DrinkServices) current drink reset!");
    }
    
}