using Game.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Game399.Shared.Enums;

public class TemperatureSelectionScreen : ScreenController
{
    [SerializeField] private Button hotButton;
    [SerializeField] private Button coldButton;
    [SerializeField] private CupAnimator cupAnimator;
    [SerializeField] private GameObject hotDrinkObject; 
    [SerializeField] private GameObject coldDrinkObject; 
    [SerializeField] private Transform hotDrinkSpawnPoint;
    [SerializeField] private Transform coldDrinkSpawnPoint;

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        
        
        // deactivate drink objects initially
        if (hotDrinkObject != null)
            hotDrinkObject.SetActive(false);
        if (coldDrinkObject != null)
            coldDrinkObject.SetActive(false);
        
        hotButton.gameObject.SetActive(true);
        hotButton.interactable = true;
        
        coldButton.gameObject.SetActive(true);
        coldButton.interactable = true;

        if (cupAnimator != null)
        {
            cupAnimator.ResetCups();
        }
        
        //SetupButtons();
    }

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingTemperature;
        
        if (hotDrinkObject != null) hotDrinkObject.SetActive(false);
        if (coldDrinkObject != null) coldDrinkObject.SetActive(false);
        
        if (hotButton != null)
        {
            hotButton.onClick.RemoveAllListeners();
            hotButton.onClick.AddListener(OnHotSelected);
        }
        
        if (coldButton != null)
        {
            hotButton.onClick.RemoveAllListeners();
            coldButton.onClick.AddListener(OnColdSelected);
        }
    }

    private void OnHotSelected()
    {
        logger.Log("TempScreen: HOT drink selected");

        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CreateNewDrink();
        drink.Temp = Temperature.Hot;

        if (hotDrinkObject != null)
        {
            hotDrinkObject.SetActive(true);
            hotDrinkObject.transform.position = hotDrinkSpawnPoint.position;
            logger.Log($"TempScreen: Hot drink activated at {hotDrinkSpawnPoint.position}");
        }
        
        cupAnimator.SelectHot(hotDrinkSpawnPoint, hotDrinkObject);
        
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }

    private void OnColdSelected()
    {
        logger.Log("TempScreen: COLD drink selected");
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CreateNewDrink();
        drink.Temp = Temperature.Iced;

        if (coldDrinkObject != null)
        {
            coldDrinkObject.SetActive(true);
            coldDrinkObject.transform.position = coldDrinkSpawnPoint.position;
            logger.Log($"TempScreen: Cold drink activated at {coldDrinkSpawnPoint.position}");
        }
        
        logger.Log("TempScreen: Calling cupAnimator.SelectCold()");
        cupAnimator.SelectCold(coldDrinkSpawnPoint, coldDrinkObject);
        
        logger.Log("TempScreen: Marking station completed & switching to PlayingIceGame");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.PlayingIceGame);
    }
}
