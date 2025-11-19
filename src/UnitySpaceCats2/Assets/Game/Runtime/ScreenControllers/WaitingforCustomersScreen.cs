using UnityEngine;
using UnityEngine.UI;

public class WaitingForCustomersScreen : ScreenController
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button catsEncounteredButton;
    [SerializeField] private Button takeOrderButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.WaitingforCustomers;
        
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(OnPauseClicked);
            logger.Log("WaitingScreen: Pause button ready");
        }
        
        if (catsEncounteredButton != null)
        {
            catsEncounteredButton.onClick.AddListener(OnCatsClicked);
            logger.Log("WaitingScreen: Cats Encountered button ready");
        }
        
        if (takeOrderButton != null)
        {
            takeOrderButton.onClick.AddListener(OnTakeOrder);
            logger.Log("WaitingScreen: Take Order button ready");
        }
    }

    private void OnPauseClicked()
    {
        logger.Log("Pause clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.Pause);
    }

    private void OnCatsClicked()
    {
        logger.Log("Cats Encountered clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.ViewingCollectedCats);
    }

    private void OnTakeOrder()
    {
        logger.Log("Taking order from cat!");
        GameStateManager.Instance.ChangeState(GameStateType.TakingOrder);
    }
}