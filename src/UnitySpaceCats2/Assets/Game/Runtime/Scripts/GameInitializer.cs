using UnityEngine;
using Game.Runtime;

/// <summary>
/// Handles game initialization tasks like assigning dialogue paths to cats
/// Attach this to a GameObject in your first/main scene
/// </summary>
public class GameInitializer : MonoBehaviour
{
    [SerializeField] private bool initializeOnStart = true;
    
    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    void Start()
    {
        if (initializeOnStart)
        {
            InitializeGame();
        }
    }

    public void InitializeGame()
    {
        logger.Log("Initializing game...");
        
        // Initialize dialogue paths for all cats
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.InitializeCatDialoguePaths();
            logger.Log("Cat dialogue paths initialized!");
        }
        else
        {
            logger.LogWarning("DialogueManager not found!");
        }
    }

    /// <summary>
    /// Call this to reset the game (new game button, etc.)
    /// </summary>
    public void ResetGame()
    {
        logger.Log("Resetting game...");
        
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ResetAllCatDialoguePaths();
        }
        
        // Reinitialize
        InitializeGame();
    }
}