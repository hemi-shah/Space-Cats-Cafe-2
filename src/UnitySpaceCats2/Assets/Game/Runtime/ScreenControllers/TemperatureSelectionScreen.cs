using Game.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSelectionScreen : ScreenController
{
    [SerializeField] private Button hotButton;
    [SerializeField] private Button coldButton;
    [SerializeField] private CupAnimator cupAnimator;
    [SerializeField] private GameObject hotDrinkObject; // Changed from Prefab - assign the drink in scene
    [SerializeField] private GameObject coldDrinkObject; // Changed from Prefab - assign the drink in scene
    [SerializeField] private Transform hotDrinkSpawnPoint;
    [SerializeField] private Transform coldDrinkSpawnPoint;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingTemperature;
        
        // Make sure drink objects start disabled
        if (hotDrinkObject != null) hotDrinkObject.SetActive(false);
        if (coldDrinkObject != null) coldDrinkObject.SetActive(false);
        
        if (hotButton != null)
        {
            hotButton.onClick.AddListener(OnHotSelected);
            Debug.Log("HotOrIcedStation: Hot button ready");
        }
        
        if (coldButton != null)
        {
            coldButton.onClick.AddListener(OnColdSelected);
            Debug.Log("HotOrIcedStation: Cold button ready");
        }
    }

    private void OnHotSelected()
    {
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CreateNewDrink();
        drink.Temp = Temperature.Hot;

        //cupAnimator.SelectHot(hotDrinkSpawnPoint, hotDrinkPrefab);
        
        GameObject drinkVisual = null;
        if (cupAnimator != null)
            drinkVisual = cupAnimator.SelectHot(hotDrinkSpawnPoint, hotDrinkPrefab);
        else
            drinkVisual = Instantiate(hotDrinkPrefab, hotDrinkSpawnPoint.position, hotDrinkSpawnPoint.rotation, hotDrinkSpawnPoint);

        if (drinkServices != null)
            drinkServices.SetCurrentDrink(drinkVisual);
        
        OrderManager.Instance.SetCurrentDrinkObject(drinkVisual);

        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
    }

    private void OnColdSelected()
    {
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CreateNewDrink();
        drink.Temp = Temperature.Iced;

       // cupAnimator.SelectCold(coldDrinkSpawnPoint, coldDrinkPrefab);
        
        GameObject drinkVisual = null;
        if (cupAnimator != null)
            drinkVisual = cupAnimator.SelectCold(coldDrinkSpawnPoint, coldDrinkPrefab);
        else
            drinkVisual = Instantiate(coldDrinkPrefab, coldDrinkSpawnPoint.position, coldDrinkSpawnPoint.rotation, coldDrinkSpawnPoint);

        if (drinkServices != null)
            drinkServices.SetCurrentDrink(drinkVisual);
        
        OrderManager.Instance.SetCurrentDrinkObject(drinkVisual);

        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.PlayingIceGame);
    }
}