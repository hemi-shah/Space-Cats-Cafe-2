using UnityEngine;

/// <summary>
/// Defines all possible game states/screens in the coffee sim
/// </summary>
public enum GameStateType
{
    Title,
    Pause,           
    ChoosingCat,  
    ViewingCollectedCats,          
    WaitingforCustomers,         
    TakingOrder,      
    ChoosingTemperature,           
    PlayingIceGame,         
    PumpingSyrup,
    PouringEspresso,
    ChoosingMilk,
    PlacingToppings,
    ServingDrinks,
    OrderTicketReceived,
    OrderCompleted
}