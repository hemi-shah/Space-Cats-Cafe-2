using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [Header("Random Order Weights")] 
    [Range(0f, 1f)] [SerializeField] private float hotChance = 0.5f;
    [Range(0f, 1f)] [SerializeField] private float syrupChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float milkChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float whippedCreamChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float drizzleChance = 0.35f;

    private OrderGenerator generator;
    private CatDefinition selectedCat;
    private OrderTicketData currentOrder;
    
    private int nextOrderNumber = 1;
    
    private List<Action<OrderTicketData, Drink>> listeners = new();
    
    // list of stuff to notify
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        generator = new OrderGenerator(new RandomProvider());

        generator.hotChance = hotChance;
        generator.syrupChance = syrupChance;
        generator.milkChance = milkChance;
        generator.whippedCreamChance = whippedCreamChance;
        generator.drizzleChance = drizzleChance;
    }

    public void SetSelectedCat(CatDefinition cat)
        => selectedCat = cat;

    public CatDefinition GetSelectedCat()
        => selectedCat;

    public OrderTicketData GenerateRandomOrderData()
    {
        currentOrder = generator.Generate();
        return currentOrder;
    }

    public OrderTicketData GetCurrentOrder()
        => currentOrder;

    public int GetAndIncrementNextOrderNumber()
    {
        int result = nextOrderNumber;
        nextOrderNumber++;
        return result;
    }

    public void ClearSelectedCat()
        => selectedCat = null;

    // TODO: Call this when they click the drink is done
    public void CompleteOrder(Drink finishedDrink)
    {
        CheckAndFire(finishedDrink);
    }

    public void AddListener(Action<OrderTicketData, Drink> listener)
    {
        if (listener != null && !listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void RemoveListener(Action<OrderTicketData, Drink> listener)
    {
        listeners.Remove(listener);
    }
    
    void CheckAndFire(Drink finishedDrink)
    {
        foreach (Action<OrderTicketData, Drink> listener in listeners)
        {
            try
            {
                listener?.Invoke(currentOrder, finishedDrink);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Listener failed: {ex}");
            }
        }
    }
}