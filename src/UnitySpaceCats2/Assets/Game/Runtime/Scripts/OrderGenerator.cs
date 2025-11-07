public class OrderGenerator
{
    private readonly IRandomProvider _random;

    public float hotChance = 0.5f;
    public float syrupChance = 0.75f;
    public float milkChance = 0.75f;
    public float whippedCreamChance = 0.75f;
    public float drizzleChance = 0.35f;

    public OrderGenerator(IRandomProvider random)
    {
        _random = random;
    }

    public OrderTicketData Generate()
    {
        bool isHot = _random.Value() < hotChance;
        int ice = isHot ? 0 : _random.Range(1, 5);

        bool hasSyrup = _random.Value() < syrupChance;
        SyrupType syrup = hasSyrup ? PickRandomSyrup() : SyrupType.None;

        bool hasMilk = _random.Value() < milkChance;
        MilkType milk = hasMilk ? PickRandomMilk() : MilkType.None;

        bool hasWhipped = _random.Value() < whippedCreamChance;
        bool chocoDrizzle = hasWhipped && _random.Value() < drizzleChance;
        bool caramelDrizzle = hasWhipped && _random.Value() < drizzleChance;

        string hotIced = isHot ? "Hot" : "Iced";
        string flavor = syrup.ToString() == "None" ? "" : syrup.ToString();
        string baseName = milk == MilkType.None ? "Espresso" : "Latte";

        return new OrderTicketData
        {
            isHot = isHot,
            numberOfIceCubes = ice,
            syrup = syrup,
            milk = milk,
            hasWhippedCream = hasWhipped,
            hasChocolateSyrup = chocoDrizzle,
            hasCaramelSyrup = caramelDrizzle,
            drinkName = $"{hotIced} {flavor} {baseName}".Trim()
        };
    }

    private SyrupType PickRandomSyrup() =>
        (SyrupType)_random.Range(0, 3);

    private MilkType PickRandomMilk() =>
        (MilkType)_random.Range(0, 3);
}