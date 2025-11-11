using UnityEngine;
using UnityEngine.UI;

public class RatingStationScreen : ScreenController
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text feedbackText;
    [SerializeField] private Button nextOrderButton;
    [SerializeField] private Image catImage;
    
    public DrinkVerifier drinkVerifier;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ServingDrinks;
        
        if (nextOrderButton != null)
        {
            nextOrderButton.onClick.AddListener(OnNextOrder);
            Debug.Log("RatingStation: Next Order button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        
        CatDefinition cat = null;
        if (OrderManager.Instance != null)
        {
            cat = OrderManager.Instance.GetSelectedCat();
        }

        if (cat != null && catImage != null)
        {
            catImage.sprite = cat.catSprite;
            catImage.enabled = true;
        }
        else
        {
            Debug.LogWarning("RatingStation: no selected cat found in OrderManager!");
        }

        int rating = drinkVerifier.lastRating;
        Debug.Log("Rating: " + rating);

        // Increment cat counter for good drinks (4-5 stars)
        if (rating >= 4 && cat != null)
        {
            cat.counter++;
            Debug.Log($"Cat {cat.catName} counter increased to: {cat.counter}");
        }

        // Updated feedback to match 1-5 star rating system
        if (rating <= 2)
        {
            feedbackText.text = "Keep practicing! The cat wants better.";
        }
        else if (rating == 3)
        {
            feedbackText.text = "Not bad, but the cat expects more.";
        }
        else if (rating == 4)
        {
            feedbackText.text = "Good job! The cat enjoyed it.";
        }
        else if (rating == 5)
        {
            feedbackText.text = "Perfect! The cat is very happy!";
        }
    }

    private void OnNextOrder()
    {
        Debug.Log("Next order!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ServingDrinks);
        GameStateManager.Instance.ClearHistory();
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }
}