using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;

public class SyrupStation : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button chocolateButton;
    public Button caramelButton;
    public Button mochaButton;

    [Header("Bottle Images")]
    public Image chocolateImage;
    public Image caramelImage;
    public Image mochaImage;

    [Header("Pump Sprites (Tilted)")]
    public GameObject chocolatePumpedPrefab;
    public GameObject caramelPumpedPrefab;
    public GameObject mochaPumpedPrefab;

    [Header("Spawn Point Over Cup")]
    public Transform pumpedSpawnPoint;

    private IDrink currentDrink;

    private void OnEnable()
    {
        if (currentDrink == null)
        {
            var drinkServices = ServiceResolver.Resolve<DrinkServices>();
            currentDrink = drinkServices.CurrentDrink;

            if (currentDrink != null)
                Debug.Log("[SyrupStation] Assigned currentDrink on enable.");
            else
                Debug.LogError("[SyrupStation] CurrentDrink is null! Did Temperature Station create it?");
        }

        SetupButtons();
    }

    private void SetupButtons()
    {
        if (chocolateButton != null)
            chocolateButton.onClick.AddListener(() => PumpSyrup("Chocolate"));

        if (caramelButton != null)
            caramelButton.onClick.AddListener(() => PumpSyrup("Caramel"));

        if (mochaButton != null)
            mochaButton.onClick.AddListener(() => PumpSyrup("Mocha"));
    }

    public void PumpSyrup(string syrup)
    {
        if (currentDrink == null)
        {
            Debug.LogError("[SyrupStation] Cannot pump syrup. currentDrink is null.");
            return;
        }

        currentDrink.AddSyrup(syrup);

        var countsLog = string.Join(", ", currentDrink.SyrupCounts.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        Debug.Log($"[SyrupStation] Pumped {syrup}. Syrup counts: {countsLog}");

        StartCoroutine(PumpRoutine(syrup));
    }

    private IEnumerator PumpRoutine(string syrup)
    {
        Image bottleImage = null;
        Button bottleButton = null;
        GameObject pumpedPrefab = null;

        switch (syrup)
        {
            case "Chocolate":
                bottleImage = chocolateImage;
                bottleButton = chocolateButton;
                pumpedPrefab = chocolatePumpedPrefab;
                break;

            case "Caramel":
                bottleImage = caramelImage;
                bottleButton = caramelButton;
                pumpedPrefab = caramelPumpedPrefab;
                break;

            case "Mocha":
                bottleImage = mochaImage;
                bottleButton = mochaButton;
                pumpedPrefab = mochaPumpedPrefab;
                break;

            default:
                Debug.LogError($"[SyrupStation] Unknown syrup: {syrup}");
                yield break;
        }

        Debug.Log($"[SyrupStation] Pumping {syrup}...");

        // Hide bottle + disable button
        if (bottleImage != null) bottleImage.enabled = false;
        if (bottleButton != null) bottleButton.interactable = false;

        // Spawn pumped sprite
        if (pumpedPrefab != null && pumpedSpawnPoint != null)
        {
            GameObject pumped = Instantiate(pumpedPrefab, pumpedSpawnPoint.position, pumpedSpawnPoint.rotation);
            pumped.transform.SetParent(pumpedSpawnPoint.parent, worldPositionStays: true);
            Debug.Log($"[SyrupStation] Spawned pumped sprite for {syrup}");

            yield return new WaitForSeconds(1f);

            Destroy(pumped);
            Debug.Log($"[SyrupStation] Destroyed pumped sprite for {syrup}");
        }

        // Restore bottle + button
        if (bottleImage != null) bottleImage.enabled = true;
        if (bottleButton != null) bottleButton.interactable = true;
        Debug.Log($"[SyrupStation] {syrup} bottle restored and button re-enabled");
    }
}
