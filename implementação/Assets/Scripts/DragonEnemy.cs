using UnityEngine;
using System.Collections;

public class DragonEnemy : MonoBehaviour
{
    [Header("Configurações")]
    public int maxHealth = 10;
    public int currentHealth;

    [Header("Ataque")]
    public float fireRate = 2f;        // tempo entre disparos
    private float fireCooldown;
    public float attackRange = 6f;     // distância mínima para atacar

    [Header("Referências")]
    public Transform firePoint;        // onde nasce a bola
    public GameObject fireballPrefab;  // prefab da bola
    public GameObject magnus;          // objeto a destruir junto com o dragão

    [Header("Efeito de dano")]
    public SpriteRenderer spriteRenderer; // sprite do dragão
    public float flashDuration = 0.1f;    // duração do flash
    public int flashCount = 4;            // quantas vezes pisca

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        fireCooldown = fireRate;

        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // só dispara se o player estiver dentro do alcance
        if (distanceToPlayer <= attackRange)
        {
            fireCooldown -= Time.deltaTime;

            if (fireCooldown <= 0)
            {
                ShootFireball();
                fireCooldown = fireRate;
            }
        }
    }

    void ShootFireball()
    {
        GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Fireball fireball = fb.GetComponent<Fireball>();
        if (fireball != null)
        {
            fireball.SetDirection(firePoint.up);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (spriteRenderer != null)
                StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.white;      // pisca branco
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;    // volta à cor normal
            yield return new WaitForSeconds(flashDuration);
        }
    }

    void Die()
    {
        // destrói Magnus junto com o dragão
        if (magnus != null)
            Destroy(magnus);

        Destroy(gameObject); // destrói o dragão
    }
}
