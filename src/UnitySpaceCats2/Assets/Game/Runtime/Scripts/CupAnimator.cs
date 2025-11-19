using Game.Runtime;
using UnityEngine;

public class CupAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform hotCup;
    [SerializeField] private RectTransform coldCup;
    [SerializeField] private float slideDistance = 1000f;
    [SerializeField] private float slideDuration = 0.4f;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    public void SelectHot(Transform spawnPoint, GameObject drinkObject)
    {
        AnimateSelection(hotCup, coldCup, true, spawnPoint, drinkObject);
    }

    public void SelectCold(Transform spawnPoint, GameObject drinkObject)
    {
        AnimateSelection(coldCup, hotCup, false, spawnPoint, drinkObject);
    }

    private void AnimateSelection(RectTransform chosen, RectTransform other, bool isHot, Transform spawnPoint, GameObject drinkObject)
    {
        Vector3 otherTarget = other.position + new Vector3(isHot ? slideDistance : -slideDistance, 0, 0);
        other.position = otherTarget;

        chosen.gameObject.SetActive(false);
        other.gameObject.SetActive(false);

        if (drinkObject != null)
        {
            // Enable the existing drink object instead of instantiating
            drinkObject.SetActive(true);
            drinkObject.transform.position = spawnPoint.position;
            drinkObject.transform.localScale = Vector3.one;
            
            logger.Log($"CupAnimator: Enabled drink at {spawnPoint.position}");
        }
        else
        {
            logger.LogError("CupAnimator: drinkObject is null!");
        }
    }
}