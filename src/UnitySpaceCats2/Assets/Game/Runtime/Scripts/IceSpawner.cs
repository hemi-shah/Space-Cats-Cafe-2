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
    public IDrink drink; // current drink instance
    public Image drinkSprite; // assign the Image on the drink prefab
    public SpriteRenderer drinkRenderer; // keeps existing renderer reference if needed

    [Header("Ice Sprites")]
    public Sprite[] iceLevelSprites; // assign 0 = empty, 1..4 = ice

    private List<GameObject> activeIceCubes = new List<GameObject>();
    private bool drinkInitialized = false;

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

            Debug.Log($"✓ IceSpawner: Drink initialized! IceLevel={drink.IceLevel}");
            Debug.Log($"✓ DrinkSprite assigned: {(drinkSprite != null ? drinkSprite.name : "NULL")}");
        }
    }

    void UpdateDrinkImage()
    {
        if (drinkSprite == null)
        {
            Debug.LogError("Cannot update drink image — drinkSprite is null!");
            return;
        }

        int level = Mathf.Clamp(drink.IceLevel, 0, iceLevelSprites.Length - 1);
        Sprite newSprite = iceLevelSprites[level];

        if (newSprite != null)
        {
            drinkSprite.sprite = newSprite;
            Debug.Log($"✓ Updated drink image to ice level {level}");
        }
        else
        {
            Debug.LogError($"No sprite assigned for ice level {level}");
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
