using Game.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSelectionScreen : ScreenController
{
    [SerializeField] private Button hotButton;
    [SerializeField] private Button coldButton;
    [SerializeField] private CupAnimator cupAnimator;
    [SerializeField] private GameObject holdCupPrefab;
    [SerializeField] private GameObject coldCupPrefab;
    [SerializeField] private Transform hotDrinkSpawnPoint;
    [SerializeField] private Transform coldDrinkSpawnPoint;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingTemperature;
        
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
        Debug.Log("Hot drink selected!");

        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CreateNewDrink();
        drink.Temp = Temperature.Hot;

        cupAnimator.SelectHot(hotDrinkSpawnPoint);

        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
    }

    private void OnColdSelected()
    {
        Debug.Log("Iced drink selected!");

        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CreateNewDrink();
        drink.Temp = Temperature.Iced;

        cupAnimator.SelectCold(coldDrinkSpawnPoint);

        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.PlayingIceGame);
    }
}