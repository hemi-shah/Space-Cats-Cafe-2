using UnityEngine;
using Game.Runtime;

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
    protected abstract void SetupButtons();
    
    protected virtual void OnScreenShow()
    {
        logger.Log($"{GetType().Name} is now visible");
    }
    
    protected virtual void OnScreenHide()
    {
        logger.Log($"{GetType().Name} is now hidden");
    }
}