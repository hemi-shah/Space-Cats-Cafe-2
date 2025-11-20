using NUnit.Framework;
using Game399.Shared.Services;
using Game399.Shared.Enums;
using System.Collections.Generic;

[TestFixture]
public class DrinkTests
{
    [Test]
    public void IceLevel_StartsAtZero()
    {
        var drink = new MockDrink();
        Assert.AreEqual(0, drink.IceLevel);
    }

    [Test]
    public void AddIce_IncrementsIceLevel()
    {
        var drink = new MockDrink();
        drink.IceLevel++;
        drink.IceLevel++;
        Assert.AreEqual(2, drink.IceLevel);
    }

    [Test]
    public void AddSyrup_IncrementsCount()
    {
        var drink = new MockDrink();
        drink.AddSyrup("Chocolate");
        drink.AddSyrup("Chocolate");
        drink.AddSyrup("Caramel");

        Assert.AreEqual(2, drink.SyrupCounts.Count);
        Assert.AreEqual(2, drink.SyrupCounts["Chocolate"]);
        Assert.AreEqual(1, drink.SyrupCounts["Caramel"]);
    }

    [Test]
    public void AddMilk_SetsMilkType()
    {
        var drink = new MockDrink();
        drink.AddMilk(MilkType.Oat);
        Assert.AreEqual(MilkType.Oat, drink.Milk);
    }

    [Test]
    public void AddTopping_AddsUnique()
    {
        var drink = new MockDrink();
        drink.AddTopping("WhippedCream");
        drink.AddTopping("WhippedCream");
        drink.AddTopping("CaramelDrizzle");

        Assert.AreEqual(2, drink.Toppings.Count);
        CollectionAssert.Contains(drink.Toppings, "WhippedCream");
        CollectionAssert.Contains(drink.Toppings, "CaramelDrizzle");
    }

    // [Test]
    // public void GetSpriteName_CoversAllCases()
    // {
    //     var drink = new MockDrink();
    //
    //     // Hot Coffee
    //     drink.Temp = Temperature.Hot;
    //     drink.Coffee = CoffeeType.Black;
    //     Assert.AreEqual("HotDrinkCoffeeSprite", drink.GetSpriteName());
    //
    //     // Hot Milk
    //     drink.Coffee = CoffeeType.Milk;
    //     Assert.AreEqual("HotDrinkMilkSprite", drink.GetSpriteName());
    //
    //     // Iced Milk 3 ice
    //     drink.Temp = Temperature.Iced;
    //     drink.Coffee = CoffeeType.Milk;
    //     drink.IceLevel = 3;
    //     Assert.AreEqual("IcedDrinkThreeIceMilkSprite", drink.GetSpriteName());
    //
    //     // Iced Coffee no ice
    //     drink.Temp = Temperature.Iced;
    //     drink.Coffee = CoffeeType.Black;
    //     drink.IceLevel = 0;
    //     Assert.AreEqual("IcedDrinkEmptyCoffeeSprite", drink.GetSpriteName());
    //
    //     // Iced Milk ice > 4
    //     drink.Temp = Temperature.Iced;
    //     drink.Coffee = CoffeeType.Milk;
    //     drink.IceLevel = 10;
    //     Assert.AreEqual("IcedDrinkEmptyMilkSprite", drink.GetSpriteName());
    // }

    [Test]
    public void MultipleDrinks_HaveIndependentStates()
    {
        var drink1 = new MockDrink { Temp = Temperature.Hot, Coffee = CoffeeType.Black };
        var drink2 = new MockDrink { Temp = Temperature.Iced, Coffee = CoffeeType.Milk };

        drink1.AddMilk(MilkType.Oat);
        drink2.AddMilk(MilkType.Almond);

        Assert.AreEqual(MilkType.Oat, drink1.Milk);
        Assert.AreEqual(MilkType.Almond, drink2.Milk);

        drink1.AddSyrup("Caramel");
        drink2.AddSyrup("Chocolate");

        Assert.AreEqual(1, drink1.SyrupCounts.Count);
        Assert.AreEqual(1, drink2.SyrupCounts.Count);
    }
}
