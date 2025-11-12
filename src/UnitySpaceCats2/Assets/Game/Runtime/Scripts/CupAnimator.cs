using System.Collections;
using UnityEngine;
using Game.Runtime;

public class CupAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform hotCup;
    [SerializeField] private RectTransform coldCup;
    [SerializeField] private float slideDuration = 0.4f;
    [SerializeField] private GameObject hotDrinkPrefab;
    [SerializeField] private GameObject coldDrinkPrefab;

    public void SelectHot(Transform spawnPoint)
    {
        StartCoroutine(AnimateSelection(hotCup, coldCup, true, spawnPoint));
    }

    public void SelectCold(Transform spawnPoint)
    {
        StartCoroutine(AnimateSelection(coldCup, hotCup, false, spawnPoint));
    }

    private IEnumerator AnimateSelection(RectTransform chosen, RectTransform other, bool isHot, Transform spawnPoint)
    {
        Vector2 chosenStart = chosen.anchoredPosition;
        Vector2 otherStart = other.anchoredPosition;

        // Only move X to 0, keep Y the same
        Vector2 chosenEnd = new Vector2(0, chosenStart.y);

        // Slide other cup off-screen along X only
        Vector2 otherEnd = otherStart + new Vector2(isHot ? 1000 : -1000, 0);

        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;
            t = t * t * (3f - 2f * t); // smoothstep ease

            chosen.anchoredPosition = Vector2.Lerp(chosenStart, chosenEnd, t);
            other.anchoredPosition = Vector2.Lerp(otherStart, otherEnd, t);
            yield return null;
        }

        // Snap to final positions
        chosen.anchoredPosition = chosenEnd;
        other.anchoredPosition = otherEnd;

        other.gameObject.SetActive(false);

        // Spawn drink object at passed spawn point
        SpawnDrinkVisual(spawnPoint);

        chosen.gameObject.SetActive(false);

    }

    private void SpawnDrinkVisual(Transform spawnPoint)
    {
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        var drink = drinkServices.CurrentDrink;

        if (drink == null)
        {
            Debug.LogError("No drink created before animation!");
            return;
        }

        GameObject prefabToSpawn = drink.Temp == Temperature.Hot ? hotDrinkPrefab : coldDrinkPrefab;

        Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
    }
}