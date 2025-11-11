using UnityEngine;

[CreateAssetMenu(fileName = "CatDefinition", menuName = "Scriptable Objects/CatDefinition")]
public class CatDefinition : ScriptableObject
{
    public int counter;
    public string catName;
    public Sprite catSprite;
    
    [Header("Dialogue Settings")]
    [Tooltip("Dialogue path this cat follows (1, 2, 3, or 4). Set at game start.")]
    public int dialoguePath = 0; // 0 = unassigned, will be set to 1, 2, 3, or 4
}