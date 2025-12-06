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

        string n = itemSprite.name.ToLower();

        if (n.Contains("metal_0"))
            InventarioManager.instance.hasGold = true;

        if (n.Contains("amm_0"))
            InventarioManager.instance.hasAmetista = true;
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
        if (!isFull || itemSprite == null)
            return;

        string itemName = itemSprite.name.ToLower();

        // -------- BLOQUEIO PERMANENTE DO MACHADO -----------
        if (itemName.Contains("axe") || itemName.Contains("machado"))
        {
            if (player.axePermanentlyRemoved)
            {
                Debug.Log("Machado não pode mais ser equipado.");
                return;
            }
        }

        // -------- PONTE --------
        if (itemName.Contains("ponte"))
        {
            UsarPonte();
            return;
        }

        // -------- ROSA --------
        if (itemName.Contains("rosa"))
        {
            UsarRosa();
            return;
        }

        // -------- MAÇÃ --------
        if (itemName.Contains("maca") || itemName.Contains("maça"))
        {
            UsarMaca();
            return;
        }

        // -------- AMETISTA --------
        if (itemName.Contains("amm_0"))
        {
            UsarAmetista();
            return;
        }

        // -------- MACHADO --------
        if (itemName.Contains("axe") || itemName.Contains("machado"))
        {
            if (player.weaponType == 1)
                player.UnequipAllWeapons();
            else
                player.EquipAxe();

            return;
        }

        // -------- ESPADA --------
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
