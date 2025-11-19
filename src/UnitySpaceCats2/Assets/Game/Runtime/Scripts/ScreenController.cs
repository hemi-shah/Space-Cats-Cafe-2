using UnityEngine;
using Game.Runtime;

/// <summary>
/// Simple base class for all screens
/// Each screen GameObject must be ENABLED in the scene hierarchy
/// </summary>
public abstract class ScreenController : MonoBehaviour
{
    [Header("Screen Settings")]
    [SerializeField] protected GameStateType associatedState;

    protected IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    void Start()
    {
        // Set up button listeners immediately
        SetupButtons();
        
        // Subscribe to state changes
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.CurrentState.ChangeEvent += OnStateChanged;
            // Check initial state
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

    private void OnStateChanged(GameStateType newState)
    {
        if (newState == associatedState)
        {
            ShowScreen();
        }
        else
        {
            HideScreen();
        }
    }

    private void ShowScreen()
    {
        gameObject.SetActive(true);
        OnScreenShow();
    }

    private void HideScreen()
    {
        gameObject.SetActive(false);
        OnScreenHide();
    }

    /// <summary>
    /// Override this in child classes to set up button listeners
    /// </summary>
    protected abstract void SetupButtons();

    /// <summary>
    /// Called when screen becomes visible
    /// </summary>
    protected virtual void OnScreenShow()
    {
        logger.Log($"{GetType().Name} is now visible");
    }

    /// <summary>
    /// Called when screen is hidden
    /// </summary>
    protected virtual void OnScreenHide()
    {
        logger.Log($"{GetType().Name} is now hidden");
    }
}