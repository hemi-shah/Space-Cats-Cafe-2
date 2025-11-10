using System.Collections.Generic;

public class Drink : IDrink
{
    public Temperature Temp { get; set; }
    public CoffeeType Type { get; set; }
    public MilkType Milk { get; set; } = MilkType.None;
    public int IceLevel { get; set; } = 0; // default no ice
    public List<string> Syrups { get; private set; } = new List<string>();
    public List<string> Toppings { get; private set; } = new List<string>();
    public int CurrentStationIndex { get; set; } = 0;
    public bool IsComplete { get; set; } = false;

    public string GetSpriteName()
    {
        // need to implement based on current naming convention of sprites
        return "";
    }

    public void AddSyrup(string syrup)
    {
        if (!Syrups.Contains(syrup))
            Syrups.Add(syrup);
    }

    public void AddMilk(MilkType milk)
    {
        Milk = milk;
    }

    public void AddTopping(string topping)
    {
        if (!Toppings.Contains(topping))
            Toppings.Add(topping);
    }
}