using UnityEngine;

public class FrascoSangueTrigger : MonoBehaviour
{
    public Sprite frascoSangueSprite; // Arraste o sprite do frasco no Inspector
    private bool dado = false;        // Evita adicionar várias vezes

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dado) return;

        if (collision.CompareTag("Player"))
        {
            if (InventarioManager.instance != null && frascoSangueSprite != null)
            {
                InventarioManager.instance.AddItemToInventory(frascoSangueSprite);
                Debug.Log("Frasco de sangue adicionado ao inventário!");
                dado = true; // para não adicionar de novo
            }
        }
    }
}