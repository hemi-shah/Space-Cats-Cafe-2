using UnityEngine;
using UnityEngine.UI;

public class EspressoStationScreen : ScreenController
{
    [SerializeField] private Button pourButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Text statusText;

    private bool espressoPoured = false;

    protected override void SetupButtons()
    {
        associatedState = GameStateType.PouringEspresso;
        
        if (pourButton != null)
        {
            pourButton.onClick.AddListener(OnPourEspresso);
            logger.Log("EspressoStation: Pour button ready");
        }
        
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinue);
            logger.Log("EspressoStation: Continue button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        espressoPoured = false;
        if (statusText != null)
        {
            statusText.text = "Pour espresso shots";
        }
    }

    private void OnPourEspresso()
    {
        espressoPoured = true;
        logger.Log("Espresso poured!");
        if (statusText != null)
        {
            statusText.text = "Espresso ready!";
        }
    }

    private void OnContinue()
    {
        logger.Log("Moving to toppings");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PouringEspresso);
        GameStateManager.Instance.ChangeState(GameStateType.PlacingToppings);
    }
}