using UnityEngine;
using UnityEngine.UI;

public class ReviewScreen : ScreenController
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text feedbackText;
    [SerializeField] private Button nextOrderButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ServingDrinks;
        
        if (nextOrderButton != null)
        {
            nextOrderButton.onClick.AddListener(OnNextOrder);
            Debug.Log("ReviewScreen: Next Order button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        
        int score = Random.Range(60, 100);
        
        if (scoreText != null)
            scoreText.text = $"Score: {score}%";
        
        if (feedbackText != null)
        {
            if (score >= 90)
                feedbackText.text = "Perfect!";
            else if (score >= 70)
                feedbackText.text = "Good job!";
            else
                feedbackText.text = "Keep practicing!";
        }
    }

    private void OnNextOrder()
    {
        Debug.Log("Next order!");
        GameStateManager.Instance.ClearHistory();
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }
}