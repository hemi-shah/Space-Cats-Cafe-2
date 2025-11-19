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
            logger.Log("MilkScreen: Oat Milk button ready");
        }
        
        if (almondMilkButton != null)
        {
            almondMilkButton.onClick.AddListener(() => OnMilkSelected("Almond Milk"));
            logger.Log("MilkScreen: Almond Milk button ready");
        }
        
        if (wholeMilkButton != null)
        {
            wholeMilkButton.onClick.AddListener(() => OnMilkSelected("Whole Milk"));
            logger.Log("MilkScreen: Whole Milk button ready");
        }
        
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(OnSkip);
            logger.Log("MilkScreen: Skip button ready");
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
        logger.Log($"{milkType} selected!");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingMilk);
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }

    private void OnSkip()
    {
        logger.Log("Milk skipped");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.ChoosingMilk);
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }
}