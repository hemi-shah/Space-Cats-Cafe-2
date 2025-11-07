using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [Header("Random Order Weights")] 
    [Range(0f, 1f)] [SerializeField] private float hotChance = 0.5f;

    [Range(0f, 1f)] [SerializeField] private float syrupChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float milkChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float whippedCreamChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float drizzleChance = 0.35f;

    private CatDefinition selectedCat;
    private OrderTicketData currentOrder;
    private int nextOrderNumber = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedCat(CatDefinition cat)
    {
        selectedCat = cat;
    }

    public CatDefinition GetSelectedCat()
    {
        return selectedCat;
    }

    public OrderTicketData GenerateRandomOrderData()
    {

        // hot or iced
        bool randIsHot = Random.value < hotChance;

        // randomize ice
        int randIce;
        if (randIsHot)
            randIce = 0;
        else
            randIce = Random.Range(1, 5);
        
        // syrup
        SyrupType randSyrupType = SyrupType.None;
        
        bool randHasSyrup = Random.value < syrupChance;
        
        if (randHasSyrup)
            randSyrupType = PickRandomSyrup();
        
        // milk
        MilkType randMilkType = MilkType.None;
        
        bool randHasMilk = Random.value < milkChance;
        
        if (randHasMilk)
            randMilkType = PickRandomMilk();
        
        
        bool randHasWhippedCream = Random.value < whippedCreamChance;
        
        // if whipped cream, randomize drizzle
        bool randHasChocolateDrizzle = randHasWhippedCream && (Random.value < drizzleChance);
        bool randHasCaramelDrizzle = randHasWhippedCream && (Random.value < drizzleChance);
        
        // drink name
        string hotIced;
        if (randIsHot)
            hotIced = "Hot";
        else
            hotIced = "Iced";
        
        string flavor;
        switch (randSyrupType)
        {
            case (SyrupType.Caramel):
                flavor = "Caramel";
                break;
            case (SyrupType.Chocolate):
                flavor = "Chocolate";
                break;
            case (SyrupType.Mocha):
                flavor = "Mocha";
                break;
            default:
                flavor = "";
                break;
        }
        
        string baseName;
        if (randMilkType != MilkType.None)
            baseName = "Latte";
        else
            baseName = "Espresso";

        string drinkName = $"{hotIced} {flavor} {baseName}";

        // return OrderTicket Data
        return new OrderTicketData
        {
            isHot = randIsHot,
            numberOfIceCubes = randIce,
            
            syrup = randSyrupType,
            milk = randMilkType,
            
            hasWhippedCream = randHasWhippedCream,
            hasChocolateSyrup = randHasChocolateDrizzle,
            hasCaramelSyrup = randHasCaramelDrizzle,
            
            drinkName = drinkName,
        };

    }

    public OrderTicketData GetCurrentOrder()
    {
        return currentOrder;
    }

    public int GetAndIncrementNextOrderNumber()
    {
        int result = nextOrderNumber;
        nextOrderNumber++;
        return result;
    }

    public void ClearSelectedCat()
    {
        selectedCat = null;
    }

    private static SyrupType PickRandomSyrup()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0: return SyrupType.Caramel;
            case 1: return SyrupType.Chocolate;
            default: return SyrupType.Mocha;
        }
    }

    private static MilkType PickRandomMilk()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0: return MilkType.Almond;
            case 1: return MilkType.Oat;
            default: return MilkType.Dairy;
        }
    }
}
