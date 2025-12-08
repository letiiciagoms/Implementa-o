using UnityEngine;
using UnityEngine.UI;

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager instance;

    [Header("Flags de Itens Especiais")]
    public bool hasGold = false;
    public bool hasAmetista = false;

    [Header("UI")]
    public GameObject inventoryUI;
    public InventorySlot[] slots;

    public CommandInvoker invoker = new CommandInvoker(); // Undo global

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

    public void AddItemToInventory(Sprite itemSprite)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot != null && !slot.isFull)
            {
                slot.AddItem(itemSprite);
                UpdateFlags(itemSprite);
                return;
            }
        }

        Debug.Log("Inventário cheio!");
    }

    // Adiciona item exclusivo (como espada)
    public void AddUniqueItem(Sprite itemSprite)
    {
        foreach (InventorySlot slot in slots)
            slot.ClearSlot();

        hasGold = false;
        hasAmetista = false;

        // Passa null como GameObject porque não existe no mapa
        ICommand collectCommand = new CollectItemCommand(this, itemSprite, null);
        invoker.ExecuteCommand(collectCommand);

        Debug.Log("Item exclusivo adicionado via Command!");
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

        UpdateFlagsAfterRemoval();
    }

    private void UpdateFlags(Sprite itemSprite)
    {
        string n = itemSprite.name.ToLower();
        if (n.Contains("metal_0")) hasGold = true;
        if (n.Contains("amm_0")) hasAmetista = true;
    }

    private void UpdateFlagsAfterRemoval()
    {
        hasGold = CountItems("metal_0") > 0;
        hasAmetista = CountItems("amm_0") > 0;
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

    public void UndoLastCommand()
    {
        invoker.UndoLastCommand();
    }
}
