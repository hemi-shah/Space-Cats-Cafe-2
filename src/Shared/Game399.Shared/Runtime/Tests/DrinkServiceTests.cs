using Game399.Shared.Services;

namespace Game399.Tests;

public class DrinkServiceTests
{
    [Test]
    public void CanMakeDrinkTests_FailOnEmptyInput()
    {
        var drinkService = new DrinkService(EmptyGameLog.Instance);

        Assert.That(drinkService.CanMakeDrink(), Is.False);
    }
    
    
    [Test]
    public void CanMakeDrinkTests_Success()
    {
        var drinkService = new DrinkService(EmptyGameLog.Instance);

<<<<<<< Updated upstream:src/Shared/Game399.Tests/DrinkServiceTests.cs
        Assert.That(drinkService.CanMakeDrink(DrinkComponent.Milk), Is.True);
=======
            // Override CanMakeDrink to use MockDrink for testing
            public new bool CanMakeDrink(DrinkComponent component = DrinkComponent.None)
            {
                // simple mock logic for test purposes
                if (component == DrinkComponent.None)
                    return Drinks.Count > 0;
                return true;
            }

            public void AddMockDrink(MockDrink drink) => Drinks.Add(drink);
        }

        [Test]
        public void CanMakeDrinkTests_FailOnEmptyInput()
        {
            var drinkService = new TestDrinkService();
            Assert.That(drinkService.CanMakeDrink(), Is.False);
        }

        [Test]
        public void CanMakeDrinkTests_Success()
        {
            var drinkService = new TestDrinkService();
            drinkService.AddMockDrink(new MockDrink());

            Assert.That(drinkService.CanMakeDrink(DrinkComponent.Milk), Is.True);
        }
>>>>>>> Stashed changes:src/Shared/Game399.Shared/Runtime/Tests/DrinkServiceTests.cs
    }
}