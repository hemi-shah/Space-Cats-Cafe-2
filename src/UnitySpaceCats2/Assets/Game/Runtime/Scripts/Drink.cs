using System.Collections.Generic;
using System.Linq;

public class Drink : IDrink
{
    public Temperature Temp { get; set; }
    public CoffeeType Type { get; set; }
    public MilkType Milk { get; set; } = MilkType.None;
    public int IceLevel { get; set; } = 0;
    public Dictionary<string, int> SyrupCounts { get; private set; } = new(); // used to keep track of syrup counts
    public List<string> Toppings { get; private set; } = new();
    public int CurrentStationIndex { get; set; } = 0;
    public bool IsComplete { get; set; } = false;

    public void AddSyrup(string syrup)
    {
        if (SyrupCounts.ContainsKey(syrup))
            SyrupCounts[syrup]++;
        else
            SyrupCounts[syrup] = 1;

        UnityEngine.Debug.Log($"[Drink] Syrup counts: {string.Join(", ", SyrupCounts.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
    }

    public void AddMilk(MilkType milk) => Milk = milk;

    public void AddTopping(string topping)
    {
        if (!Toppings.Contains(topping))
            Toppings.Add(topping);
    }

    public string GetSpriteName()
    {
        if (Temp == Temperature.Hot)
        {
            return Type switch
            {
                CoffeeType.Black => "HotDrinkCoffeeSprite",
                CoffeeType.Milk => "HotDrinkMilkSprite",
                _ => "HotDrinkEmptySprite"
            };
        }
        else
        {
            string iceWord = IceLevel switch
            {
                0 => "Empty",
                1 => "OneIce",
                2 => "TwoIce",
                3 => "ThreeIce",
                4 => "FourIce",
                _ => "Empty"
            };

            string content = Type switch
            {
                CoffeeType.Black => "Coffee",
                CoffeeType.Milk => "Milk",
                _ => "Empty"
            };

            return $"IcedDrink{iceWord}{content}Sprite";
        }
    }
}