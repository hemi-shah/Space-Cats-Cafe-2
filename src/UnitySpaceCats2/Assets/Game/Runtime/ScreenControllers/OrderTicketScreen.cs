using UnityEngine;
using UnityEngine.UI;

public class OrderTicketScreen : ScreenController
{
    [SerializeField] private Text orderDetailsText;
    [SerializeField] private Button startMakingButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.OrderTicketReceived;
        
        if (startMakingButton != null)
        {
            startMakingButton.onClick.AddListener(OnStartMaking);
            Debug.Log("OrderTicketScreen: Start Making button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        if (orderDetailsText != null)
        {
            orderDetailsText.text = "Order Ticket:\n- Iced Coffee\n- 2 Pumps Vanilla\n- Oat Milk\n- Whipped Cream";
        }
    }

    private void OnStartMaking()
    {
        Debug.Log("Starting to make drink!");
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingTemperature);
    }
}