using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public HeartItemSO itemData; // referência ao ScriptableObject

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                // usa o valor vindo do ScriptableObject
                player.Heal(itemData.healAmount);
            }

            // instancia partícula ou efeito do ScriptableObject
            if (itemData.collectEffect != null)
            {
                GameObject effect = Instantiate(
                    itemData.collectEffect,
                    transform.position,
                    Quaternion.identity
                );
                Destroy(effect, 1f);
            }

            Destroy(gameObject);
        }
    }
}