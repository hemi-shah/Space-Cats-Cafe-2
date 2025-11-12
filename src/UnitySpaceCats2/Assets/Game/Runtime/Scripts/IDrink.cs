using System.Collections.Generic;

public interface IDrink
{
    Temperature Temp { get; set; }
    CoffeeType Type { get; set; }
    MilkType Milk { get; set; }
    int IceLevel { get; set; }
    List<string> Syrups { get; }
    List<string> Toppings { get; }
    int CurrentStationIndex { get; set; }
    bool IsComplete { get; set; }

    string GetSpriteName();
    void AddSyrup(string syrup);
    void AddMilk(MilkType milk);
    void AddTopping(string topping);
}