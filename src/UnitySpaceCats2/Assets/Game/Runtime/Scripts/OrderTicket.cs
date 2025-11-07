using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct OrderTicketData
{
    public bool isHot;
    public bool hasWhippedCream;
    public bool hasChocolateSyrup;
    public bool hasCaramelSyrup;

    public int numberOfIceCubes;
    public MilkType milk;
    public SyrupType syrup;

    public string drinkName;
}

public class OrderTicket : MonoBehaviour
{
    [Header("Sprites")] 
    public Sprite emptyHotCup;
    public Sprite emptyColdCup;

    public Sprite oneIce;
    public Sprite twoIce;
    public Sprite threeIce;
    public Sprite fourIce;

    public Sprite chocolateSyrupPump;
    public Sprite caramelSyrupPump;
    public Sprite mochaSyrupPump;

    public Sprite dairyMilk;
    public Sprite almondMilk;
    public Sprite oatMilk;

    public Sprite whippedCream;
    public Sprite chocolateSyrup;
    public Sprite caramelSyrup;

    /*
    [Header("GameObjects")] 
    public GameObject drinkType;
    public GameObject iceAmount;
    public GameObject syrupType;
    public GameObject milkType;
    public GameObject whippedCreamTopping;
    public GameObject chocolateSyrupTopping;
    public GameObject caramelSyrupTopping;
    */
    
    [Header("UI Images")]
    [SerializeField] private Image drinkTypeImage;
    [SerializeField] private Image iceAmountImage;
    [SerializeField] private Image syrupTypeImage;
    [SerializeField] private Image milkTypeImage;
    [SerializeField] private Image whippedCreamImage;
    [SerializeField] private Image chocolateSyrupImage;
    [SerializeField] private Image caramelSyrupImage;

    [SerializeField] private Text orderNumberText;
    [SerializeField] private Text drinkNameText;

    public RectTransform contentRoot;
    
    public int OrderNumber { get; private set; }

    public void Setup(int orderNumber, OrderTicketData data)
    {
        OrderNumber = orderNumber;

        if (orderNumberText)
            orderNumberText.text = $"#{orderNumber}";

        if (drinkNameText)
            drinkNameText.text = data.drinkName;

        // set drink image
        if (drinkTypeImage != null)
        {
            if (data.isHot)
                drinkTypeImage.sprite = emptyHotCup;
            else
                drinkTypeImage.sprite = emptyColdCup;
        }
        
        // Set ice image
        if (iceAmountImage)
        {
            if (data.isHot || data.numberOfIceCubes == 0)
                iceAmountImage.gameObject.SetActive(false);
            else
            {
                iceAmountImage.gameObject.SetActive(true);
                if (data.numberOfIceCubes == 1)
                    iceAmountImage.sprite = oneIce;
                else if (data.numberOfIceCubes == 2)
                    iceAmountImage.sprite = twoIce;
                else if (data.numberOfIceCubes == 3)
                    iceAmountImage.sprite = threeIce;
                else
                    iceAmountImage.sprite = fourIce;
            }
        }
        
        // Set syrup image
        if (syrupTypeImage)
        {
            if (data.syrup == SyrupType.None)
                syrupTypeImage.gameObject.SetActive(false);
            else
            {
                syrupTypeImage.gameObject.SetActive(true);
                if (data.syrup == SyrupType.Caramel)
                    syrupTypeImage.sprite = caramelSyrupPump;
                else if (data.syrup == SyrupType.Chocolate)
                    syrupTypeImage.sprite = chocolateSyrupPump;
                else if (data.syrup == SyrupType.Mocha)
                    syrupTypeImage.sprite = mochaSyrupPump;
            }
        }
        
        // Set milk
        // 4️⃣ Set milk
        if (milkTypeImage != null)
        {
            if (data.milk == MilkType.None)
            {
                milkTypeImage.gameObject.SetActive(false);
            }
            else
            {
                milkTypeImage.gameObject.SetActive(true);
                if (data.milk == MilkType.Almond)
                    milkTypeImage.sprite = almondMilk;
                else if (data.milk == MilkType.Oat)
                    milkTypeImage.sprite = oatMilk;
                else if (data.milk == MilkType.Dairy)
                    milkTypeImage.sprite = dairyMilk;
            }
        }

        // 5️⃣ Toppings
        if (whippedCreamImage != null)
        {
            if (data.hasWhippedCream)
                whippedCreamImage.gameObject.SetActive(true);
            else
                whippedCreamImage.gameObject.SetActive(false);
        }

        if (chocolateSyrupImage != null)
        {
            if (data.hasChocolateSyrup)
                chocolateSyrupImage.gameObject.SetActive(true);
            else
                chocolateSyrupImage.gameObject.SetActive(false);
        }

        if (caramelSyrupImage != null)
        {
            if (data.hasCaramelSyrup)
                caramelSyrupImage.gameObject.SetActive(true);
            else
                caramelSyrupImage.gameObject.SetActive(false);
        }
        
    }

    public void SetContentVisible(bool visible)
    {
        if (!contentRoot)
            return;
        
        contentRoot.gameObject.SetActive(visible);
    }
    
}
