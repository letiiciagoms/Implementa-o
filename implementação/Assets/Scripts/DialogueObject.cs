using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] sentences; // Falas do NPC
}



