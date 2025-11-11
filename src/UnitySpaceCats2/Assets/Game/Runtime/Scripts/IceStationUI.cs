using UnityEngine;

public class IceStationUI : MonoBehaviour
{
    public SpriteRenderer drinkRenderer;
    private IceStation iceStation = new IceStation();
    public IDrink drink;

    public void OnIceButtonPressed()
    {
        if (!iceStation.CanUseStation(drink)) return;

        iceStation.ProcessDrink(drink);
        UpdateDrinkSprite();
    }

    private void UpdateDrinkSprite()
    {
        string spriteName = drink.GetSpriteName();
        Sprite sprite = Resources.Load<Sprite>("DrinkSprites/IcedDrinkSprites/Empty/" + spriteName);
        drinkRenderer.sprite = sprite;
    }
}

