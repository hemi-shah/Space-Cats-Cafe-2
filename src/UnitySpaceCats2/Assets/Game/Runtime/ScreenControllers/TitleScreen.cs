using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : ScreenController
{
    [SerializeField] private Button startButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.Title;
        
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartClicked);
            logger.Log("TitleScreen: Start button ready");
        }
    }

    private void OnStartClicked()
    {
        logger.Log("Start clicked!");
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingCat);
    }
}