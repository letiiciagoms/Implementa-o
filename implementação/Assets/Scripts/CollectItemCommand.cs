using UnityEngine;

public class CollectItemCommand : ICommand
{
    private Sprite itemSprite;
    private InventarioManager inventario;
    private GameObject itemObject; // referência ao objeto no mapa (null para itens exclusivos)

    public CollectItemCommand(InventarioManager inventario, Sprite itemSprite, GameObject itemObject)
    {
        this.inventario = inventario;
        this.itemSprite = itemSprite;
        this.itemObject = itemObject;
    }

    public void Execute()
    {
        inventario.AddItemToInventory(itemSprite);

        if (itemObject != null)
            itemObject.SetActive(false); // esconde o item no mapa
    }

    public void Undo()
    {
        inventario.RemoveItems(itemSprite.name, 1);

        if (itemObject != null)
        {
            itemObject.SetActive(true); // volta o item para o mapa
            itemObject.transform.position = itemObject.GetComponent<ItemColetavel>().originalPosition;
        }
    }
}