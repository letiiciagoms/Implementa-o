using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public Sprite itemIcon;
    public GameObject audioColetaSimples;
    public KeyCode undoKey = KeyCode.Z;

    [HideInInspector] public Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Reset()
    {
        Collider2D c = GetComponent<Collider2D>();
        if (c == null)
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        else
            c.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (InventarioManager.instance == null)
        {
            Debug.LogError("InventarioManager.instance é null!");
            return;
        }

        // Passa o GameObject para o comando
        ICommand collectCommand = new CollectItemCommand(InventarioManager.instance, itemIcon, this.gameObject);
        InventarioManager.instance.invoker.ExecuteCommand(collectCommand);

        if (audioColetaSimples != null)
        {
            GameObject preFab = Instantiate(audioColetaSimples, transform.position, Quaternion.identity);
            Destroy(preFab, 1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(undoKey) && InventarioManager.instance != null)
        {
            InventarioManager.instance.UndoLastCommand();
        }
    }
}