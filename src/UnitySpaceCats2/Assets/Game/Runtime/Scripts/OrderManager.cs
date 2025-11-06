using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    private CatDefinition selectedCat;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedCat(CatDefinition cat)
    {
        selectedCat = cat;
    }

    public CatDefinition GetSelectedCat()
    {
        return selectedCat;
    }

    public void ClearSelectedCat()
    {
        selectedCat = null;
    }
}
