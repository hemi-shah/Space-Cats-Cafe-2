using Game399.Shared.Enums;

public class MockDrink
{
    public Temperature Temp { get; set; }
    public CoffeeType Coffee { get; set; }
    public MilkType Milk { get; set; }
    public int IceLevel { get; set; } = 0;
    public Dictionary<string,int> SyrupCounts { get; set; } = new();
    public HashSet<string> Toppings { get; set; } = new();

    public void AddMilk(MilkType milk) => Milk = milk;
    public void AddSyrup(string syrup)
    {
        if (!SyrupCounts.ContainsKey(syrup)) SyrupCounts[syrup] = 0;
        SyrupCounts[syrup]++;
    }
    public void AddTopping(string topping) => Toppings.Add(topping);

    public string GetSpriteName()
    {
        if (Temp == Temperature.Hot)
            return Coffee == CoffeeType.Black ? "HotDrinkCoffeeSprite" : "HotDrinkMilkSprite";
        if (IceLevel == 0 || IceLevel > 4)
            return $"IcedDrinkEmpty{Coffee}Sprite";
        return $"IcedDrink{NumberToWord(IceLevel)}Ice{Coffee}Sprite";
    }

    private string NumberToWord(int number) => number switch {1=>"One",2=>"Two",3=>"Three",4=>"Four", _=>""};
}