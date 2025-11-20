using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;
using System.Collections.Generic;

public class IceSpawner : MonoBehaviour
{
    public Button iceButton;
    public RectTransform spawnPoint;
    public GameObject iceCubePrefab;

    [Header("Drink References")]
    public IDrink drink;
    public Image drinkSprite; 
    public SpriteRenderer drinkRenderer; 

    [Header("Ice Sprites")]
    public Sprite[] iceLevelSprites; 

    private List<GameObject> activeIceCubes = new List<GameObject>();
    private bool drinkInitialized = false;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    void Start()
    {
        iceButton.onClick.AddListener(SpawnCube);
    }

    void Update()
    {
        if (!drinkInitialized)
            TryInitializeDrink();

        if (drink == null || drinkSprite == null) return;

        for (int i = activeIceCubes.Count - 1; i >= 0; i--)
        {
            GameObject cube = activeIceCubes[i];
            if (cube == null)
            {
                activeIceCubes.RemoveAt(i);
                continue;
            }

            if (CheckIceHitDrink(cube))
            {
                Destroy(cube);
                activeIceCubes.RemoveAt(i);

                if (drink.IceLevel < 4)
                {
                    drink.IceLevel++;
                    logger.Log($"Ice level: {drink.IceLevel}");
                    UpdateDrinkImage();
                }
            }
        }
    }

    void TryInitializeDrink()
    {
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        if (drinkServices != null && drinkServices.CurrentDrink != null)
        {
            drink = drinkServices.CurrentDrink;
            drinkInitialized = true;
            logger.Log($"Ice level: {drink.IceLevel}");
        }
    }

    void UpdateDrinkImage()
    {
        AudioManager.Instance.PlayIceSfx();
        
        if (drinkSprite == null)
        {
            logger.LogError("Cannot update drink image: drinkSprite is null!");
            return;
        }

        int level = Mathf.Clamp(drink.IceLevel, 0, iceLevelSprites.Length - 1);
        Sprite newSprite = iceLevelSprites[level];

        if (newSprite != null)
        {
            drinkSprite.sprite = newSprite;
        }
        else
        {
            logger.LogError($"No sprite assigned for ice level {level}");
        }
    }

    bool CheckIceHitDrink(GameObject iceCube)
    {
        if (drinkRenderer == null) return false;

        Camera cam = Camera.main;
        Vector3 iceWorldPos = cam.ScreenToWorldPoint(iceCube.transform.position);
        iceWorldPos.z = 0f;

        Vector3 drinkPos = drinkRenderer.transform.position;

        float distance = Vector2.Distance(new Vector2(iceWorldPos.x, iceWorldPos.y),
                                         new Vector2(drinkPos.x, drinkPos.y));

        return distance < 0.5f;
    }

    void SpawnCube()
    {
        Canvas canvas = spawnPoint.GetComponentInParent<Canvas>();
        GameObject cubeObj = Instantiate(iceCubePrefab, canvas.transform);

        RectTransform cubeRect = cubeObj.GetComponent<RectTransform>();
        cubeRect.position = spawnPoint.position;

        cubeObj.transform.SetAsLastSibling();
        activeIceCubes.Add(cubeObj);
    }
}