using UnityEngine;
using UnityEngine.UI;

public class CatView : MonoBehaviour
{
    [SerializeField] private Image catImage;
    [SerializeField] private Text catNameText; 
    [SerializeField] private Button takeOrderButton;
    
    private CatDefinition catDefinition;
    
    public void Initialize(CatDefinition definiiton)
    {
        catDefinition = definiiton;

        if (catImage != null)
            catImage.sprite = catDefinition.catSprite;
        
        if (catNameText != null)
            catNameText.text = catDefinition.catName;

        if (takeOrderButton != null)
        {
            takeOrderButton.onClick.RemoveAllListeners();
            takeOrderButton.onClick.AddListener(OnTakeOrderClicked);
        }
    }

    private void OnTakeOrderClicked()
    {
        if (catDefinition == null)
        {
            Debug.LogWarning("CatView.OnTakeOrderClicked: CatDefinition is null");
            return;
        }
        
        if (OrderManager.Instance != null)
            OrderManager.Instance.SetSelectedCat(catDefinition);
        
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.ChangeState(GameStateType.TakingOrder);
    }

    private void OnDestroy()
    {
        if (takeOrderButton != null)
        {
            takeOrderButton.onClick.RemoveListener(OnTakeOrderClicked);
        }
    }
}