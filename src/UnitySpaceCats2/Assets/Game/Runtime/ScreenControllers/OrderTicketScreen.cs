using UnityEngine;
using UnityEngine.UI;

public class OrderTicketScreen : ScreenController
{
    [SerializeField] private Text orderDetailsText;
    [SerializeField] private Button startMakingButton;

    [SerializeField] private GameObject panel; // root of orderticket
    
    [SerializeField] private OrderTicket orderTicket;
    
    [SerializeField] private OrderTicketData currentOrderData;

    void Start()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.CurrentState.ChangeEvent += OnGameStateChanged;
            
        }
        
        if (panel != null)
            panel.SetActive(false);
    }

    private void OnGameStateChanged(GameStateType newState)
    {
        if (newState == GameStateType.TakingOrder)
        {
            panel.SetActive(true);

            if (OrderManager.Instance != null)
            {
                OrderTicketData ticketData = OrderManager.Instance.GenerateRandomOrderData();
                currentOrderData = ticketData;
                int ticketNumber = OrderManager.Instance.GetAndIncrementNextOrderNumber();
                orderTicket.Setup(ticketNumber, ticketData);
                orderTicket.SetContentVisible(true);
            }
        }
        
        if (newState != GameStateType.Title || newState != GameStateType.Pause || newState != GameStateType.ChoosingCat || newState != GameStateType.ViewingCollectedCats || newState != GameStateType.WaitingforCustomers)
        {
            panel.SetActive(true);
            orderTicket.SetContentVisible(true);
            
        }
        
        else
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }

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
        
        // generated ticket
        OrderTicketData ticketData = default(OrderTicketData);
        if (OrderManager.Instance != null)
            ticketData = OrderManager.Instance.GetCurrentOrder();
        else 
            Debug.LogWarning("OrderTicketScreen: OrderManager.Instance is null");

        if (orderTicket != null)
        {
            int ticketNumber = 0;
            if (OrderManager.Instance != null)
            {
                ticketNumber = OrderManager.Instance.GetAndIncrementNextOrderNumber();
            }

            orderTicket.Setup(ticketNumber, ticketData);
            orderTicket.SetContentVisible(true);
        }

    }

    private void OnStartMaking()
    {
        Debug.Log("Starting to make drink!");
        GameStateManager.Instance.ChangeState(GameStateType.ChoosingTemperature);
    }
}