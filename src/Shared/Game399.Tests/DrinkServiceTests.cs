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

        Assert.That(drinkService.CanMakeDrink(DrinkComponent.Milk), Is.True);
    }
}