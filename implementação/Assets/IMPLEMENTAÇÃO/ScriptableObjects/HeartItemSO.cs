using UnityEngine;

[CreateAssetMenu(fileName = "New Heart Item", menuName = "Items/Heart")]
public class HeartItemSO : ScriptableObject
{
    [Header("Configuração do Item")]
    public string itemName = "Heart";
    public int healAmount = 1;

    [Header("Efeitos Visuais")]
    public GameObject collectEffect; // partículas, animação, etc.
}