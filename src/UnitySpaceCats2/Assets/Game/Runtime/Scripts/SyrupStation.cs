using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SyrupStation : MonoBehaviour
{
    [SerializeField] private Drink currentDrink;

    [Header("Bottle Buttons (UI)")]
    public Button chocolateButton;
    public Button caramelButton;
    public Button mochaButton;

    [Header("Bottle Images (UI)")]
    public Image chocolateImage;
    public Image caramelImage;
    public Image mochaImage;

    [Header("Pump Sprites (Tilted Prefabs)")]
    public GameObject chocolatePumpedPrefab;
    public GameObject caramelPumpedPrefab;
    public GameObject mochaPumpedPrefab;

    [Header("Spawn Point Over Cup")]
    public Transform pumpedSpawnPoint;

    private void Start()
    {
        chocolateButton.onClick.AddListener(() => PumpSyrup("Chocolate"));
        caramelButton.onClick.AddListener(() => PumpSyrup("Caramel"));
        mochaButton.onClick.AddListener(() => PumpSyrup("Mocha"));
    }

    public void PumpSyrup(string syrup)
    {
        currentDrink?.AddSyrup(syrup);
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
        }

        // Hide bottle and button
        bottleImage.enabled = false;
        bottleButton.interactable = false;

        // Spawn the pumped sprite
        GameObject pumped = Instantiate(
            pumpedPrefab, 
            pumpedSpawnPoint.position, 
            pumpedSpawnPoint.rotation
        );
        pumped.transform.SetParent(pumpedSpawnPoint.parent, worldPositionStays: true);

        // Wait 1 second
        yield return new WaitForSeconds(1f);

        Destroy(pumped);

        // Restore bottle and button
        bottleImage.enabled = true;
        bottleButton.interactable = true;
    }
}