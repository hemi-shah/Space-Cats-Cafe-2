using UnityEngine;
using UnityEngine.UI;

public class OrderingScreen : ScreenController
{
    [SerializeField] private Button takeOrderButton;
    [SerializeField] private Text orderDetailsText;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.TakingOrder;
        
        if (takeOrderButton != null)
        {
            takeOrderButton.onClick.AddListener(OnTakeOrder);
            Debug.Log("OrderingScreen: Button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        if (orderDetailsText != null)
        {
            orderDetailsText.text = "Customer wants:\n- Iced Coffee\n- 2 Pumps Vanilla\n- Oat Milk";
        }
    }

    private void OnTakeOrder()
    {
        Debug.Log("Order taken!");
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingTemperature);
    }
}