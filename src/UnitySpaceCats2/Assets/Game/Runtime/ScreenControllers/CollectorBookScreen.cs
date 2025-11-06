using UnityEngine;
using UnityEngine.UI;

public class CollectorBookScreen : ScreenController
{
    [SerializeField] private Text catListText;
    [SerializeField] private Button closeButton;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.ViewingCollectedCats;
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnClose);
            Debug.Log("CollectorBookScreen: Close button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        if (catListText != null)
        {
            catListText.text = "Cats Encountered:\n\n- Whiskers the Tabby\n- Luna the Black Cat\n- Mittens the Orange Cat\n\nMore cats coming soon!";
        }
    }

    private void OnClose()
    {
        Debug.Log("Closing collector book");
        GameStateManager.Instance.ChangeState(GameStateType.WaitingforCustomers);
    }
}