using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RatingStationScreen : ScreenController
{
    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Button playerChoice1Button;
    [SerializeField] private Button playerChoice2Button;
    [SerializeField] private Text playerChoice1Text;
    [SerializeField] private Text playerChoice2Text;
    [SerializeField] private Button nextOrderButton;
    [SerializeField] private Image catImage;
    
    [Header("Dependencies")]
    public DrinkVerifier drinkVerifier;

    private DialogueNode currentDialogue;
    private CatDefinition currentCat;
    private int currentRating;
    private bool showingFollowUp = false;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ServingDrinks;
        
        if (nextOrderButton != null)
        {
            nextOrderButton.onClick.AddListener(OnNextOrder);
        }

        if (playerChoice1Button != null)
        {
            playerChoice1Button.onClick.AddListener(OnPlayerChoice1);
        }

        if (playerChoice2Button != null)
        {
            playerChoice2Button.onClick.AddListener(OnPlayerChoice2);
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        
        showingFollowUp = false;
        currentCat = null;
        
        if (OrderManager.Instance != null)
        {
            currentCat = OrderManager.Instance.GetSelectedCat();
        }

        if (currentCat != null && catImage != null)
        {
            catImage.sprite = currentCat.catSprite;
            catImage.enabled = true;
        }
        else
        {
            logger.LogWarning("RatingStation: no selected cat found in OrderManager!");
        }

        currentRating = drinkVerifier.lastRating;
        logger.Log("Rating: " + currentRating);

        // Increment cat counter for good drinks (4-5 stars)
        if (currentRating >= 4 && currentCat != null)
        {
            currentCat.counter++;
            logger.Log($"Cat {currentCat.catName} counter increased to: {currentCat.counter}");
        }

        // Get and display dialogue
        if (DialogueManager.Instance != null && currentCat != null)
        {
            currentDialogue = DialogueManager.Instance.GetDialogue(currentCat, currentRating);
            DisplayInitialDialogue();
        }
        else
        {
            // Fallback if no dialogue system
            DisplayFallbackFeedback();
        }
    }

    private void DisplayInitialDialogue()
    {
        if (currentDialogue == null)
        {
            DisplayFallbackFeedback();
            return;
        }

        // Show customer's initial line
        if (dialogueText != null)
        {
            dialogueText.text = currentDialogue.customerLine;
        }

        // Show player choice 1 button (if text exists)
        if (playerChoice1Button != null && playerChoice1Text != null)
        {
            if (!string.IsNullOrEmpty(currentDialogue.playerResponseOption1))
            {
                playerChoice1Text.text = currentDialogue.playerResponseOption1;
                playerChoice1Button.gameObject.SetActive(true);
            }
            else
            {
                playerChoice1Button.gameObject.SetActive(false);
            }
        }

        // Show player choice 2 button (if text exists)
        if (playerChoice2Button != null && playerChoice2Text != null)
        {
            if (!string.IsNullOrEmpty(currentDialogue.playerResponseOption2))
            {
                playerChoice2Text.text = currentDialogue.playerResponseOption2;
                playerChoice2Button.gameObject.SetActive(true);
            }
            else
            {
                playerChoice2Button.gameObject.SetActive(false);
            }
        }

        // If no player choices exist, skip straight to next order
        if (string.IsNullOrEmpty(currentDialogue.playerResponseOption1) && 
            string.IsNullOrEmpty(currentDialogue.playerResponseOption2))
        {
            if (nextOrderButton != null)
            {
                nextOrderButton.gameObject.SetActive(true);
            }
        }
        else
        {
            // Hide next order button until dialogue is complete
            if (nextOrderButton != null)
            {
                nextOrderButton.gameObject.SetActive(false);
            }
        }
    }

    private void OnPlayerChoice1()
    {
        if (showingFollowUp) return;
        
        showingFollowUp = true;
        
        // Show customer's follow-up response (if it exists)
        if (dialogueText != null && currentDialogue != null)
        {
            if (!string.IsNullOrEmpty(currentDialogue.customerFollowUp1))
            {
                dialogueText.text = currentDialogue.customerFollowUp1;
            }
        }

        // Hide choice buttons
        HideChoiceButtons();

        // Check if this dialogue has remake option
        if (currentDialogue != null && currentDialogue.hasRemakeOption)
        {
            // Trigger remake - go back to drink making
            Invoke(nameof(TriggerRemake), 2f); // Wait 2 seconds before transitioning
        }
        else
        {
            // Show next order button
            if (nextOrderButton != null)
            {
                nextOrderButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnPlayerChoice2()
    {
        if (showingFollowUp) return;
        
        showingFollowUp = true;
        
        // Show customer's follow-up response (if it exists)
        if (dialogueText != null && currentDialogue != null)
        {
            if (!string.IsNullOrEmpty(currentDialogue.customerFollowUp2))
            {
                dialogueText.text = currentDialogue.customerFollowUp2;
            }
        }

        // Hide choice buttons
        HideChoiceButtons();

        // Show next order button
        if (nextOrderButton != null)
        {
            nextOrderButton.gameObject.SetActive(true);
        }
    }

    private void HideChoiceButtons()
    {
        if (playerChoice1Button != null)
        {
            playerChoice1Button.gameObject.SetActive(false);
        }

        if (playerChoice2Button != null)
        {
            playerChoice2Button.gameObject.SetActive(false);
        }
    }

    private void TriggerRemake()
    {
        logger.Log("Triggering drink remake...");
        // Go back to drink making state
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingTemperature);
    }

    private void DisplayFallbackFeedback()
    {
        // Original feedback system as fallback
        if (dialogueText != null)
        {
            if (currentRating <= 2)
            {
                dialogueText.text = "Keep practicing! The cat wants better.";
            }
            else if (currentRating == 3)
            {
                dialogueText.text = "Not bad, but the cat expects more.";
            }
            else if (currentRating == 4)
            {
                dialogueText.text = "Good job! The cat enjoyed it.";
            }
            else if (currentRating == 5)
            {
                dialogueText.text = "Perfect! The cat is very happy!";
            }
        }

        // Hide choice buttons and show next order
        HideChoiceButtons();
        if (nextOrderButton != null)
        {
            nextOrderButton.gameObject.SetActive(true);
        }
    }

    private void OnNextOrder()
    {
        logger.Log("Next order!");
        
        // Remove the current cat unless there's a remake option
        if (currentDialogue != null && !currentDialogue.hasRemakeOption && currentCat != null)
        {
            RemoveCurrentCat();
        }
        
        OrderManager.Instance.MarkDrinkCompleted(true);
        
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ServingDrinks);
        GameStateManager.Instance.ClearHistory();
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }

    private void RemoveCurrentCat()
    {
        if (catImage != null)
        {
            catImage.overrideSprite = null;
            catImage.sprite = null;
            logger.Log("Removing cat image");
            catImage.enabled = false;
        }

        if (currentCat == null)
            return;
        
        // Find and destroy the cat GameObject in the scene
        CatView[] allCatViews = FindObjectsOfType<CatView>(true);
        foreach (CatView catView in allCatViews)
        {
            if (catView.GetCatDefinition() == currentCat)
            {
                logger.Log($"Removing cat: {currentCat.catName}");
                //currentCat = null;
                //logger.Log($"Current cat: {currentCat.catName}");
                Destroy(catView.gameObject);
                break;
            }
        }
        
        currentCat = null;
        OrderManager.Instance.ClearSelectedCat();
    }
}