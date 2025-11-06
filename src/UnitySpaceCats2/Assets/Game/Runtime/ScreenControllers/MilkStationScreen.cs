using UnityEngine;
using UnityEngine.UI;

public class MilkStationScreen : ScreenController
{
    [SerializeField] private Button oatMilkButton;
    [SerializeField] private Button almondMilkButton;
    [SerializeField] private Button wholeMilkButton;
    [SerializeField] private Button skipButton;

    private string selectedMilk = "";

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingMilk;
        
        if (oatMilkButton != null)
        {
            oatMilkButton.onClick.AddListener(() => OnMilkSelected("Oat Milk"));
            Debug.Log("MilkScreen: Oat Milk button ready");
        }
        
        if (almondMilkButton != null)
        {
            almondMilkButton.onClick.AddListener(() => OnMilkSelected("Almond Milk"));
            Debug.Log("MilkScreen: Almond Milk button ready");
        }
        
        if (wholeMilkButton != null)
        {
            wholeMilkButton.onClick.AddListener(() => OnMilkSelected("Whole Milk"));
            Debug.Log("MilkScreen: Whole Milk button ready");
        }
        
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(OnSkip);
            Debug.Log("MilkScreen: Skip button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        selectedMilk = "";
    }

    private void OnMilkSelected(string milkType)
    {
        selectedMilk = milkType;
        Debug.Log($"{milkType} selected!");
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }

    private void OnSkip()
    {
        Debug.Log("Milk skipped");
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }
}