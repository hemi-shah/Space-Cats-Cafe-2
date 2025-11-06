using UnityEngine;
using UnityEngine.UI;

public class SyrupStationScreen : ScreenController
{
    [SerializeField] private Button vanillaButton;
    [SerializeField] private Button caramelButton;
    [SerializeField] private Button hazelnutButton;
    [SerializeField] private Button continueButton;

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
        
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinue);
            Debug.Log("SyrupScreen: Continue button ready");
        }
    }

    private void OnSyrupSelected(string syrup)
    {
        Debug.Log($"{syrup} syrup added");
    }

    private void OnContinue()
    {
        Debug.Log("Syrup done!");
        GameStateManager.Instance.ChangeState(GameStateType.PouringEspresso);
    }
}