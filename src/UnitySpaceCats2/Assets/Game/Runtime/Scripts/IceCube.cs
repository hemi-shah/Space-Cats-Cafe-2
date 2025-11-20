using UnityEngine;

public class IceCube : MonoBehaviour
{
    public System.Action<IceCube> OnHitDrink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drink"))
        {
            OnHitDrink?.Invoke(this);
        }
    }
}