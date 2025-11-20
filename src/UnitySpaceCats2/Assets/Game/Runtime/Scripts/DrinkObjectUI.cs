using UnityEngine;
using Game.Runtime;
using Game399.Shared.Enums;
using TMPro;
using UnityEngine.UI;
using System;

public class DrinkObjectUI : MonoBehaviour
{
    public IDrink Drink { get; private set; }
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject drinkSprite;
    [SerializeField] private ToppingOverlay toppingOverlay;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    private void OnEnable()
    {
        if (drinkSprite != null)
            drinkSprite.SetActive(true);
        
        if  (toppingOverlay != null)
            toppingOverlay.gameObject.SetActive(true);
    }

    private void Start()
    {
        Drink = ServiceResolver.Resolve<DrinkServices>().CurrentDrink;

        ServiceResolver.Resolve<DrinkServices>().RegisterCurrentDrinkUI(this);
        
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

    public void UpdateToppingOverlay()
    {
        if (toppingOverlay != null)
            toppingOverlay.UpdateToppingOverlay(Drink);
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

    public void ResetUI()
    {
        try
        {
            if (drinkSprite != null)
                drinkSprite.SetActive(false);

            if (toppingOverlay != null && toppingOverlay.gameObject != null)
                toppingOverlay.gameObject.SetActive(false);

            if (gameObject != null)
                gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"ResetUI failed: {e.Message}");
        }
    }
    
}