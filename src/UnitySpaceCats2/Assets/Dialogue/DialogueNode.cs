using System;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    [TextArea(2, 4)]
    public string customerLine;
    
    [TextArea(2, 4)]
    public string playerResponseOption1;
    
    [TextArea(2, 4)]
    public string customerFollowUp1;
    
    [TextArea(2, 4)]
    public string playerResponseOption2;
    
    [TextArea(2, 4)]
    public string customerFollowUp2;
    
    public bool hasRemakeOption;
}

[CreateAssetMenu(fileName = "DialoguePathData", menuName = "Scriptable Objects/DialoguePathData")]
public class DialoguePathData : ScriptableObject
{
    [Header("No Star Dialogue")]
    public DialogueNode noStarDialogue;
    
    [Header("One Star - Good Drink")]
    public DialogueNode oneStarGoodDialogue;
    
    [Header("One Star - Bad Drink")]
    public DialogueNode oneStarBadDialogue;
    
    [Header("Three Star - Good Drink")]
    public DialogueNode threeStarGoodDialogue;
    
    [Header("Three Star - Bad Drink")]
    public DialogueNode threeStarBadDialogue;
    
    [Header("Five Star - Good Drink")]
    public DialogueNode fiveStarGoodDialogue;
    
    [Header("Five Star - Bad Drink")]
    public DialogueNode fiveStarBadDialogue;
}