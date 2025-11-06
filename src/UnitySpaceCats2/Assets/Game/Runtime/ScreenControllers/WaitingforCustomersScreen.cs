using UnityEngine;
using UnityEngine.UI;

public class WaitingForCustomersScreen : ScreenController
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button catsEncounteredButton;

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
            Debug.Log("WaitingScreen: Cats button ready");
        }
    }

    private void OnPauseClicked()
    {
        Debug.Log("Pause clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.Pause);
    }

    private void OnCatsClicked()
    {
        Debug.Log("Cats clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.ViewingCollectedCats);
    }
}