using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : ScreenController
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.Pause;
        
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(OnResume);
            Debug.Log("PauseScreen: Resume button ready");
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenu);
            Debug.Log("PauseScreen: Main Menu button ready");
        }
    }

    private void OnResume()
    {
        Debug.Log("Resuming game");
        GameStateManager.Instance.GoToPreviousState();
    }

    private void OnMainMenu()
    {
        Debug.Log("Returning to main menu");
        GameStateManager.Instance.ClearHistory();
        GameStateManager.Instance.ChangeState(GameStateType.Title);
    }
}