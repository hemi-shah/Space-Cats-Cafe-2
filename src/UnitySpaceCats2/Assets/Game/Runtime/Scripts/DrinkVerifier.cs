using Game.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class DrinkVerifier : ObserverMonoBehaviour
{
    public OrderManager OrderManager;
    public int lastRating { get; private set; }

    protected override void Subscribe()
    {
        OrderManager.AddListener(OnDrinkCompleted);
    }

    protected override void Unsubscribe()
    {
        // no-op
    }

    private void OnDrinkCompleted(OrderTicketData orderTicket, Drink drink)
    {
        if (orderTicket.Equals(default(OrderTicketData)) || drink == null)
        {
            logger.LogWarning("[DrinkVerifier] Missing order or drink");
            lastRating = 1;
            return;
        }

        int points = 0;

        // Temperature
        bool drinkIsHot = (drink.Temp == Temperature.Hot);
        if (drinkIsHot == orderTicket.isHot)
        {
            points++;
            logger.Log("[DrinkVerifier] Correct temperature");
        }

        // Ice
        if (!orderTicket.isHot)
        {
            if (drink.IceLevel == orderTicket.numberOfIceCubes)
            {
                points++;
                logger.Log("[DrinkVerifier] Correct ice count");
            }
        }
        else if (drink.IceLevel == 0)
        {
            points++;
            logger.Log("[DrinkVerifier] Correct ice for hot drink");
        }

        // Syrup
        if (drink.SyrupCounts != null)
        {
            foreach (var kvp in drink.SyrupCounts)
            {
                logger.Log($"[DrinkVerifier] Syrup {kvp.Key}: {kvp.Value} pumps");
            }
        }

        if (orderTicket.syrup != SyrupType.None)
        {
            string syrupName = orderTicket.syrup.ToString();
            if (drink.SyrupCounts != null &&
                drink.SyrupCounts.TryGetValue(syrupName, out int count) &&
                count > 0)
            {
                points++;
                logger.Log($"[DrinkVerifier] Correct syrup: {syrupName}, pumps: {count}");
            }
        }
        else
        {
            // no syrup
            bool anySyrup = drink.SyrupCounts != null && drink.SyrupCounts.Count > 0;
            if (!anySyrup)
            {
                points++;
                logger.Log("[DrinkVerifier] Correct syrup (none)");
            }
        }

        // Milk
        if (drink.Milk == orderTicket.milk)
        {
            points++;
            logger.Log("[DrinkVerifier] Correct milk");
        }

        // Toppings
        points += CheckTopping(drink, orderTicket.hasWhippedCream, "WhippedCream", "[DrinkVerifier] Correct whipped cream");
        points += CheckTopping(drink, orderTicket.hasChocolateSyrup, "ChocolateDrizzle", "[DrinkVerifier] Correct chocolate drizzle");
        points += CheckTopping(drink, orderTicket.hasCaramelSyrup, "CaramelDrizzle", "[DrinkVerifier] Correct caramel drizzle");

        // Assign rating based on points (0-7)
        lastRating = points switch
        {
            0 or 1 => 1,
            2 or 3 => 2,
            4 or 5 => 3,
            6 => 4,
            7 => 5,
            _ => throw new InvalidOperationException($"Invalid points: {points}")
        };

        logger.Log($"[DrinkVerifier] Total points: {points}, Rating: {lastRating}");
    }

    private int CheckTopping(Drink drink, bool orderHas, string toppingName, string logMessage)
    {
        bool drinkHas = drink.Toppings.Exists(t => string.Equals(t, toppingName, StringComparison.OrdinalIgnoreCase));
        if (drinkHas == orderHas)
        {
            logger.Log(logMessage);
            return 1;
        }
        return 0;
    }
}
