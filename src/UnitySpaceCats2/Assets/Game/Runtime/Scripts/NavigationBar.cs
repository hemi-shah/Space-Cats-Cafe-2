using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Navigation bar controller - handles button clicks to navigate between screens
/// </summary>
public class NavigationBar : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button waitingScreenButton;
    [SerializeField] private Button hotOrColdDrinkButton;
    [SerializeField] private Button iceGameButton;
    [SerializeField] private Button syrupButton;
    [SerializeField] private Button espressoButton;
    [SerializeField] private Button milkButton;
    [SerializeField] private Button toppingsButton;
    [SerializeField] private Button reviewButton;

    [Header("Visual Feedback")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color activeColor = Color.yellow;
    [SerializeField] private Color disabledColor = Color.gray;

    void Start()
    {
        SetupButtonListeners();

        // Subscribe to state changes to update button visuals
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.CurrentState.ChangeEvent += OnStateChanged;
            OnStateChanged(GameStateManager.Instance.CurrentState.Value);
        }
    }

    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.CurrentState.ChangeEvent -= OnStateChanged;
        }
    }

    private void SetupButtonListeners()
    {
        if (waitingScreenButton != null)
            waitingScreenButton.onClick.AddListener(() => NavigateTo(GameStateType.WaitingforCustomers));
        if (hotOrColdDrinkButton != null)
            hotOrColdDrinkButton.onClick.AddListener(() => NavigateTo(GameStateType.ChoosingTemperature));
        if (iceGameButton != null)
            iceGameButton.onClick.AddListener(() => NavigateTo(GameStateType.PlayingIceGame));
        if (syrupButton != null)
            syrupButton.onClick.AddListener(() => NavigateTo(GameStateType.PumpingSyrup));
        if (espressoButton != null)
            espressoButton.onClick.AddListener(() => NavigateTo(GameStateType.PouringEspresso));
        if (milkButton != null)
            milkButton.onClick.AddListener(() => NavigateTo(GameStateType.ChoosingMilk));
        if (toppingsButton != null)
            toppingsButton.onClick.AddListener(() => NavigateTo(GameStateType.PlacingToppings));
        if (reviewButton != null)
            reviewButton.onClick.AddListener(() => NavigateTo(GameStateType.ServingDrinks));
    }

    private void NavigateTo(GameStateType targetState)
    {
        // Optional: Add validation here (e.g., can't go to review until drink is complete)
        if (CanNavigateTo(targetState))
        {
            GameStateManager.Instance.ChangeState(targetState);
        }
        else
        {
            Debug.LogWarning($"Cannot navigate to {targetState} at this time");
        }
    }

    /// <summary>
    /// Optional validation logic - prevent navigation to certain states
    /// </summary>
    private bool CanNavigateTo(GameStateType targetState)
    {
        // Example: Can't review until drink is made
        if (targetState == GameStateType.ServingDrinks)
        {
            // Check if drink is complete
            // return DrinkManager.Instance.IsDrinkComplete();
        }

        // Default: allow navigation
        return true;
    }

    /// <summary>
    /// Update button visuals based on current state
    /// </summary>
    private void OnStateChanged(GameStateType newState)
    {
        UpdateButtonVisual(waitingScreenButton, GameStateType.WaitingforCustomers, newState);
        UpdateButtonVisual(hotOrColdDrinkButton, GameStateType.ChoosingTemperature, newState);
        UpdateButtonVisual(iceGameButton, GameStateType.PlayingIceGame, newState);
        UpdateButtonVisual(syrupButton, GameStateType.PumpingSyrup, newState);
        UpdateButtonVisual(espressoButton, GameStateType.PouringEspresso, newState);
        UpdateButtonVisual(milkButton, GameStateType.ChoosingMilk, newState);
        UpdateButtonVisual(toppingsButton, GameStateType.PlacingToppings, newState);
        UpdateButtonVisual(reviewButton, GameStateType.ServingDrinks, newState);
    }

    private void UpdateButtonVisual(Button button, GameStateType buttonState, GameStateType currentState)
    {
        if (button == null) return;

        ColorBlock colors = button.colors;
        
        if (buttonState == currentState)
        {
            // Highlight active button
            colors.normalColor = activeColor;
            colors.selectedColor = activeColor;
        }
        else
        {
            // Normal state
            colors.normalColor = normalColor;
            colors.selectedColor = normalColor;
        }

        button.colors = colors;
    }

    /// <summary>
    /// Enable/disable specific buttons based on game progress
    /// </summary>
    public void SetButtonEnabled(GameStateType buttonState, bool enabled)
    {
        Button button = GetButtonForState(buttonState);
        if (button != null)
        {
            button.interactable = enabled;

            ColorBlock colors = button.colors;
            colors.disabledColor = disabledColor;
            button.colors = colors;
        }
    }

    private Button GetButtonForState(GameStateType state)
    {
        switch (state)
        {
            case GameStateType.WaitingforCustomers: return waitingScreenButton;
            case GameStateType.ChoosingTemperature: return hotOrColdDrinkButton;
            case GameStateType.PlayingIceGame: return iceGameButton;
            case GameStateType.PumpingSyrup: return syrupButton;
            case GameStateType.PouringEspresso: return espressoButton;
            case GameStateType.ChoosingMilk: return milkButton;
            case GameStateType.PlacingToppings: return toppingsButton;
            case GameStateType.ServingDrinks: return reviewButton;
            default: return null;
        }
    }
}