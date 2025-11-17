using UnityEngine;

public class SyrupStation : MonoBehaviour
{
    [SerializeField] private Transform syrupSpawnPoint;
    [SerializeField] private GameObject syrupEffectPrefab;

    [SerializeField] private Drink currentDrink;

    public void PumpSyrup(string syrupType)
    {
        // spawn visual
        if (syrupEffectPrefab != null && syrupSpawnPoint != null)
            Instantiate(syrupEffectPrefab, syrupSpawnPoint.position, Quaternion.identity);
        
        // update in drink object
        if (currentDrink != null)
            currentDrink.AddSyrup(syrupType);
    }
}