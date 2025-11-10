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
            Debug.LogWarning("OrderingScreen: no selected cat found in OrderManager!");
        }

        int rating = drinkVerifier.lastRating;
        Debug.Log("Rating: " + rating);

        if (rating <= 3)
        {
            feedbackText.text = "Keep practicing! The cat wants better.";
        }
        else if (rating > 3 && rating < 6)
        {
            feedbackText.text = "Good job! The cat enjoyed it.";
        }
        else if (rating == 7)
        {
            feedbackText.text = "Perfect! The cat is very happy!";
        }

        /*
        int score = Random.Range(60, 100);

        if (scoreText != null)
            scoreText.text = $"Score: {score}%";

        if (feedbackText != null)
        {
            if (score >= 90)
                feedbackText.text = "Perfect! The cat is very happy!";
            else if (score >= 70)
                feedbackText.text = "Good job! The cat enjoyed it.";
            else
                feedbackText.text = "Keep practicing! The cat wants better.";
        }
        */
    }

    private void OnNextOrder()
    {
        Debug.Log("Next order!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ServingDrinks);
        GameStateManager.Instance.ClearHistory();
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }
}