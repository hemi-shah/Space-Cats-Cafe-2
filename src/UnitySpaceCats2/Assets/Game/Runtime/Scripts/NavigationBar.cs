using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Navigation bar controller - handles button clicks to navigate between screens
/// Locks/unlocks stations based on order progress
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

    // Track which stations are locked/unlocked
    private HashSet<GameStateType> lockedStations = new HashSet<GameStateType>();
    private HashSet<GameStateType> completedStations = new HashSet<GameStateType>();
    
    // Track if we chose hot or cold
    private bool isHotDrink = false;
    private bool isColdDrink = false;

    void Start()
    {
        SetupButtonListeners();

        // Subscribe to state changes to update button visuals and locks
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
        if (lockedStations.Contains(targetState))
        {
            Debug.LogWarning($"Cannot navigate to {targetState} - station is locked");
            return;
        }

        GameStateManager.Instance.ChangeState(targetState);
    }

    /// <summary>
    /// Update button visuals and lock states based on current state
    /// </summary>
    private void OnStateChanged(GameStateType newState)
    {
        // Handle state-specific logic
        switch (newState)
        {
            case GameStateType.WaitingforCustomers:
                // Reset everything when waiting for customers
                ResetAllStations();
                break;

            case GameStateType.TakingOrder:
                // Lock everything except order taking
                // LockAllStations();
                break;

            case GameStateType.OrderTicketReceived:
                // Unlock only hot/cold selection
                lockedStations.Clear();
                LockAllStationsExcept(GameStateType.ChoosingTemperature);
                break;

            case GameStateType.ChoosingTemperature:
                // When leaving temperature selection, track choice and lock appropriately
                break;
        }

        // Update all button visuals
        UpdateButtonVisual(waitingScreenButton, GameStateType.WaitingforCustomers, newState);
        UpdateButtonVisual(hotOrColdDrinkButton, GameStateType.ChoosingTemperature, newState);
        UpdateButtonVisual(iceGameButton, GameStateType.PlayingIceGame, newState);
        UpdateButtonVisual(syrupButton, GameStateType.PumpingSyrup, newState);
        UpdateButtonVisual(espressoButton, GameStateType.PouringEspresso, newState);
        UpdateButtonVisual(milkButton, GameStateType.ChoosingMilk, newState);
        UpdateButtonVisual(toppingsButton, GameStateType.PlacingToppings, newState);
        UpdateButtonVisual(reviewButton, GameStateType.ServingDrinks, newState);
    }

    /// <summary>
    /// Called by screen controllers when they complete their action
    /// </summary>
    public void MarkStationCompleted(GameStateType stationType)
    {
        completedStations.Add(stationType);
        lockedStations.Add(stationType);

        switch (stationType)
        {
            case GameStateType.ChoosingTemperature:
                // Lock temperature selection and waiting
                lockedStations.Add(GameStateType.ChoosingTemperature);
                lockedStations.Add(GameStateType.WaitingforCustomers);
                
                // Check if we went hot or cold based on next state
                if (GameStateManager.Instance.CurrentState.Value == GameStateType.ChoosingMilk)
                {
                    // Hot drink chosen - lock ice game, unlock milk
                    isHotDrink = true;
                    lockedStations.Add(GameStateType.PlayingIceGame);
                    lockedStations.Remove(GameStateType.ChoosingMilk);
                }
                else if (GameStateManager.Instance.CurrentState.Value == GameStateType.PlayingIceGame)
                {
                    // Cold drink chosen - unlock ice game
                    isColdDrink = true;
                    lockedStations.Remove(GameStateType.PlayingIceGame);
                }
                break;

            case GameStateType.PlayingIceGame:
                // Lock ice game, temperature, and waiting after completing
                lockedStations.Add(GameStateType.PlayingIceGame);
                lockedStations.Add(GameStateType.ChoosingTemperature);
                lockedStations.Add(GameStateType.WaitingforCustomers);
                // Unlock milk
                lockedStations.Remove(GameStateType.ChoosingMilk);
                break;

            case GameStateType.ChoosingMilk:
                // Lock milk, unlock syrup
                lockedStations.Add(GameStateType.ChoosingMilk);
                lockedStations.Remove(GameStateType.PumpingSyrup);
                break;

            case GameStateType.PumpingSyrup:
                // Lock syrup, unlock espresso
                lockedStations.Add(GameStateType.PumpingSyrup);
                lockedStations.Remove(GameStateType.PouringEspresso);
                break;

            case GameStateType.PouringEspresso:
                // Lock espresso, unlock toppings
                lockedStations.Add(GameStateType.PouringEspresso);
                lockedStations.Remove(GameStateType.PlacingToppings);
                break;

            case GameStateType.PlacingToppings:
                // Lock toppings, unlock review
                lockedStations.Add(GameStateType.PlacingToppings);
                lockedStations.Remove(GameStateType.ServingDrinks);
                break;

            case GameStateType.ServingDrinks:
                // After review, unlock waiting room
                lockedStations.Clear();
                ResetAllStations();
                break;
        }

        // Refresh button states
        OnStateChanged(GameStateManager.Instance.CurrentState.Value);
    }

    private void UpdateButtonVisual(Button button, GameStateType buttonState, GameStateType currentState)
    {
        if (button == null) return;

        ColorBlock colors = button.colors;
        bool isLocked = lockedStations.Contains(buttonState);
        
        // Set interactable state
        button.interactable = !isLocked;

        if (buttonState == currentState)
        {
            // Highlight active button
            colors.normalColor = activeColor;
            colors.selectedColor = activeColor;
        }
        else if (isLocked)
        {
            // Locked button
            colors.normalColor = disabledColor;
            colors.selectedColor = disabledColor;
        }
        else
        {
            // Normal state
            colors.normalColor = normalColor;
            colors.selectedColor = normalColor;
        }

        button.colors = colors;
    }

    private void LockAllStations()
    {
        lockedStations.Add(GameStateType.WaitingforCustomers);
        lockedStations.Add(GameStateType.ChoosingTemperature);
        lockedStations.Add(GameStateType.PlayingIceGame);
        lockedStations.Add(GameStateType.ChoosingMilk);
        lockedStations.Add(GameStateType.PumpingSyrup);
        lockedStations.Add(GameStateType.PouringEspresso);
        lockedStations.Add(GameStateType.PlacingToppings);
        lockedStations.Add(GameStateType.ServingDrinks);
    }

    private void LockAllStationsExcept(GameStateType exception)
    {
        LockAllStations();
        lockedStations.Remove(exception);
    }

    private void ResetAllStations()
    {
        lockedStations.Clear();
        completedStations.Clear();
        isHotDrink = false;
        isColdDrink = false;
    }

    public static NavigationBar Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}