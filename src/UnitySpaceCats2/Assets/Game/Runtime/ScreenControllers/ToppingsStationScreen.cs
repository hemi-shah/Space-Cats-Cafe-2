using UnityEngine;
using UnityEngine.UI;

public class ToppingsStationScreen : ScreenController
{
    [SerializeField] private Button whippedCreamButton;
    [SerializeField] private Button caramelDrizzleButton;
    [SerializeField] private Button cinnamonButton;
    [SerializeField] private Button continueButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PlacingToppings;
        
        if (whippedCreamButton != null)
        {
            whippedCreamButton.onClick.AddListener(() => OnToppingSelected("Whipped Cream"));
        }
        
        if (caramelDrizzleButton != null)
        {
            caramelDrizzleButton.onClick.AddListener(() => OnToppingSelected("Caramel Drizzle"));
        }
        
        if (cinnamonButton != null)
        {
            cinnamonButton.onClick.AddListener(() => OnToppingSelected("Cinnamon"));
        }
        
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinue);
            Debug.Log("ToppingsStation: Continue button ready");
        }
    }

    private void OnToppingSelected(string topping)
    {
        Debug.Log($"{topping} added");
    }

    private void OnContinue()
    {
        Debug.Log("Toppings complete!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PlacingToppings);
        GameStateManager.Instance.ChangeState(GameStateType.ServingDrinks);
    }
}