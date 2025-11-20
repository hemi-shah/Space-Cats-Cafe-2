using UnityEngine;
using Game399.Shared;
using Game399.Shared.Enums;
using System.Collections.Generic;
using Game.Runtime;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    // Observable current state 
    public ObservableValue<GameStateType> CurrentState { get; private set; }

    // History of states for back navigation if needed
    private Stack<GameStateType> stateHistory = new Stack<GameStateType>();

    // track previous state
    public GameStateType PreviousState { get; private set; }

    private IGameLogger logger;

    void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
        
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize observable state
        CurrentState = new ObservableValue<GameStateType>(GameStateType.Title);
    }

    void Start()
    {
        // Subscribe to state changes for logging/debugging
        CurrentState.ChangeEvent += OnStateChanged;
        DialogueDataSetup setup = GetComponent<DialogueDataSetup>();
        if (setup != null)
        {
            setup.SetupAllDialogue();
        }
    }

    void OnDestroy()
    {
        CurrentState.ChangeEvent -= OnStateChanged;
    }
    
    public void ChangeState(GameStateType newState)
    {
        if (newState == GameStateType.PlayingIceGame)
        {
            var drink = ServiceResolver.Resolve<DrinkServices>()?.CurrentDrink;

            if (drink == null)
            {
                logger.LogError("[StateGuard] CurrentDrink is null when trying to enter Ice Game.");
            }
            else if (drink.Temp == Temperature.Hot)
            {
                logger.Log("[StateGuard] Hot drink — skipping IceGame state.");

                NavigationBar.Instance?.MarkStationCompleted(GameStateType.PlayingIceGame);
                ChangeState(GameStateType.ChoosingMilk);
                return;
            }
        }

        // Existing logic
        if (CurrentState.Value == newState)
        {
            logger.LogWarning($"Already in state: {newState}");
            return;
        }

        PreviousState = CurrentState.Value;
        stateHistory.Push(PreviousState);
        CurrentState.Value = newState;
    }
    
    public void GoToPreviousState()
    {
        if (stateHistory.Count > 0)
        {
            GameStateType previousState = stateHistory.Pop();
            PreviousState = CurrentState.Value;
            CurrentState.Value = previousState;
        }
        else
        {
            logger.LogWarning("No previous state in history");
        }
    }
    public void ClearHistory()
    {
        stateHistory.Clear();
    }
    
    private void OnStateChanged(GameStateType newState)
    {
        logger.Log($"State Changed: {PreviousState} -> {newState}");
    }
    public bool IsInState(GameStateType state)
    {
        return CurrentState.Value == state;
    }

    public bool IsInAnyState(params GameStateType[] states)
    {
        foreach (var state in states)
        {
            if (CurrentState.Value == state)
                return true;
        }
        return false;
    }
}