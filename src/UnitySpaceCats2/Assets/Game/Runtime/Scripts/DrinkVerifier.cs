using Game.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkVerifier : ObserverMonoBehaviour
{

    public OrderManager OrderManager;
    
    
    protected override void Subscribe()
    {
        // subscribe to the drink thing that knows when the drink is done and add OnDrinkCompleted
        OrderManager.AddListener(OnDrinkCompleted);
    }

    protected override void Unsubscribe()
    {
        // nothing to see here
    }

    // TODO: we need to get the information about the drink and also about the order
    private void OnDrinkCompleted(OrderTicketData orderTicket, Drink drink)
    {
        // compare the drink and the order
        
        // call some method to provide a rating
    }
}
