using UnityEngine;

[CreateAssetMenu(fileName = "CatDefinition", menuName = "Scriptable Objects/CatDefinition")]
public class CatDefinition : ScriptableObject
{
    public int counter;
    public string catName;
    public Sprite catSprite;
}
