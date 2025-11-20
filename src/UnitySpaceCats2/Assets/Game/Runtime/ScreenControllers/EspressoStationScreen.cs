using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game.Runtime;
using Game399.Shared.Enums;

public class EspressoStationScreen : ScreenController
{
    [Header("UI Buttons")]
    [SerializeField] private Button pourButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Text statusText;

    [Header("Drink References")]
    [SerializeField] private GameObject hotDrinkObject;
    [SerializeField] private GameObject coldDrinkObject;

    [Header("Espresso Sprites")]
    [SerializeField] private Sprite hotEspressoSprite;
    [SerializeField] private Sprite[] coldEspressoIceSprites; // Array of sprites for different ice levels with espresso

    [Header("Animation Settings")] 
    [SerializeField] private float pourDuration = 3f;

    private bool espressoPoured = false;
    private IDrink currentDrink;
    private GameObject activeDrinkObject;

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
            continueButton.interactable = false; // Disabled until espresso is poured
            logger.Log("EspressoStation: Continue button ready");
        }
    }

    protected override void OnScreenShow()
    {
        base.OnScreenShow();
        
        espressoPoured = false;
        
        // Get the current drink from DrinkServices
        var drinkServices = ServiceResolver.Resolve<DrinkServices>();
        currentDrink = drinkServices.CurrentDrink;
        
        if (currentDrink == null)
        {
            logger.LogError("EspressoStation: No current drink found!");
            return;
        }

        // Determine which drink object to use based on temperature
        if (currentDrink.Temp == Temperature.Hot)
        {
            activeDrinkObject = hotDrinkObject;
            if (coldDrinkObject != null) coldDrinkObject.SetActive(false);
        }
        else
        {
            activeDrinkObject = coldDrinkObject;
            if (hotDrinkObject != null) hotDrinkObject.SetActive(false);
        }

        // Make sure the active drink object is visible
        if (activeDrinkObject != null)
        {
            activeDrinkObject.SetActive(true);
            logger.Log($"EspressoStation: Active drink object set to {currentDrink.Temp}");
        }
        
        // Reset UI
        if (statusText != null)
        {
            statusText.text = "Pour espresso shots";
        }
        
        if (pourButton != null)
        {
            pourButton.interactable = true;
        }
        
        if (continueButton != null)
        {
            continueButton.interactable = false;
        }
    }

    private void OnPourEspresso()
    {
        if (espressoPoured)
        {
            logger.Log("EspressoStation: Espresso already poured");
            return;
        }

        logger.Log("EspressoStation: Starting espresso pour");
        
        // Disable pour button to prevent multiple clicks
        if (pourButton != null)
        {
            pourButton.interactable = false;
        }

        // Start the pouring animation
        StartCoroutine(PourEspressoRoutine());
    }

    private IEnumerator PourEspressoRoutine()
    {
        // Update status text
        if (statusText != null)
        {
            statusText.text = "Pouring espresso...";
        }

        // Wait for initial delay
        yield return new WaitForSeconds(1f);
        
        // Update the drink data - set coffee type
        if (currentDrink != null)
        {
            currentDrink.Coffee = CoffeeType.Black; // Espresso is black coffee
            logger.Log("EspressoStation: Set coffee type to Black");
        }

        // Update the drink sprite based on temperature and ice level
        UpdateDrinkSpriteWithEspresso();

        // Wait for pouring animation duration
        yield return new WaitForSeconds(pourDuration);
        
        // Mark as complete
        espressoPoured = true;
        
        // Update UI
        if (statusText != null)
        {
            statusText.text = "Espresso ready!";
        }

        // Enable continue button
        if (continueButton != null)
        {
            continueButton.interactable = true;
        }

        logger.Log("EspressoStation: Espresso pour complete");
    }

    private void UpdateDrinkSpriteWithEspresso()
    {
        if (activeDrinkObject == null)
        {
            logger.LogError("EspressoStation: Active drink object is null!");
            return;
        }

        // Try to get the Image component from the active drink object
        Image drinkImage = activeDrinkObject.GetComponent<Image>();
        
        // If not found on the main object, try to find it in children
        if (drinkImage == null)
        {
            drinkImage = activeDrinkObject.GetComponentInChildren<Image>(true);
            logger.Log("EspressoStation: Found Image component in children");
        }

        if (drinkImage == null)
        {
            logger.LogError("EspressoStation: No Image component found on drink object or its children!");
            return;
        }

        if (currentDrink.Temp == Temperature.Hot)
        {
            // For hot drinks, use the simple espresso sprite
            if (hotEspressoSprite != null)
            {
                drinkImage.sprite = hotEspressoSprite;
                logger.Log("EspressoStation: Updated hot drink sprite to espresso");
            }
            else
            {
                logger.LogError("EspressoStation: No hotEspressoSprite assigned!");
            }
        }
        else
        {
            // For cold drinks, use the appropriate sprite based on ice level
            if (coldEspressoIceSprites != null && coldEspressoIceSprites.Length > 0)
            {
                int iceLevel = currentDrink.IceLevel;
                int spriteIndex = Mathf.Clamp(iceLevel, 1, coldEspressoIceSprites.Length - 1);
                
                if (coldEspressoIceSprites[spriteIndex] != null)
                {
                    drinkImage.sprite = coldEspressoIceSprites[spriteIndex];
                    logger.Log($"EspressoStation: Updated cold drink sprite to espresso with ice level {iceLevel}");
                }
                else
                {
                    logger.LogError($"EspressoStation: No sprite assigned for coldEspressoIceSprites at index {spriteIndex}");
                }
            }
            else
            {
                logger.LogError("EspressoStation: No coldEspressoIceSprites array assigned!");
            }
        }
        
        // Force refresh the UI
        drinkImage.SetAllDirty();
        Canvas.ForceUpdateCanvases();
    }

    private void OnContinue()
    {
        if (!espressoPoured)
        {
            logger.LogWarning("EspressoStation: Cannot continue - espresso not poured yet");
            return;
        }

        logger.Log("EspressoStation: Moving to next station");
        NavigationBar.Instance?.MarkStationCompleted(GameStateType.PouringEspresso);
        
        // Determine next state based on drink temperature
        if (currentDrink != null && currentDrink.Temp == Temperature.Iced)
        {
            // For iced drinks, might need to add milk or go to toppings
            GameStateManager.Instance.ChangeState(GameStateType.PlacingToppings);
        }
        else
        {
            // For hot drinks, go to toppings or milk
            GameStateManager.Instance.ChangeState(GameStateType.PlacingToppings);
        }
    }

    protected override void OnScreenHide()
    {
        base.OnScreenHide();
        
        // Stop any running coroutines when leaving the screen
        StopAllCoroutines();
    }
}