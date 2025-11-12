using UnityEngine;
using Game399.Shared;
using System.Collections.Generic;
using Game.Runtime;

/// <summary>
/// Manages game state transitions and notifies subscribers of changes
/// </summary>
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    // Observable current state - any script can subscribe to state changes
    public ObservableValue<GameStateType> CurrentState { get; private set; }

    // History of states for back navigation if needed
    private Stack<GameStateType> stateHistory = new Stack<GameStateType>();

    // Optional: track previous state
    public GameStateType PreviousState { get; private set; }

    void Awake()
    {
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

    /// <summary>
    /// Changes to a new game state
    /// </summary>
    public void ChangeState(GameStateType newState)
    {
        
        if (CurrentState.Value == newState)
        {
            Debug.LogWarning($"Already in state: {newState}");
            return;
        }

        // Store previous state
        PreviousState = CurrentState.Value;
        stateHistory.Push(PreviousState);

        // Change state (this will trigger ChangeEvent)
        CurrentState.Value = newState;
    }

    /// <summary>
    /// Go back to the previous state (if history exists)
    /// </summary>
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
            Debug.LogWarning("No previous state in history");
        }
    }

    /// <summary>
    /// Clear state history (useful when starting a new order)
    /// </summary>
    public void ClearHistory()
    {
        stateHistory.Clear();
    }

    /// <summary>
    /// Called whenever state changes - useful for debugging
    /// </summary>
    private void OnStateChanged(GameStateType newState)
    {
        Debug.Log($"State Changed: {PreviousState} -> {newState}");
    }

    /// <summary>
    /// Quick state check methods
    /// </summary>
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