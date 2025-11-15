using UnityEngine;

public class CupAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform hotCup;
    [SerializeField] private RectTransform coldCup;
    [SerializeField] private float slideDistance = 1000f;
    [SerializeField] private float slideDuration = 0.4f;

    public GameObject SelectHot(Transform spawnPoint, GameObject prefabToSpawn)
    {
        return AnimateSelection(hotCup, coldCup, true, spawnPoint, prefabToSpawn);
    }

    public GameObject SelectCold(Transform spawnPoint, GameObject prefabToSpawn)
    {
        return AnimateSelection(coldCup, hotCup, false, spawnPoint, prefabToSpawn);
    }

    private GameObject AnimateSelection(RectTransform chosen, RectTransform other, bool isHot, Transform spawnPoint, GameObject prefabToSpawn)
    {
        Vector3 otherTarget = other.position + new Vector3(isHot ? slideDistance : -slideDistance, 0, 0);
        other.position = otherTarget;

        chosen.gameObject.SetActive(false);
        other.gameObject.SetActive(false);

        if (prefabToSpawn != null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("CupAnimator: No Canvas found!");
                return null;
            }

            GameObject spawnedDrink = Instantiate(prefabToSpawn, canvas.transform);
            spawnedDrink.transform.position = spawnPoint.position;
            spawnedDrink.transform.localScale = Vector3.one;
            
            return spawnedDrink;
        }
        else
        {
            Debug.LogError("CupAnimator: prefabToSpawn is null!");
            return null;
        }
    }
}