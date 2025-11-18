using UnityEngine;
using UnityEngine.UI;
using Game.Runtime;

public class CatView : MonoBehaviour
{
    [SerializeField] private Image catImage;
    [SerializeField] private Text catNameText;
    [SerializeField] private Button takeOrderButton;

    private CatDefinition catDefinition;

    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }

    public void Initialize(CatDefinition definition)
    {
        catDefinition = definition;

        if (catImage != null)
        {
            catImage.sprite = definition.catSprite;
        }

        if (catNameText != null)
        {
            catNameText.text = definition.catName;
        }

        if (takeOrderButton != null)
        {
            takeOrderButton.onClick.RemoveAllListeners();
            takeOrderButton.onClick.AddListener(OnTakeOrderClicked);
        }
    }

    private void OnTakeOrderClicked()
    {
        logger.Log($"Taking order from: {catDefinition.catName}");
        
        if (OrderManager.Instance != null)
        {
            OrderManager.Instance.SetSelectedCat(catDefinition);

            OrderManager.Instance.GenerateRandomOrderData();
        }
        
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ChangeState(GameStateType.ChoosingTemperature);
        }
    }

    // New method to get the cat definition
    public CatDefinition GetCatDefinition()
    {
        return catDefinition;
    }
}