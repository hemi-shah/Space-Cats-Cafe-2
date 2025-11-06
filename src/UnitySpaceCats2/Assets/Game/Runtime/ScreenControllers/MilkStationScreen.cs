using UnityEngine;
using UnityEngine.UI;

public class MilkStationScreen : ScreenController
{
    [SerializeField] private Slider milkAmountSlider;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button skipButton;

    private float currentMilkAmount = 0f;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingMilk;
        
        if (milkAmountSlider != null)
        {
            milkAmountSlider.onValueChanged.AddListener(OnMilkChanged);
        }
        
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirm);
            Debug.Log("MilkScreen: Confirm button ready");
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
        currentMilkAmount = 0f;
        if (milkAmountSlider != null)
            milkAmountSlider.value = 0f;
    }

    private void OnMilkChanged(float amount)
    {
        currentMilkAmount = amount;
    }

    private void OnConfirm()
    {
        Debug.Log($"Milk confirmed: {currentMilkAmount}");
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }

    private void OnSkip()
    {
        Debug.Log("Milk skipped");
        GameStateManager.Instance.ChangeState(GameStateType.PumpingSyrup);
    }
}