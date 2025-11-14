using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;

public class IceSpawner : MonoBehaviour
{
    public Button iceButton;
    public RectTransform spawnPoint;
    public GameObject iceCubePrefab;
    public SpriteRenderer drinkRenderer;
    
    private IDrink drink;
    private List<GameObject> activeIceCubes = new List<GameObject>();

    void Start()
    {
        drink = ServiceResolver.Resolve<DrinkServices>().CurrentDrink;
        iceButton.onClick.AddListener(SpawnCube);
    }

    void Update()
    {
        // Check each ice cube if it's hit the drink
        for (int i = activeIceCubes.Count - 1; i >= 0; i--)
        {
            if (activeIceCubes[i] == null)
            {
                activeIceCubes.RemoveAt(i);
                continue;
            }

            if (CheckIceHitDrink(activeIceCubes[i]))
            {
                Destroy(activeIceCubes[i]);
                activeIceCubes.RemoveAt(i);
                
                if (drink.IceLevel < 4)
                    drink.IceLevel++;
                    
                UpdateDrinkSprite();
            }
        }
    }

    bool CheckIceHitDrink(GameObject iceCube)
    {
        // Convert ice cube UI position to world position
        Camera cam = Camera.main;
        Vector3 iceScreenPos = iceCube.transform.position;
        Vector3 iceWorldPos = cam.ScreenToWorldPoint(iceScreenPos);
        
        // Get drink world position
        Vector3 drinkPos = drinkRenderer.transform.position;
        
        // Check distance (adjust the threshold as needed)
        float distance = Vector2.Distance(new Vector2(iceWorldPos.x, iceWorldPos.y), 
                                         new Vector2(drinkPos.x, drinkPos.y));
        
        return distance < 0.15f; // Adjust this threshold based on your drink size
    }

    void SpawnCube()
    {
        Debug.Log("Spawning UI cube!");

        Canvas canvas = spawnPoint.GetComponentInParent<Canvas>();
        GameObject cubeObj = Instantiate(iceCubePrefab, canvas.transform);
        
        RectTransform cubeRect = cubeObj.GetComponent<RectTransform>();
        cubeRect.position = spawnPoint.position;
        
        cubeObj.transform.SetAsLastSibling();
        
        // Add to tracking list
        activeIceCubes.Add(cubeObj);

        Debug.Log("UI Cube spawned at: " + spawnPoint.position);
    }

    void UpdateDrinkSprite()
    {
        string spriteName = drink.GetSpriteName();
        Sprite sprite = Resources.Load<Sprite>("DrinkSprites/IcedDrinkSprites/Empty/" + spriteName);
        drinkRenderer.sprite = sprite;
    }
}