using Game.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkVerifier : ObserverMonoBehaviour
{

    public OrderManager OrderManager;

    public int lastRating { get; private set; }

    // Delete Awake later!! only here for testing to change the rating
    void Awake()
    {
        lastRating = 5;
    }
    
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
        // changes lastRating based on accuracy of drink
        
        
        
        // 7 total points
        // optional: 0 points: remake drink (?)
        // 0 - 1 points: 1 star
        // 2 - 3 points: 2 stars
        // 4 - 5 points: 3 stars
        // 6 points: 4 stars
        // 7 points: 5 stars

        int points;
        
        // if drink isHot matches orderTicket isHot, +1
        // if iceCubes is correct, +1
        // if syrupType is correct, +1
        // if milkType is correct, +1
        // if whipped cream is correct, +1
        // if chocolate drizzle is correct, +1
        // if caramel drizzle is correct, +1
        
        points = 7; // placeholder point value

        // set rating
        if (points == 0 || points == 1)
        {
            lastRating = 1;
        }
        else if (points == 2 || points == 3)
        {
            lastRating = 2;
        }
        else if (points == 4 || points == 5)
        {
            lastRating = 3;
        }
        else if (points == 6)
        {
            lastRating = 4;
        }
        else if (points == 7)
        {
            lastRating = 5;
        }
        else
        {
            Debug.LogError("points not 0-7");
        }

    }
    
    
}
