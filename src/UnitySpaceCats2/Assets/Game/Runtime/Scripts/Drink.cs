using System.Collections.Generic;

public class Drink : IDrink
{
    public Temperature Temp { get; set; }
    public CoffeeType Type { get; set; }
    public MilkType Milk { get; set; } = MilkType.None;
    public int IceLevel { get; set; } = 0;
    public List<string> Syrups { get; private set; } = new();
    public List<string> Toppings { get; private set; } = new();
    public int CurrentStationIndex { get; set; } = 0;
    public bool IsComplete { get; set; } = false;

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