using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    [Header("Sprites dos itens finais")] public Sprite ponteSprite;
    public Sprite espadaSprite;

    private void Awake()
    {
        instance = this;
    }

    // --------------------------
    // VERIFICA SE ALGUMA RECEITA É POSSÍVEL
    // --------------------------
    public bool CanCraftAnything()
    {
        return CanCraftPonte() || CanCraftEspada();
    }

    // ---------- RECEITA: PONTE ----------
    private bool CanCraftPonte()
    {
        return InventarioManager.instance.CountItems("corda_0") >= 2 &&
               InventarioManager.instance.CountItems("Tronco") >= 2;
    }

    // ---------- RECEITA: ESPADA ----------
    private bool CanCraftEspada()
    {
        return InventarioManager.instance.CountItems("Metal_0") >= 1 &&
               InventarioManager.instance.CountItems("aMM_0") >= 1 &&
               InventarioManager.instance.CountItems("FS_0") >= 1;
    }

    // Chamado pelo botão CRIAR
    public void Craft()
    {
        // Criar ponte
        if (CanCraftPonte())
        {
            InventarioManager.instance.RemoveItems("corda_0", 2);
            InventarioManager.instance.RemoveItems("Tronco", 2);
            InventarioManager.instance.AddItemToInventory(ponteSprite);
            Debug.Log("Ponte criada!");
            return;
        }

        // Criar espada
        if (CanCraftEspada())
        {
            // Remove os materiais
            InventarioManager.instance.RemoveItems("metal_0", 1);
            InventarioManager.instance.RemoveItems("amm_0", 1);
            InventarioManager.instance.RemoveItems("fs_0", 1); // supondo que FS_0 seja o item "purosangue"

            // ADICIONA A ESPADA E LIMPA TODO O INVENTÁRIO
            InventarioManager.instance.AddUniqueItem(espadaSprite);
            Debug.Log("Espada criada e inventário limpo!");
            return;
        }

        Debug.Log("Sem materiais!");
    }
}

