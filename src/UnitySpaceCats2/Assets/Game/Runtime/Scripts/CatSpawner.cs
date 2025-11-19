using UnityEngine;
using System.Collections.Generic;
using Game.Runtime;
using Game399.Shared; // adjust if your GameStateType lives elsewhere

public class CatSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CatView catPrefab = null; // assign CatPrefab (the component on the prefab)
    [SerializeField] private RectTransform parentPanel = null; // panel to place cats under (usually this GameObject's RectTransform)

    [Header("Available Cat Definitions (5)")]
    [SerializeField] private List<CatDefinition> catDefinitions = new List<CatDefinition>(); // assign your 5 ScriptableObjects

    [Header("Spawn positions (anchored positions)")]
    [SerializeField] private Vector2[] anchoredPositions =
    {
        new Vector2(-235f, 198f),
        new Vector2(25f, 140f),
        new Vector2(425f, 67f)
    };

    private List<GameObject> spawnedCats = new List<GameObject>();
    
    private bool hasSpawnedInitialCats = false;
    
    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.CurrentState.ChangeEvent += OnStateChanged;
        }
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.CurrentState.ChangeEvent -= OnStateChanged;
        }
    }

    private void OnStateChanged(GameStateType newState)
    {
        if (newState == GameStateType.WaitingforCustomers)
        {
            if (!hasSpawnedInitialCats)
            {
                SpawnThreeCats();
                hasSpawnedInitialCats = true;
            }
            
        }
        else
        {
            // optional: clear when leaving the state
            //ClearSpawnedCats();
        }
    }

    public void RemoveCat(GameObject cat)
    {
        if (spawnedCats.Contains(cat))
        {
            spawnedCats.Remove(cat);
        }
    }

    private void SpawnThreeCats()
    {
        ClearSpawnedCats();

        if (catPrefab == null || parentPanel == null || catDefinitions.Count == 0)
        {
            logger.LogWarning("CatSpawner: missing references or definitions.");
            return;
        }

        // pick 3 unique random indices from catDefinitions
        List<int> available = new List<int>();
        for (int i = 0; i < catDefinitions.Count; i++) available.Add(i);

        for (int i = 0; i < anchoredPositions.Length; i++)
        {
            if (available.Count == 0) break;

            int pickIndex = UnityEngine.Random.Range(0, available.Count);
            int definitionIndex = available[pickIndex];
            available.RemoveAt(pickIndex);

            CatDefinition def = catDefinitions[definitionIndex];
            CatView instance = Instantiate(catPrefab, parentPanel);
            
            RectTransform rt = instance.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = anchoredPositions[i];
                rt.localScale = Vector3.one;
            }

            instance.Initialize(def);
            spawnedCats.Add(instance.gameObject);
        }
    }

    private void ClearSpawnedCats()
    {
        for (int i = spawnedCats.Count - 1; i >= 0; i--)
        {
            if (spawnedCats[i] != null) Destroy(spawnedCats[i]);
        }
        spawnedCats.Clear();
    }
}
