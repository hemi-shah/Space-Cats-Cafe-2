using UnityEngine;
using Game.Runtime;
using UnityEngine.UI;

public class ToppingOverlay : MonoBehaviour
{
    [Header("Topping Sprites")] 
    public Sprite whippedCreamSprite;
    public Sprite caramelDrizzleSprite;
    public Sprite chocolateDrizzleSprite;
    public Sprite bothDrizzleSprite;

    [Header("UI Image")] [SerializeField] 
    private Image toppingImage;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    public void UpdateToppingOverlay(IDrink drink)
    {

        bool hasWhip = drink.Toppings.Contains("WhippedCream");
        bool hasCaramel = drink.Toppings.Contains("CaramelDrizzle");
        bool hasChocolate = drink.Toppings.Contains("ChocolateDrizzle");

        // hide if no toppings
        if (!hasWhip && !hasCaramel && !hasChocolate)
        {
            //toppingImage.gameObject.SetActive(false);
            toppingImage.enabled = false;
            logger.Log("setting topping overlay inactive");
            return;
        }

        if ((hasCaramel || hasCaramel) && !hasWhip)
            return;

        // change sprite
        Sprite chosen = null;
        if (hasWhip & !hasChocolate && !hasCaramel)
            chosen = whippedCreamSprite;
        if (hasWhip && hasCaramel)
            chosen = caramelDrizzleSprite;
        if (hasWhip && hasChocolate)
            chosen = chocolateDrizzleSprite;
        if (hasWhip && hasCaramel && hasChocolate)
            chosen = bothDrizzleSprite;

        if (chosen != null)
        {
            toppingImage.sprite = chosen;
            //toppingImage.gameObject.SetActive(true);
            toppingImage.enabled = true;
        }
    }
}
