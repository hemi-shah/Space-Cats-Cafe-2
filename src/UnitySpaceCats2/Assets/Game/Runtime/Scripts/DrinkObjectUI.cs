using UnityEngine;
using Game.Runtime;
using Game399.Shared.Enums;

public class DrinkObjectUI : MonoBehaviour
{
    public IDrink Drink { get; private set; }
    [SerializeField] private SpriteRenderer renderer;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    private void Start()
    {
        Drink = ServiceResolver.Resolve<DrinkServices>().CurrentDrink;
        UpdateDrinkSprite();
    }
    
    public void UpdateDrinkSprite()
    {
        string spriteName = Drink.GetSpriteName();
        string path = Drink.Temp == Temperature.Hot 
            ? $"Art/DrinkSprites/HotDrinkSprites/{spriteName}"
            : GetIcedDrinkPath(spriteName);
    
        Sprite sprite = Resources.Load<Sprite>(path);
        renderer.sprite = sprite;
    }

    private string GetIcedDrinkPath(string spriteName)
    {
        if (spriteName.Contains("Milk"))
            return $"Art/DrinkSprites/IcedDrinkSprites/Milk/{spriteName}";
        else if (spriteName.Contains("Coffee"))
            return $"Art/DrinkSprites/IcedDrinkSprites/Coffee/{spriteName}";
        else
            return $"Art/DrinkSprites/IcedDrinkSprites/Empty/{spriteName}";
    }
}