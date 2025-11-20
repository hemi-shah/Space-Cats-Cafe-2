using NUnit.Framework;
using Game399.Shared.Services;
using Game399.Shared.Enums;
using System.Collections.Generic;

[TestFixture]
public class DrinkUITests
{
    [Test]
    public void HotDrinkSpritePath_BlackCoffee_IsCorrect()
    {
        var drink = new MockDrink
        {
            Temp = Temperature.Hot,
            Coffee = CoffeeType.Black
        };

        string spriteName = drink.GetSpriteName();
        string path = $"Art/DrinkSprites/HotDrinkSprites/{spriteName}";

        Assert.AreEqual("Art/DrinkSprites/HotDrinkSprites/HotDrinkCoffeeSprite", path);
    }

    [Test]
    public void HotDrinkSpritePath_MilkCoffee_IsCorrect()
    {
        var drink = new MockDrink
        {
            Temp = Temperature.Hot,
            Coffee = CoffeeType.Milk
        };

        string spriteName = drink.GetSpriteName();
        string path = $"Art/DrinkSprites/HotDrinkSprites/{spriteName}";

        Assert.AreEqual("Art/DrinkSprites/HotDrinkSprites/HotDrinkMilkSprite", path);
    }

    //[Test]
    // public void IcedDrinkSpritePath_OneIce_Coffee()
    // {
    //     var drink = new MockDrink
    //     {
    //         Temp = Temperature.Iced,
    //         Coffee = CoffeeType.Black,
    //         IceLevel = 1
    //     };
    //
    //     string spriteName = drink.GetSpriteName();
    //     string path = $"Art/DrinkSprites/IcedDrinkSprites/{spriteName}";
    //
    //     Assert.AreEqual("Art/DrinkSprites/IcedDrinkSprites/IcedDrinkOneIceCoffeeSprite", path);
    // }

    [Test]
    public void IcedDrinkSpritePath_FourIce_Milk()
    {
        var drink = new MockDrink
        {
            Temp = Temperature.Iced,
            Coffee = CoffeeType.Milk,
            IceLevel = 4
        };

        string spriteName = drink.GetSpriteName();
        string path = $"Art/DrinkSprites/IcedDrinkSprites/{spriteName}";

        Assert.AreEqual("Art/DrinkSprites/IcedDrinkSprites/IcedDrinkFourIceMilkSprite", path);
    }

    [Test]
    public void IcedDrinkSpritePath_AboveFourIce_UsesEmptyIceSprite()
    {
        var drink = new MockDrink
        {
            Temp = Temperature.Iced,
            Coffee = CoffeeType.Milk,
            IceLevel = 10
        };

        string spriteName = drink.GetSpriteName();
        string path = $"Art/DrinkSprites/IcedDrinkSprites/{spriteName}";

        Assert.AreEqual("Art/DrinkSprites/IcedDrinkSprites/IcedDrinkEmptyMilkSprite", path);
    }

    //[Test]
    // public void IcedDrinkSpritePath_ZeroIce_UsesEmptySprite()
    // {
    //     var drink = new MockDrink
    //     {
    //         Temp = Temperature.Iced,
    //         Coffee = CoffeeType.Black,
    //         IceLevel = 0
    //     };
    //
    //     string spriteName = drink.GetSpriteName();
    //     string path = $"Art/DrinkSprites/IcedDrinkSprites/{spriteName}";
    //
    //     Assert.AreEqual("Art/DrinkSprites/IcedDrinkSprites/IcedDrinkEmptyCoffeeSprite", path);
    // }

    [Test]
    public void AllCoffeeTypes_HotAndIced_HaveCorrectSprites()
    {
        foreach (CoffeeType type in (CoffeeType[])System.Enum.GetValues(typeof(CoffeeType)))
        {
            var hotDrink = new MockDrink { Temp = Temperature.Hot, Coffee = type };
            var icedDrink = new MockDrink { Temp = Temperature.Iced, Coffee = type, IceLevel = 2 };

            string hotSprite = hotDrink.GetSpriteName();
            string icedSprite = icedDrink.GetSpriteName();

            Assert.IsNotNull(hotSprite);
            Assert.IsNotNull(icedSprite);
            Assert.IsNotEmpty(hotSprite);
            Assert.IsNotEmpty(icedSprite);
        }
    }

    [Test]
    public void IcedDrink_SpriteNameChangesWithIceLevel()
    {
        var drink = new MockDrink { Temp = Temperature.Iced, Coffee = CoffeeType.Milk };

        for (int ice = 0; ice <= 5; ice++)
        {
            drink.IceLevel = ice;
            string sprite = drink.GetSpriteName();

            if (ice == 0)
                Assert.AreEqual("IcedDrinkEmptyMilkSprite", sprite);
            else if (ice > 4)
                Assert.AreEqual("IcedDrinkEmptyMilkSprite", sprite);
            else
                Assert.IsTrue(sprite.Contains($"IcedDrink{NumberToWord(ice)}IceMilkSprite"));
        }
    }

    private string NumberToWord(int number) => number switch
    {
        1 => "One",
        2 => "Two",
        3 => "Three",
        4 => "Four",
        _ => ""
    };
}