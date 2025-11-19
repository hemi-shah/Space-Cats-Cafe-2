using UnityEngine;
using Game.Runtime;

/// <summary>
/// Ensures all screen content is set up properly before the game starts
/// Attach this to your Canvas
/// </summary>
[DefaultExecutionOrder(-100)]
public class ScreenInitializer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool logInitialization = true;

    private IGameLogger logger;
    
    void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
        
        if (logInitialization)
        {
            logger.Log("═══════════════════════════════════════");
            logger.Log("<color=cyan>[ScreenInitializer] Setting up screens...</color>");
            logger.Log("═══════════════════════════════════════");
        }
        
        // Find all ScreenControllers
        ScreenController[] allScreens = GetComponentsInChildren<ScreenController>(true);
        
        if (allScreens.Length == 0)
        {
            logger.LogWarning("[ScreenInitializer] No ScreenControllers found!");
            return;
        }
        
        foreach (var screen in allScreens)
        {
            // Make sure the GameObject with ScreenController is enabled
            if (!screen.gameObject.activeSelf)
            {
                screen.gameObject.SetActive(true);
                if (logInitialization)
                {
                    logger.Log($"  • Enabled {screen.gameObject.name} GameObject");
                }
            }
            
            // Get the screenRoot using reflection
            var screenRootField = typeof(ScreenController).GetField("screenRoot", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            GameObject root = (GameObject)screenRootField.GetValue(screen);
            
            // If screenRoot is assigned and different from the GameObject
            if (root != null && root != screen.gameObject)
            {
                // IMPORTANT: Make sure screenRoot STARTS enabled
                // ScreenController will handle hiding it if needed
                if (!root.activeSelf)
                {
                    root.SetActive(true);
                    if (logInitialization)
                    {
                        logger.Log($"  • Enabled {root.name} content (will be hidden by ScreenController if needed)");
                    }
                }
            }
        }
        
        if (logInitialization)
        {
            logger.Log($"<color=green>[ScreenInitializer] Prepared {allScreens.Length} screens</color>");
            logger.Log("═══════════════════════════════════════");
        }
    }
}