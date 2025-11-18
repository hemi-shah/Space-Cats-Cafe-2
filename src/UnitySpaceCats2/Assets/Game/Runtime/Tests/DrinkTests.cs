using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class DrinkTests
{
    [Test]
    public void IceLevel_StartsAtZero()
    {
        var drink = new Drink();
        Assert.AreEqual(0, drink.IceLevel);
    }

    [Test]
    public void AddIce_IncrementsIceLevel()
    {
        var drink = new Drink();

        drink.IceLevel++;
        drink.IceLevel++;

        Assert.AreEqual(2, drink.IceLevel);
    }

    [Test]
    public void AddSyrup_IncrementsCount()
    {
        var drink = new Drink();

        drink.AddSyrup("Chocolate");
        drink.AddSyrup("Chocolate"); // same syrup should increment count
        drink.AddSyrup("Caramel");

        Assert.AreEqual(2, drink.SyrupCounts.Count); // 2 types
        Assert.IsTrue(drink.SyrupCounts.ContainsKey("Chocolate"));
        Assert.IsTrue(drink.SyrupCounts.ContainsKey("Caramel"));
        Assert.AreEqual(2, drink.SyrupCounts["Chocolate"]); // 2 pumps of vanilla
        Assert.AreEqual(1, drink.SyrupCounts["Caramel"]);
    }

    [Test]
    public void AddMilk_StoresMilkType()
    {
        var drink = new Drink();

        drink.AddMilk(MilkType.Oat);

        Assert.AreEqual(MilkType.Oat, drink.Milk);
    }

    [Test]
    public void AddTopping_AddsUniqueToppings()
    {
        var drink = new Drink();

        drink.AddTopping("WhippedCream");
        drink.AddTopping("WhippedCream");
        drink.AddTopping("CaramelDrizzle");

        Assert.AreEqual(2, drink.Toppings.Count);
        Assert.Contains("WhippedCream", drink.Toppings);
        Assert.Contains("CaramelDrizzle", drink.Toppings);
    }

    [Test]
    public void GetSpriteName_HotBlackCoffee_ReturnsCorrectName()
    {
        var drink = new Drink
        {
            Temp = Temperature.Hot,
            Type = CoffeeType.Black
        };

        string sprite = drink.GetSpriteName();

        Assert.AreEqual("HotDrinkCoffeeSprite", sprite);
    }

    [Test]
    public void GetSpriteName_IcedCoffee_NoIce_ReturnsCorrectName()
    {
        var drink = new Drink
        {
            Temp = Temperature.Iced,
            Type = CoffeeType.Black,
            IceLevel = 0
        };

        string sprite = drink.GetSpriteName();

        Assert.AreEqual("IcedDrinkEmptyCoffeeSprite", sprite);
    }

    [Test]
    public void GetSpriteName_IcedMilk_FourIce_ReturnsCorrectName()
    {
        var drink = new Drink
        {
            Temp = Temperature.Iced,
            Type = CoffeeType.Milk,
            IceLevel = 4
        };

        string sprite = drink.GetSpriteName();

        Assert.AreEqual("IcedDrinkFourIceMilkSprite", sprite);
    }

    [Test]
    public void GetSpriteName_IceLevelAboveFour_UsesEmptyIceSprite()
    {
        var drink = new Drink
        {
            Temp = Temperature.Iced,
            Type = CoffeeType.Milk,
            IceLevel = 10
        };

        string sprite = drink.GetSpriteName();

        Assert.AreEqual("IcedDrinkEmptyMilkSprite", sprite);
    }
}
