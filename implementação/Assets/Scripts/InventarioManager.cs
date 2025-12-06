using UnityEngine;
using UnityEngine.UI;

public class InventarioManager : MonoBehaviour
{
    public bool hasGold = false;
    public bool hasAmetista = false; // nova variável
    public static InventarioManager instance;

    [Header("UI")]
    public GameObject inventoryUI;
    public InventorySlot[] slots;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ToggleInventory()
    {
        if (inventoryUI != null)
            inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    // ----------------------- ADICIONAR ITEM NORMAL -----------------------
    public void AddItemToInventory(Sprite itemSprite)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot != null && !slot.isFull)
            {
                slot.AddItem(itemSprite);

                string n = itemSprite.name.ToLower();

                // ----------- DETECTAR OURO -----------
                if (n.Contains("metal_0")) // <-- SEU SPRITE DE OURO
                {
                    hasGold = true;
                    Debug.Log("OURO detectado: Metal_0");
                }

                // ----------- DETECTAR AMETISTA -----------
                if (n.Contains("amm_0")) // <-- SEU SPRITE DE AMETISTA
                {
                    hasAmetista = true;
                    Debug.Log("AMETISTA detectada!");
                }

                return;
            }
        }

        Debug.Log("Inventário cheio!");
    }

    // ----------------------- ADICIONAR ITEM EXCLUSIVO -----------------------
    // Limpa todo o inventário antes de adicionar o item (ex: espada)
    public void AddUniqueItem(Sprite itemSprite)
    {
        // Limpa todos os slots
        foreach (InventorySlot slot in slots)
        {
            slot.ClearSlot();
        }

        // Reseta flags
        hasGold = false;
        hasAmetista = false;

        // Adiciona a espada no primeiro slot disponível
        if (slots.Length > 0)
        {
            slots[0].AddItem(itemSprite);
            Debug.Log("Espada adicionada no inventário e outros itens removidos!");
        }
    }

    public int CountItems(string itemName)
    {
        int count = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.isFull && slot.itemSprite != null)
            {
                if (slot.itemSprite.name.ToLower().Contains(itemName.ToLower()))
                    count++;
            }
        }

        return count;
    }

    public void RemoveItems(string itemName, int amount)
    {
        int removed = 0;

        foreach (InventorySlot slot in slots)
        {
            if (removed >= amount) break;

            if (slot.isFull && slot.itemSprite != null)
            {
                if (slot.itemSprite.name.ToLower().Contains(itemName.ToLower()))
                {
                    slot.ClearSlot();
                    removed++;
                }
            }
        }
    }
}
