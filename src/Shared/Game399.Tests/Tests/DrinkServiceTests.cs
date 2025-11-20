// using Game399.Shared.Services;
//
// namespace Game399.Tests;
//
// public class DrinkServiceTests
// {
//     private class TestDrinkService : DrinkService
//     {
//         private class TestDrinkService : DrinkService
//         {
//             public TestDrinkService() : base(EmptyGameLog.Instance) { }
//
//             public bool CanMakeDrink(DrinkComponent component = DrinkComponent.Milk)
//             {
//                 if (Drinks.Count == 0) return false;
//                 return true;
//             }
//
//             public void AddMockDrink(MockDrink drink) => Drinks.Add(drink);
//         }
//
//         public void AddMockDrink(MockDrink drink) => Drinks.Add(drink);
//     }
//
//     [Test]
//     public void CanMakeDrinkTests_FailOnEmptyInput()
//     {
//         var drinkService = new TestDrinkService();
//         Assert.That(drinkService.CanMakeDrink(), Is.False);
//     }
//     
//     [Test]
//     public void CanMakeDrinkTests_Success()
//     {
//         var drinkService = new TestDrinkService();
//         drinkService.AddMockDrink(new MockDrink());
//
//         Assert.That(drinkService.CanMakeDrink(DrinkComponent.Milk), Is.True);
//     }
// }