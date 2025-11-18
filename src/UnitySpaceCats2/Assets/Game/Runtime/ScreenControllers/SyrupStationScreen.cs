using UnityEngine;
using UnityEngine.UI;

public class SyrupStationScreen : ScreenController
{
    [SerializeField] private Button vanillaButton;
    [SerializeField] private Button caramelButton;
    [SerializeField] private Button hazelnutButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PumpingSyrup;
        
        if (vanillaButton != null)
        {
            vanillaButton.onClick.AddListener(() => OnSyrupSelected("Vanilla"));
        }
        
        if (caramelButton != null)
        {
            caramelButton.onClick.AddListener(() => OnSyrupSelected("Caramel"));
        }
        
        if (hazelnutButton != null)
        {
            hazelnutButton.onClick.AddListener(() => OnSyrupSelected("Hazelnut"));
        }
    }

    private void OnSyrupSelected(string syrup)
    {
        logger.Log($"{syrup} syrup added");
    }

    private void OnContinue()
    {
        logger.Log("Syrup done!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PumpingSyrup);
        GameStateManager.Instance.ChangeState(GameStateType.PouringEspresso);
    }
}