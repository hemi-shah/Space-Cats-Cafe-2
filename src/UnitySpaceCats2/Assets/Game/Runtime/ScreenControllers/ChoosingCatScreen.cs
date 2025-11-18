using UnityEngine;
using UnityEngine.UI;

public class ChooseCatScreen : ScreenController
{
    [SerializeField] private Button cat1Button;
    [SerializeField] private Button cat2Button;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ChoosingCat;
        
        if (cat1Button != null)
        {
            cat1Button.onClick.AddListener(() => OnCatSelected(1));
            logger.Log("ChooseCatScreen: Cat1 button ready");
        }
        
        if (cat2Button != null)
        {
            cat2Button.onClick.AddListener(() => OnCatSelected(2));
            logger.Log("ChooseCatScreen: Cat2 button ready");
        }
    }

    private void OnCatSelected(int catNumber)
    {
        logger.Log($"Cat {catNumber} selected!");
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }
}