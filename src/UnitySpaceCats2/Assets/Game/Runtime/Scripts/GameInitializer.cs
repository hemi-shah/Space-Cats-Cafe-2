using UnityEngine;
using Game.Runtime;

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
        logger.Log("initializing game");
        
        // initialize dialogue paths for all cats
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.InitializeCatDialoguePaths();
            logger.Log("dialogue paths initialized");
        }
        else
        {
            logger.LogWarning("DialogueManager not found");
        }
    }
    
    public void ResetGame()
    {
        logger.Log("resetting game");
        
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ResetAllCatDialoguePaths();
        }
        
        // Reinitialize
        InitializeGame();
    }
}