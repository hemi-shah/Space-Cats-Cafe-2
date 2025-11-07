using NUnit.Framework;

[TestFixture]
public class OrderGeneratorTests
{
    [Test]
    public void Generate_HotDrink_HasNoIce()
    {
        var fake = new MockRandomProvider();
        
        fake.QueueValue(0.1f); // hot (under 0.5 hotChance)

        var generator = new OrderGenerator(fake) { hotChance = 0.5f };
        var order = generator.Generate();

        Assert.IsTrue(order.isHot);
        Assert.AreEqual(0, order.numberOfIceCubes);
    }

    [Test]
    public void Generate_IcedDrink_HasIceInRange()
    {
        var fake = new MockRandomProvider();

        fake.QueueValue(0.9f); // iced
        fake.QueueRange(3);    // ice count

        var generator = new OrderGenerator(fake) { hotChance = 0.5f };
        var order = generator.Generate();

        Assert.IsFalse(order.isHot);
        Assert.AreEqual(3, order.numberOfIceCubes);
    }

    [Test]
    public void Generate_SyrupRollsNone_WhenRandomFails()
    {
        var fake = new MockRandomProvider();

        fake.QueueValue(1f); // no syrup

        var gen = new OrderGenerator(fake) { syrupChance = 0.5f };
        var order = gen.Generate();

        Assert.AreEqual(SyrupType.None, order.syrup);
    }

    [Test]
    public void Generate_MilkRollsPicked_WhenRandomSucceeds()
    {
        var fake = new MockRandomProvider();

        fake.QueueValue(0.1f); // has milk
        fake.QueueRange((int)MilkType.Oat);

        var gen = new OrderGenerator(fake) { milkChance = 0.5f };
        var order = gen.Generate();

        Assert.AreEqual(MilkType.Oat, order.milk);
    }

    [Test]
    public void Generate_WhippedCreamCanHaveDrizzle()
    {
        var fake = new MockRandomProvider();

        fake.QueueValue(0.1f); // whipped true
        fake.QueueValue(0.2f); // choco drizzle true
        fake.QueueValue(0.2f); // caramel drizzle true

        var gen = new OrderGenerator(fake)
        {
            whippedCreamChance = 0.5f,
            drizzleChance = 0.5f
        };

        var order = gen.Generate();

        Assert.IsTrue(order.hasWhippedCream);
        Assert.IsTrue(order.hasChocolateSyrup);
        Assert.IsTrue(order.hasCaramelSyrup);
    }
}
