using NUnit.Framework;
using UnityEngine;

public class OrderManagerTests
{
    [Test]
    public void SetSelectedCat_StoresCat()
    {
        var managerGO = new GameObject();
        var manager = managerGO.AddComponent<OrderManager>();

        var cat = ScriptableObject.CreateInstance<CatDefinition>();
        manager.SetSelectedCat(cat);

        Assert.AreEqual(cat, manager.GetSelectedCat());
    }

    [Test]
    public void GetAndIncrementNextOrderNumber_Increments()
    {
        var managerGO = new GameObject();
        var manager = managerGO.AddComponent<OrderManager>();

        int first = manager.GetAndIncrementNextOrderNumber();
        int second = manager.GetAndIncrementNextOrderNumber();

        Assert.AreEqual(1, first);
        Assert.AreEqual(2, second);
    }

    [Test]
    public void ClearSelectedCat_ResetsToNull()
    {
        var managerGO = new GameObject();
        var manager = managerGO.AddComponent<OrderManager>();

        var cat = ScriptableObject.CreateInstance<CatDefinition>();
        manager.SetSelectedCat(cat);
        manager.ClearSelectedCat();

        Assert.IsNull(manager.GetSelectedCat());
    }
}