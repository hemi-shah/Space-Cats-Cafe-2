using UnityEngine;
using UnityEngine.UI;

public class TemperatureSelectionScreen : ScreenController
{
    [SerializeField] private Button hotButton;
    [SerializeField] private Button coldButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingTemperature;
        
        if (hotButton != null)
        {
            hotButton.onClick.AddListener(OnHotSelected);
            Debug.Log("HotOrIcedStation: Hot button ready");
        }
        
        if (coldButton != null)
        {
            coldButton.onClick.AddListener(OnColdSelected);
            Debug.Log("HotOrIcedStation: Cold button ready");
        }
    }

    private void OnHotSelected()
    {
        Debug.Log("Hot drink selected!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingMilk);
    }

    private void OnColdSelected()
    {
        Debug.Log("Iced drink selected!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingTemperature);
        GameStateManager.Instance.ChangeState(GameStateType.PlayingIceGame);
    }
}