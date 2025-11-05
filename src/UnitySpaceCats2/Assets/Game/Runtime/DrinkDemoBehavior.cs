using Game.Runtime;
using Game399.Shared;
using Game399.Shared.Services;
using UnityEngine;

public class DrinkDemoBehavior : ObserverMonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var drinkService = ServiceResolver.Resolve<IDrinkService>();
        drinkService.CanMakeDrink();
    }

    protected override void Subscribe()
    {
        ServiceResolver.Resolve<DrinkState>().DrinkCount.ChangeEvent += DrinkCountOnChangeEvent;
    }

    private void DrinkCountOnChangeEvent(int obj)
    {
        // DO STUFF!
    }

    protected override void Unsubscribe()
    {
        ServiceResolver.Resolve<DrinkState>().DrinkCount.ChangeEvent -= DrinkCountOnChangeEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
