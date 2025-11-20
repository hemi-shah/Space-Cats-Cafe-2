using Game.Runtime;
using Game399.Shared.Services;
using UnityEngine;
using UnityEngine.UI;

public class ToppingsStationScreen : ScreenController
{
    [SerializeField] private Button whippedCreamButton;
    [SerializeField] private Button caramelDrizzleButton;
    [SerializeField] private Button chocolateDrizzleButton;
    [SerializeField] private Button continueButton;

    private IDrink currentDrink;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PlacingToppings;
        
        var drinkService = ServiceResolver.Resolve<DrinkServices>();
        currentDrink = drinkService.CurrentDrink;
        
        if (whippedCreamButton != null)
        {
            whippedCreamButton.onClick.AddListener(() => OnToppingSelected("WhippedCream"));
        }
        
        if (caramelDrizzleButton != null)
        {
            caramelDrizzleButton.onClick.AddListener(() => OnToppingSelected("CaramelDrizzle"));
        }
        
        if (chocolateDrizzleButton != null)
        {
            chocolateDrizzleButton.onClick.AddListener(() => OnToppingSelected("ChocolateDrizzle"));
        }
        
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinue);
            logger.Log("ToppingsStation: Continue button ready");
        }
    }

    public void OnToppingSelected(string topping)
    {
        logger.Log($"{topping} added");
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        currentDrink = drinkServices.CurrentDrink;

        if (currentDrink == null)
            return;
        
        currentDrink.AddTopping(topping);

        drinkServices.UpdateCurrentToppingOverlay();
    }

    private void OnContinue()
    {
        logger.Log("Toppings complete!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PlacingToppings);
        GameStateManager.Instance.ChangeState(GameStateType.ServingDrinks);
    }
}