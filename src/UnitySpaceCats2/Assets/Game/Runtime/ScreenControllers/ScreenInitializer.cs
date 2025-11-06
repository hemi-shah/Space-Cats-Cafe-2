using UnityEngine;

/// <summary>
/// Ensures all screen content is set up properly before the game starts
/// Attach this to your Canvas
/// </summary>
[DefaultExecutionOrder(-100)]
public class ScreenInitializer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool logInitialization = true;
    
    void Awake()
    {
        if (logInitialization)
        {
            Debug.Log("═══════════════════════════════════════");
            Debug.Log("<color=cyan>[ScreenInitializer] Setting up screens...</color>");
            Debug.Log("═══════════════════════════════════════");
        }
        
        // Find all ScreenControllers
        ScreenController[] allScreens = GetComponentsInChildren<ScreenController>(true);
        
        if (allScreens.Length == 0)
        {
            Debug.LogWarning("[ScreenInitializer] No ScreenControllers found!");
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
                    Debug.Log($"  • Enabled {screen.gameObject.name} GameObject");
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
                        Debug.Log($"  • Enabled {root.name} content (will be hidden by ScreenController if needed)");
                    }
                }
            }
        }
        
        if (logInitialization)
        {
            Debug.Log($"<color=green>[ScreenInitializer] Prepared {allScreens.Length} screens</color>");
            Debug.Log("═══════════════════════════════════════");
        }
    }
}