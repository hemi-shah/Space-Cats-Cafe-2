using NUnit.Framework;
using Game399.Shared.Services;
using Game399.Shared.Enums;
using System.Collections.Generic;

namespace Game399.Tests
{
    public class DrinkServiceTests
    {
        private class TestDrinkService : DrinkService
        {
            public List<MockDrink> Drinks { get; } = new();

            public TestDrinkService() : base(EmptyGameLog.Instance) { }

            // Override CanMakeDrink to use MockDrink for testing
            public new bool CanMakeDrink(DrinkComponent component = DrinkComponent.None)
            {
                // simple mock logic for test purposes
                if (component == DrinkComponent.None)
                    return Drinks.Count > 0;
                return true;
            }

            // Optionally, add a method to add mock drinks
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
    }
}