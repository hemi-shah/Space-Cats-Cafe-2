using Game.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Game399.Shared.Enums;

public class IceGameStationScreen : ScreenController
{
    [SerializeField] private Button completeButton;
    [SerializeField] private Text instructionText;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PlayingIceGame;
        
        if (completeButton != null)
        {
            completeButton.onClick.AddListener(OnIceGameComplete);
            logger.Log("IceGameStation: Complete button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();

        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CurrentDrink;

        if (drink == null)
        {
            logger.LogError("No current drink found!");
            return;
        }

        if (drink.Temp == Temperature.Hot)
        {
            logger.Log("Hot drink — skipping IceGameStation!");

            // Hide this screen immediately
            gameObject.SetActive(false);

            // Mark complete and go to next station
            NavigationBar.Instance?.MarkStationCompleted(GameStateType.PlayingIceGame);
            GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
            return;
        }

        if (instructionText != null)
        {
            instructionText.text = "Add ice to the cup!";
        }
    }

    private void OnIceGameComplete()
    {
        logger.Log("Ice game completed!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PlayingIceGame);
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
    }
}