using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    [SerializeField] private DialoguePathData dialoguePath1Data;
    [SerializeField] private DialoguePathData dialoguePath2Data;
    [SerializeField] private DialoguePathData dialoguePath3Data;
    [SerializeField] private DialoguePathData dialoguePath4Data;
    
    [SerializeField] private CatCatalog catCatalog;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Call this at game start to randomly assign dialogue paths to all cats
    /// and reset their counters
    /// </summary>
    public void InitializeCatDialoguePaths()
    {
        if (catCatalog == null)
        {
            Debug.LogError("DialogueManager: CatCatalog is not assigned!");
            return;
        }

        // Assign dialogue paths to player cats and reset counters
        foreach (var cat in catCatalog.playerCats)
        {
            cat.dialoguePath = GetRandomDialoguePath();
            cat.counter = 0; // Reset counter
            Debug.Log($"Assigned dialogue path {cat.dialoguePath} to {cat.catName}");
        }

        // Assign dialogue paths to customer cats and reset counters
        foreach (var cat in catCatalog.customerCats)
        {
            cat.dialoguePath = GetRandomDialoguePath();
            cat.counter = 0; // Reset counter
            Debug.Log($"Assigned dialogue path {cat.dialoguePath} to {cat.catName}");
        }
    }

    /// <summary>
    /// Randomly returns 1, 2, 3, or 4
    /// </summary>
    private int GetRandomDialoguePath()
    {
        return Random.Range(1, 5); // 1, 2, 3, or 4
    }

    /// <summary>
    /// Gets the appropriate dialogue based on cat's star counter, dialogue path, and drink rating
    /// </summary>
    public DialogueNode GetDialogue(CatDefinition cat, int drinkRating)
    {
        if (cat == null)
        {
            Debug.LogError("DialogueManager: Cat is null!");
            return null;
        }

        // Get the correct dialogue data based on cat's dialogue path
        DialoguePathData dialogueData = cat.dialoguePath switch
        {
            1 => dialoguePath1Data,
            2 => dialoguePath2Data,
            3 => dialoguePath3Data,
            4 => dialoguePath4Data,
            _ => null
        };

        if (dialogueData == null)
        {
            Debug.LogError($"DialogueManager: No dialogue data for path {cat.dialoguePath}");
            return null;
        }

        // Determine which dialogue to use based on star counter and drink quality
        DialogueNode dialogue = null;

        if (cat.counter == 0)
        {
            // No stars yet - use no star dialogue
            dialogue = dialogueData.noStarDialogue;
        }
        else if (cat.counter <= 2)
        {
            // 1 star tier
            bool goodDrink = drinkRating >= 4;
            dialogue = goodDrink ? dialogueData.oneStarGoodDialogue : dialogueData.oneStarBadDialogue;
        }
        else if (cat.counter <= 4)
        {
            // 3 star tier
            bool goodDrink = drinkRating >= 4;
            dialogue = goodDrink ? dialogueData.threeStarGoodDialogue : dialogueData.threeStarBadDialogue;
        }
        else
        {
            // 5 star tier (counter >= 5)
            bool goodDrink = drinkRating == 5;
            dialogue = goodDrink ? dialogueData.fiveStarGoodDialogue : dialogueData.fiveStarBadDialogue;
        }

        if (dialogue == null)
        {
            Debug.LogError("DialogueManager: No dialogue found!");
        }

        return dialogue;
    }

    /// <summary>
    /// Resets all cat dialogue paths and counters (useful for new game)
    /// </summary>
    public void ResetAllCatDialoguePaths()
    {
        if (catCatalog == null) return;

        foreach (var cat in catCatalog.playerCats)
        {
            cat.dialoguePath = 0;
            cat.counter = 0;
        }

        foreach (var cat in catCatalog.customerCats)
        {
            cat.dialoguePath = 0;
            cat.counter = 0;
        }
        
        Debug.Log("All cat dialogue paths and counters have been reset!");
    }
}