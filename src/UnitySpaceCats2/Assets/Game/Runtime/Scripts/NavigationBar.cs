using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Navigation bar controller - handles button clicks to navigate between screens
/// </summary>
public class NavigationBar : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button tempButton;
    [SerializeField] private Button milkButton;
    [SerializeField] private Button syrupButton;
    [SerializeField] private Button espressoButton;
    [SerializeField] private Button iceButton;
    [SerializeField] private Button reviewButton;
    [SerializeField] private Button ticketsButton;
    [SerializeField] private Button pauseButton;

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
        if (tempButton != null)
            tempButton.onClick.AddListener(() => NavigateTo(GameStateType.ChoosingTemperature));
        if (milkButton != null)
            milkButton.onClick.AddListener(() => NavigateTo(GameStateType.ChoosingMilk));
        if (syrupButton != null)
            syrupButton.onClick.AddListener(() => NavigateTo(GameStateType.PumpingSyrup));
        if (espressoButton != null)
            espressoButton.onClick.AddListener(() => NavigateTo(GameStateType.PouringEspresso));
        if (iceButton != null)
            iceButton.onClick.AddListener(() => NavigateTo(GameStateType.PlayingIceGame));
        if (reviewButton != null)
            reviewButton.onClick.AddListener(() => NavigateTo(GameStateType.ServingDrinks));
        if (ticketsButton != null)
            ticketsButton.onClick.AddListener(() => NavigateTo(GameStateType.TakingOrder));
        if (pauseButton != null)
            pauseButton.onClick.AddListener(() => NavigateTo(GameStateType.Pause));
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
        UpdateButtonVisual(tempButton, GameStateType.ChoosingTemperature, newState);
        UpdateButtonVisual(milkButton, GameStateType.ChoosingMilk, newState);
        UpdateButtonVisual(syrupButton, GameStateType.PumpingSyrup, newState);
        UpdateButtonVisual(espressoButton, GameStateType.PouringEspresso, newState);
        UpdateButtonVisual(iceButton, GameStateType.PlayingIceGame, newState);
        UpdateButtonVisual(reviewButton, GameStateType.ServingDrinks, newState);
        UpdateButtonVisual(ticketsButton, GameStateType.TakingOrder, newState);
        UpdateButtonVisual(pauseButton, GameStateType.Pause, newState);
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
            case GameStateType.ChoosingTemperature: return tempButton;
            case GameStateType.ChoosingMilk: return milkButton;
            case GameStateType.PumpingSyrup: return syrupButton;
            case GameStateType.PouringEspresso: return espressoButton;
            case GameStateType.PlayingIceGame: return iceButton;
            case GameStateType.ServingDrinks: return reviewButton;
            case GameStateType.TakingOrder: return ticketsButton;
            case GameStateType.Pause: return pauseButton;
            default: return null;
        }
    }
}