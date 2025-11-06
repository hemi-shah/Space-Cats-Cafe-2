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
            orderDetailsText.text = "A cute cat approaches!\n\nReady to take their order?";
        }
    }

    private void OnTakeOrder()
    {
        Debug.Log("Order taken from cat!");
        GameStateManager.Instance.ChangeState(GameStateType.OrderTicketReceived);
    }
}