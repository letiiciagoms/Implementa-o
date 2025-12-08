using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public bool isFull = false;
    public Sprite itemSprite;

    public GameObject ponteNoRio;
    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void AddItem(Sprite sprite)
    {
        itemSprite = sprite;
        icon.sprite = itemSprite;
        icon.enabled = true;
        icon.preserveAspect = true;
        isFull = true;
    }

    public void ClearSlot()
    {
        itemSprite = null;
        icon.sprite = null;
        icon.enabled = false;
        isFull = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFull || itemSprite == null) return;

        string itemName = itemSprite.name.ToLower();

        // -------- BLOQUEIO MACHADO PERMANENTE --------
        if ((itemName.Contains("axe") || itemName.Contains("machado")) && player.axePermanentlyRemoved)
        {
            Debug.Log("Machado não pode mais ser equipado.");
            return;
        }

        // -------- USO DE ITENS --------
        if (itemName.Contains("ponte")) { UsarPonte(); return; }
        if (itemName.Contains("rosa")) { UsarRosa(); return; }
        if (itemName.Contains("maca") || itemName.Contains("maça")) { UsarMaca(); return; }
        if (itemName.Contains("amm_0")) { UsarAmetista(); return; }

        // -------- EQUIPAR ARMAS --------
        if (itemName.Contains("axe") || itemName.Contains("machado"))
        {
            if (player.weaponType == 1)
                player.UnequipAllWeapons();
            else
                player.EquipAxe();
            return;
        }

        if (itemName.Contains("sword") || itemName.Contains("espada"))
        {
            if (player.weaponType == 2)
                player.UnequipAllWeapons();
            else
                player.EquipSword();
            return;
        }
    }

    void UsarPonte()
    {
        if (ponteNoRio != null)
            ponteNoRio.SetActive(true);
        ClearSlot();
    }

    void UsarRosa()
    {
        if (AltarManager.instance != null)
            AltarManager.instance.ColocarRosaEmAltar();
        ClearSlot();
    }

    void UsarMaca()
    {
        if (AltarManager.instance != null)
            AltarManager.instance.ColocarMacaEmAltar();
        ClearSlot();
    }

    void UsarAmetista()
    {
        if (AltarManager.instance != null)
            AltarManager.instance.AtivarAmetista();
        InventarioManager.instance.hasAmetista = true;
        ClearSlot();
    }
}
