using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public GameObject audioColetaSimples;
    public Sprite itemIcon;

    private void Reset()
    {
        Collider2D c = GetComponent<Collider2D>();
        if (c == null)
        {
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            c.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventarioManager.instance != null)
            {
                GameObject preFab = Instantiate(audioColetaSimples, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                InventarioManager.instance.AddItemToInventory(itemIcon);
                Destroy(preFab.gameObject, 1f);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("ItemColetavel: InventarioManager.instance é null.");
            }
        }
    }
}



