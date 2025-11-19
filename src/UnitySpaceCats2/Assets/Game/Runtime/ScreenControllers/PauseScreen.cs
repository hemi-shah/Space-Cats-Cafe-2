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
            logger.Log("PauseScreen: Resume button ready");
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenu);
            logger.Log("PauseScreen: Main Menu button ready");
        }
    }

    private void OnResume()
    {
        logger.Log("Resuming game");
        GameStateManager.Instance.GoToPreviousState();
    }

    private void OnMainMenu()
    {
        logger.Log("Returning to main menu");
        GameStateManager.Instance.ClearHistory();
        GameStateManager.Instance.ChangeState(GameStateType.Title);
    }
}