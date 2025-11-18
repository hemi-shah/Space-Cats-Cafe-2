using UnityEngine;
using UnityEngine.UI;

public class SyrupStationScreen : ScreenController
{
    [SerializeField] private Button mochaButton;
    [SerializeField] private Button caramelButton;
    [SerializeField] private Button chocolateButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PumpingSyrup;
        
        if (mochaButton != null)
        {
            mochaButton.onClick.AddListener(() => OnSyrupSelected("Mocha"));
        }
        
        if (caramelButton != null)
        {
            caramelButton.onClick.AddListener(() => OnSyrupSelected("Caramel"));
        }
        
        if (chocolateButton != null)
        {
            chocolateButton.onClick.AddListener(() => OnSyrupSelected("Chocolate"));
        }
    }

    private void OnSyrupSelected(string syrup)
    {
        var station = FindObjectOfType<SyrupStation>();
        station?.PumpSyrup(syrup);
    }

    private void OnContinue()
    {
        Debug.Log("Syrup done!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PumpingSyrup);
        GameStateManager.Instance.ChangeState(GameStateType.PouringEspresso);
    }
}