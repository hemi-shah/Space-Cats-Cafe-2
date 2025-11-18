using Game.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkVerifier : ObserverMonoBehaviour
{

    public OrderManager OrderManager;

    public int lastRating { get; private set; }

    // Delete Awake later!! only here for testing to change the rating
    /*
    void Awake()
    {
        lastRating = 5;
    }
    */
    
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
        if (orderTicket.Equals(default(OrderTicketData)) || drink == null)
        {
            logger.LogWarning("Missing order or drink");
            lastRating = 1;
            return;
        }

        int points = 0;

        // temperature
        bool drinkIsHot = (drink.Temp == Temperature.Hot);
        if (drinkIsHot == orderTicket.isHot)
            points++;

        // ice
        if (!orderTicket.isHot)
        {
            if (drink.IceLevel == orderTicket.numberOfIceCubes)
            {
                points++;
                logger.Log("Correct temperature");
            }
        }
        else
        {
            if (drink.IceLevel == 0)
            {
                points++;
                logger.Log("Correct ice");
            }
        }
        
        // syrup
        if (orderTicket.syrup != SyrupType.None)
        {
            // syrup is not a type? look back at this later
            string syrupName = orderTicket.syrup.ToString();
            if (drink.Syrups != null &&
                drink.Syrups.Exists(s => string.Equals(s, syrupName, System.StringComparison.OrdinalIgnoreCase)))
            {
                points++;
                logger.Log("Correct syrup");
            }
                
        }
        else
        {
            // no syrup
            if (drink.Syrups == null || drink.Syrups.Count == 0)
            {
                points++;
                logger.Log("Correct syrup (none)");
            }
        }

        // milk
        if (orderTicket.milk == MilkType.None)
        {
            if (drink.Milk == MilkType.None)
            {
                points++;
                logger.Log("Correct milk (none)");
            }
        }
        else
        {
            if (drink.Milk == orderTicket.milk)
            {
                points++;
                logger.Log("Correct milk");
            }
        }
        
        // whipped cream
        bool drinkHasWhipped = false;
        foreach (string topping in drink.Toppings)
        {
            if (string.Equals(topping, "WhippedCream", System.StringComparison.OrdinalIgnoreCase))
            {
                drinkHasWhipped = true;
                break;
            }
        }
        if (drinkHasWhipped == orderTicket.hasWhippedCream)
        {
            points++;
            logger.Log("Correct whipped cream");
        }

// chocolate drizzle
        bool drinkHasChoco = false;
        foreach (string topping in drink.Toppings)
        {
            if (string.Equals(topping, "ChocolateDrizzle", System.StringComparison.OrdinalIgnoreCase))
            {
                drinkHasChoco = true;
                break;
            }
        }
        if (drinkHasChoco == orderTicket.hasChocolateSyrup)
        {
            points++;
            logger.Log("Correct chocolate drizzle");
        }

// caramel drizzle
        bool drinkHasCaramel = false;
        foreach (string topping in drink.Toppings)
        {
            if (string.Equals(topping, "CaramelDrizzle", System.StringComparison.OrdinalIgnoreCase))
            {
                drinkHasCaramel = true;
                break;
            }
        }
        if (drinkHasCaramel == orderTicket.hasCaramelSyrup)
        {
            points++;
            logger.Log("Correct caramel drizzle");
        }
        
        
        // call some method to provide a rating
        // changes lastRating based on accuracy of drink
        
        
        
        // 7 total points
        // optional: 0 points: remake drink (?)
        // 0 - 1 points: 1 star
        // 2 - 3 points: 2 stars
        // 4 - 5 points: 3 stars
        // 6 points: 4 stars
        // 7 points: 5 stars
        
        
        // if drink isHot matches orderTicket isHot, +1
        // if iceCubes is correct, +1
        // if syrupType is correct, +1
        // if milkType is correct, +1
        // if whipped cream is correct, +1
        // if chocolate drizzle is correct, +1
        // if caramel drizzle is correct, +1
        
        //points = 7; // placeholder point value

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
            logger.LogError("points not 0-7");
        }
        
        logger.Log("Points: " + points);

    }
    
    
}
