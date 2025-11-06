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
            Debug.Log("ChooseCatScreen: Cat1 button ready");
        }
        
        if (cat2Button != null)
        {
            cat2Button.onClick.AddListener(() => OnCatSelected(2));
            Debug.Log("ChooseCatScreen: Cat2 button ready");
        }
    }

    private void OnCatSelected(int catNumber)
    {
        Debug.Log($"Cat {catNumber} selected!");
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }
}