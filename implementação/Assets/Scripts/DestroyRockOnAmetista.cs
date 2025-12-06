using UnityEngine;

public class DestroyRockOnAmetista : MonoBehaviour
{
    private bool destroyed = false;

    private void Update()
    {
        // Evita spam ou checar depois de destruir
        if (destroyed) return;

        if (InventarioManager.instance != null)
        {
            // Checa se o jogador coletou a ametista
            if (InventarioManager.instance.hasAmetista) // precisa existir no InventarioManager
            {
                Debug.Log("Pedra destruída pela ametista!");
                destroyed = true;
                Destroy(gameObject);
            }
        }
    }
}