using UnityEngine;

public class DestroyRockOnAmetista : MonoBehaviour
{
    private bool destroyed = false;

    private void Update()
    {
        // Evita rodar a lógica depois que a pedra já foi destruída
        if (destroyed) return;

        // Garante que o inventário existe
        if (InventarioManager.instance == null) return;

        // Verifica se o jogador já coletou a ametista
        if (InventarioManager.instance.hasAmetista)
        {
            Debug.Log("Pedra destruída pela ametista!");
            destroyed = true; // impede chamadas repetidas
            Destroy(gameObject); // destrói a pedra
        }
    }
}