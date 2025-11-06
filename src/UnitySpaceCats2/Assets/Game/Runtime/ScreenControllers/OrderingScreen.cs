using UnityEngine;
using UnityEngine.UI;

public class OrderingScreen : ScreenController
{
    [SerializeField] private Button takeOrderButton;
    [SerializeField] private Text orderDetailsText;

    [SerializeField] private Image catImage;

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

        CatDefinition cat = null;
        if (OrderManager.Instance != null)
        {
            cat = OrderManager.Instance.GetSelectedCat();
        }

        if (cat != null)
        {
            if (orderDetailsText != null)
            {
                orderDetailsText.text = "A cute cat approaches!\n\nReady to take their order?";
                Debug.Log("Order for cat: " + cat.catName + ".");
                
            }

            if (catImage != null)
            {
                catImage.sprite = cat.catSprite;
                catImage.enabled = true;
            }
                
        }
        else
        {
            Debug.LogWarning("OrderingScreen: no selected cat found in OrderManager!");
            if (orderDetailsText != null)
                orderDetailsText.text = "No cat selected";
            
        }
        
    }

    private void OnTakeOrder()
    {
        Debug.Log("Order taken from cat!");
        GameStateManager.Instance.ChangeState(GameStateType.OrderTicketReceived);
    }
}