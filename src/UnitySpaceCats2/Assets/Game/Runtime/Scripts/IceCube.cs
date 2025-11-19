using UnityEngine;

public class IceCube : MonoBehaviour
{
    public System.Action<IceCube> OnHitDrink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drink"))
        {
            Debug.Log("Ice hit the drink!");
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayIceSfx();
            }
        }
    }
}