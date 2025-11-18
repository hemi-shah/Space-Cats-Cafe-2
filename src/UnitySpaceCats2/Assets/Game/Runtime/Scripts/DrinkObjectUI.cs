using UnityEngine;
using Game.Runtime;

public class DrinkObjectUI : MonoBehaviour
{
    public IDrink Drink { get; private set; }
    [SerializeField] private SpriteRenderer renderer;

    private void Start()
    {
        Drink = ServiceResolver.Resolve<DrinkServices>().CurrentDrink;
        UpdateDrinkSprite();
    }

    public void UpdateDrinkSprite()
    {
        string spriteName = Drink.GetSpriteName();
        Sprite sprite = Resources.Load<Sprite>("DrinkSprites/IcedDrinkSprites/Empty/" + spriteName);
        renderer.sprite = sprite;
    }
}