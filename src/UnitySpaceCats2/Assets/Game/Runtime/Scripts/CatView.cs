using UnityEngine;
using UnityEngine.UI;

public class CatView : MonoBehaviour
{
    [SerializeField] private Image catImage;
    [SerializeField] private Text catNameText; 
    
    public void Initialize(CatDefinition def)
    {
        if (def == null) return;
        if (catImage != null) catImage.sprite = def.catSprite;
        if (catNameText != null) catNameText.text = def.catName;
    }
}