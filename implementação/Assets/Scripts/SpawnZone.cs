using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [Header("Configuração do Spawn")]
    public GameObject enemyPrefab;
    public int maxSpawn = 10;        // quantos duendes no máximo
    public float spawnInterval = 2f; // intervalo entre spawns
    public Transform spawnPoint;     // onde vai spawnar (pode ser this.transform)

    private bool playerInside = false;
    private float spawnTimer = 0f;
    private int spawnedCount = 0;

    private void Start()
    {
        if (spawnPoint == null)
            spawnPoint = transform;
    }

    private void Update()
    {
        if (!playerInside) return;
        if (spawnedCount >= maxSpawn) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        spawnedCount++;
    }

    // Detecta player na região
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    // Se quiser parar de spawnar quando sair da área, pode deixar esse método.
    // Se não quiser, pode apagar.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}

