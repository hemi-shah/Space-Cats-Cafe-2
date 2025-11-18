using Game.Runtime;
using UnityEngine;

public class IceStation : IDrinkStation
{
    public string StationName => "Ice Station";

    public bool CanUseStation(IDrink drink)
    {
        return drink.Temp == Temperature.Iced;
    }

    public void ProcessDrink(IDrink drink)
    {
        if (!CanUseStation(drink)) return;

        if (drink.IceLevel < 4)
            drink.IceLevel++;
    }

    public bool IsDoneAtStation(IDrink drink)
    {
        return drink.IceLevel > 0;
    }
}

