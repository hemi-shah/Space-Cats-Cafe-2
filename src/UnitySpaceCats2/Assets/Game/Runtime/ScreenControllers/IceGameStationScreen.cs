using UnityEngine;
using UnityEngine.UI;

public class IceGameStationScreen : ScreenController
{
    [SerializeField] private Button completeButton;
    [SerializeField] private Text instructionText;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PlayingIceGame;
        
        if (completeButton != null)
        {
            completeButton.onClick.AddListener(OnIceGameComplete);
            Debug.Log("IceGameStation: Complete button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        if (instructionText != null)
        {
            instructionText.text = "Add ice to the cup!";
        }
    }

    private void OnIceGameComplete()
    {
        Debug.Log("Ice game completed!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PlayingIceGame);
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
    }
}