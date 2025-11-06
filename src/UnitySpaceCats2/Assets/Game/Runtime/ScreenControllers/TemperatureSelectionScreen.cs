using UnityEngine;
using UnityEngine.UI;

public class TemperatureSelectionScreen : ScreenController
{
    [SerializeField] private Button hotButton;
    [SerializeField] private Button icedButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingTemperature;
        
        if (hotButton != null)
        {
            hotButton.onClick.AddListener(OnHotSelected);
            Debug.Log("TempScreen: Hot button ready");
        }
        
        if (icedButton != null)
        {
            icedButton.onClick.AddListener(OnIcedSelected);
            Debug.Log("TempScreen: Iced button ready");
        }
    }

    private void OnHotSelected()
    {
        Debug.Log("Hot selected!");
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
    }

    private void OnIcedSelected()
    {
        Debug.Log("Iced selected!");
        GameStateManager.Instance.ChangeState(GameStateType.PlayingIceGame);
    }
}