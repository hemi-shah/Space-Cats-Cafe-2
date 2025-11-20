using UnityEngine;
using Game.Runtime;

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
            logger.Log("Setting up screens");
        }
        
        // Find all ScreenControllers
        ScreenController[] allScreens = GetComponentsInChildren<ScreenController>(true);
        
        if (allScreens.Length == 0)
        {
            return;
        }
        
        foreach (var screen in allScreens)
        {

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
                if (!root.activeSelf)
                {
                    root.SetActive(true);
                    if (logInitialization)
                    {
                        logger.Log($" enabled {root.name} content ");
                    }
                }
            }
        }
        
        if (logInitialization)
        {
            logger.Log($" prepared {allScreens.Length} screens</color>");
        }
    }
}