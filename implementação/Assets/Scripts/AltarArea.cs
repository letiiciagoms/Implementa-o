using UnityEngine;

public class AltarArea : MonoBehaviour
{
    public GameObject craftButton; // botão Criar

    private void Start()
    {
        if (craftButton != null)
            craftButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CraftingManager.instance != null && CraftingManager.instance.CanCraftAnything())
            {
                if (craftButton != null)
                    craftButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (craftButton != null)
                craftButton.SetActive(false);
        }
    }
}


