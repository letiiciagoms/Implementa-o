using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    [Header("Sprites finais")]
    public Sprite ponteSprite;
    public Sprite espadaSprite;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanCraftAnything()
    {
        return CanCraftPonte() || CanCraftEspada();
    }

    private bool CanCraftPonte()
    {
        return InventarioManager.instance.CountItems("corda_0") >= 2 &&
               InventarioManager.instance.CountItems("tronco") >= 2;
    }

    private bool CanCraftEspada()
    {
        return InventarioManager.instance.CountItems("metal_0") >= 1 &&
               InventarioManager.instance.CountItems("amm_0") >= 1 &&
               InventarioManager.instance.CountItems("fs_0") >= 1;
    }

    public void Craft()
    {
        if (InventarioManager.instance == null) return;

        if (CanCraftPonte())
        {
            InventarioManager.instance.RemoveItems("corda_0", 2);
            InventarioManager.instance.RemoveItems("tronco", 2);
            InventarioManager.instance.AddItemToInventory(ponteSprite);
            Debug.Log("Ponte criada!");
            return;
        }

        if (CanCraftEspada())
        {
            InventarioManager.instance.RemoveItems("metal_0", 1);
            InventarioManager.instance.RemoveItems("amm_0", 1);
            InventarioManager.instance.RemoveItems("fs_0", 1);

            InventarioManager.instance.AddUniqueItem(espadaSprite);
            Debug.Log("Espada criada e inventário limpo!");
            return;
        }

        Debug.Log("Sem materiais suficientes!");
    }
}
