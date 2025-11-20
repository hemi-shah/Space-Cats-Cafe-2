using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;
using Game399.Shared.Enums;

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

    [Header("Drink References")]
    public Image hotDrinkSprite;
    public Image icedDrinkSprite;
    public Sprite hotMilkSprite;
    public Sprite[] icedMilkSprites; 

    private IDrink currentDrink;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    private void OnEnable()
    {
        if (currentDrink == null)
        {
            var drinkServices = ServiceResolver.Resolve<DrinkServices>();
            currentDrink = drinkServices.CurrentDrink;

            if (currentDrink != null)
                logger.Log("[MilkStation] Assigned currentDrink on enable.");
            else
                logger.LogError("[MilkStation] CurrentDrink is null! Did Temperature Station create it?");
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
        }

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
            logger.LogError("[MilkStation] Cannot pour milk. currentDrink is null.");
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

        logger.Log($"[MilkStation] Poured {milk}. Enum set to: {milkType}");

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
                logger.LogError($"[MilkStation] Unknown milk type: {milk}");
                yield break;
        }

        logger.Log($"[MilkStation] Pouring {milk}...");
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopSfx();
            AudioManager.Instance.PlayMilkSfx();
        }

        if (bottleImage != null) bottleImage.enabled = false;
        if (bottleButton != null) bottleButton.interactable = false;

        if (pourPrefab != null && pourSpawnPoint != null)
        {
            GameObject pourFx = Instantiate(pourPrefab, pourSpawnPoint.position, pourSpawnPoint.rotation);
            pourFx.transform.SetParent(pourSpawnPoint.parent, worldPositionStays: true);

            logger.Log($"[MilkStation] Spawned pour sprite for {milk}");

            yield return new WaitForSeconds(1f);

            Destroy(pourFx);
            logger.Log($"[MilkStation] Destroyed pour sprite for {milk}");
        }

        // Update the drink sprite after pouring
        UpdateDrinkSprite();

        if (bottleImage != null) bottleImage.enabled = true;
        if (bottleButton != null) bottleButton.interactable = true;

        logger.Log($"[MilkStation] {milk} bottle restored and button re-enabled");
    }

    private void UpdateDrinkSprite()
    {
        if (currentDrink == null)
        {
            logger.LogError("[MilkStation] Cannot update sprite - currentDrink is null");
            return;
        }

        if (currentDrink.Temp == Temperature.Hot)
        {
            if (hotDrinkSprite != null && hotMilkSprite != null)
            {
                hotDrinkSprite.sprite = hotMilkSprite;
                logger.Log("[MilkStation] Updated hot drink sprite to milk");
            }
            else
            {
                logger.LogError("[MilkStation] Hot drink sprite or hot milk sprite not assigned!");
            }
        }
        else if (currentDrink.Temp == Temperature.Iced)
        {
            if (icedDrinkSprite != null && icedMilkSprites != null && icedMilkSprites.Length > 0)
            {
                int iceLevel = Mathf.Clamp(currentDrink.IceLevel, 0, icedMilkSprites.Length - 1);
                Sprite milkSprite = icedMilkSprites[iceLevel];
                
                if (milkSprite != null)
                {
                    icedDrinkSprite.sprite = milkSprite;
                    logger.Log($"[MilkStation] Updated iced drink sprite to milk with ice level {iceLevel}");
                }
                else
                {
                    logger.LogError($"[MilkStation] No sprite assigned for ice level {iceLevel}");
                }
            }
            else
            {
                logger.LogError("[MilkStation] Iced drink sprite or iced milk sprites array not assigned!");
            }
        }
    }
}