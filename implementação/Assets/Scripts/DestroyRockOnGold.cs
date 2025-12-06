using UnityEngine;

public class DestroyRockOnGold : MonoBehaviour
{
    private bool destroyed = false;

    private void Update()
    {
        // evitar spam ou checar depois de destruir
        if (destroyed) return;

        if (InventarioManager.instance != null)
        {
            if (InventarioManager.instance.hasGold)
            {
                Debug.Log("Pedra destruída!");
                destroyed = true;
                Destroy(gameObject);
            }
        }
    }
}





