using System.Collections.Generic;
using System.Linq;
using Game.Runtime;
using Game399.Shared.Enums;

public class Drink : IDrink
{
    public Temperature Temp { get; set; }
    public CoffeeType Coffee { get; set; }
    public MilkType Milk { get; set; } = MilkType.None;
    public int IceLevel { get; set; } = 0;
    public Dictionary<string, int> SyrupCounts { get; private set; } = new(); // used to keep track of syrup counts
    public List<string> Toppings { get; private set; } = new();
    public int CurrentStationIndex { get; set; } = 0;
    public bool IsComplete { get; set; } = false;

    private IGameLogger logger;
    private IGameLogger Logger => logger ??= ServiceResolver.Resolve<IGameLogger>();

    public void AddSyrup(string syrup)
    {
        if (SyrupCounts.ContainsKey(syrup))
            SyrupCounts[syrup]++;
        else
            SyrupCounts[syrup] = 1;

        //logger.Log($"[Drink] Syrup counts: {string.Join(", ", SyrupCounts.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
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
            return Coffee switch
            {
                CoffeeType.Black => "HotDrinkCoffeeSprite",
                CoffeeType.Milk => "HotDrinkMilkSprite",
                _ => "HotDrinkEmptySprite"
            };
        }
        else
        {
            string content = Coffee switch
            {
                CoffeeType.Black => "Coffee",
                CoffeeType.Milk => "Milk",
                _ => "Empty"
            };

            string spriteName = IceLevel switch
            {
                1 => $"IcedDrinkOneIce{content}Sprite",
                2 => $"IcedDrinkTwoIce{content}Sprite",
                3 => $"IcedDrinkThreeIce{content}Sprite",
                4 => $"IcedDrinkFourIce{content}Sprite",
                _ => $"IcedDrinkEmpty{content}Sprite" // <--- fixed here
            };

            return spriteName;
        }
    }
}