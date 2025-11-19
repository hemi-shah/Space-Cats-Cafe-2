using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;

public class MilkStation : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button wholeMilkButton;
    public Button almondMilkButton;
    public Button oatMilkButton;

    [Header("Bottle Images")]
    public Image wholeMilkImage;
    public Image almondMilkImage;
    public Image oatMilkImage;

    [Header("Spawn Point Over Cup")]
    public Transform pourSpawnPoint;
    
    [Header("Poured Prefabs")]
    public GameObject wholeMilkPouredPrefab;
    public GameObject almondMilkPouredPrefab;
    public GameObject oatMilkPouredPrefab;

    private IDrink currentDrink;

    private void OnEnable()
    {
        if (currentDrink == null)
        {
            var drinkServices = ServiceResolver.Resolve<DrinkServices>();
            currentDrink = drinkServices.CurrentDrink;

            if (currentDrink != null)
                Debug.Log("[MilkStation] Assigned currentDrink on enable.");
            else
                Debug.LogError("[MilkStation] CurrentDrink is null! Did Temperature Station create it?");
        }

        SetupButtons();
    }

    private void SetupButtons()
    {
        if (wholeMilkButton != null)
        {
            wholeMilkButton.onClick.RemoveAllListeners();
            wholeMilkButton.onClick.AddListener(() => PourMilk("Whole Milk"));
        }

        if (almondMilkButton != null)
        {
            almondMilkButton.onClick.RemoveAllListeners();
            almondMilkButton.onClick.AddListener(() => PourMilk("Almond Milk"));
        }   // ← FIXED: this bracket was missing!

        if (oatMilkButton != null)
        {
            oatMilkButton.onClick.RemoveAllListeners();
            oatMilkButton.onClick.AddListener(() => PourMilk("Oat Milk"));
        }
    } 

    public void PourMilk(string milk)
    {
        if (currentDrink == null)
        {
            Debug.LogError("[MilkStation] Cannot pour milk. currentDrink is null.");
            return;
        }

        // Convert string to enum
        MilkType milkType = milk switch
        {
            "Whole Milk" => MilkType.Dairy,
            "Almond Milk" => MilkType.Almond,
            "Oat Milk" => MilkType.Oat,
            _ => MilkType.None
        };

        currentDrink.AddMilk(milkType);

        Debug.Log($"[MilkStation] Poured {milk}. Enum set to: {milkType}");

        StartCoroutine(PourRoutine(milk));
    }

    private IEnumerator PourRoutine(string milk)
    {
        Image bottleImage = null;
        Button bottleButton = null;
        GameObject pourPrefab = null;

        switch (milk)
        {
            case "Whole Milk":
                bottleImage = wholeMilkImage;
                bottleButton = wholeMilkButton;
                pourPrefab = wholeMilkPouredPrefab;
                break;

            case "Almond Milk":
                bottleImage = almondMilkImage;
                bottleButton = almondMilkButton;
                pourPrefab = almondMilkPouredPrefab;
                break;

            case "Oat Milk":
                bottleImage = oatMilkImage;
                bottleButton = oatMilkButton;
                pourPrefab = oatMilkPouredPrefab;
                break;

            default:
                Debug.LogError($"[MilkStation] Unknown milk type: {milk}");
                yield break;
        }

        Debug.Log($"[MilkStation] Pouring {milk}...");

        if (bottleImage != null) bottleImage.enabled = false;
        if (bottleButton != null) bottleButton.interactable = false;

        if (pourPrefab != null && pourSpawnPoint != null)
        {
            GameObject pourFx = Instantiate(pourPrefab, pourSpawnPoint.position, pourSpawnPoint.rotation);
            pourFx.transform.SetParent(pourSpawnPoint.parent, worldPositionStays: true);

            Debug.Log($"[MilkStation] Spawned pour sprite for {milk}");

            yield return new WaitForSeconds(1f);

            Destroy(pourFx);
            Debug.Log($"[MilkStation] Destroyed pour sprite for {milk}");
        }

        if (bottleImage != null) bottleImage.enabled = true;
        if (bottleButton != null) bottleButton.interactable = true;

        Debug.Log($"[MilkStation] {milk} bottle restored and button re-enabled");
    }
}
