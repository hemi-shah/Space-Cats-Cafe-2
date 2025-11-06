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
            Debug.Log("WaitingScreen: Pause button ready");
        }
        
        if (catsEncounteredButton != null)
        {
            catsEncounteredButton.onClick.AddListener(OnCatsClicked);
            Debug.Log("WaitingScreen: Cats Encountered button ready");
        }
        
        if (takeOrderButton != null)
        {
            takeOrderButton.onClick.AddListener(OnTakeOrder);
            Debug.Log("WaitingScreen: Take Order button ready");
        }
    }

    private void OnPauseClicked()
    {
        Debug.Log("Pause clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.Pause);
    }

    private void OnCatsClicked()
    {
        Debug.Log("Cats Encountered clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.ViewingCollectedCats);
    }

    private void OnTakeOrder()
    {
        Debug.Log("Taking order from cat!");
        GameStateManager.Instance.ChangeState(GameStateType.TakingOrder);
    }
}