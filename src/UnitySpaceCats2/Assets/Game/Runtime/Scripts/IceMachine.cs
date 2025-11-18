using UnityEngine;

public class IceMachine : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject iceCubePrefab;

    public void DispenseIce()
    {
        Instantiate(iceCubePrefab, spawnPoint.position, Quaternion.identity);
    }
}