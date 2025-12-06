using UnityEngine;

public class DuendeEnemy : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 3f;
    public Transform player;

    [Header("Dano ao Player")]
    public int damage = 1;                 // agora é inteiro
    public float damageCooldown = 1f;      // tempo entre danos
    private float damageTimer = 0f;

    void Update()
    {
        // Se ainda não encontrou o player, procura
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            return;
        }

        // Movimentação do duende em direção ao player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Reduz o cooldown com o tempo
        if (damageTimer > 0f)
            damageTimer -= Time.fixedDeltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (damageTimer <= 0f)
        {
            // Tenta pegar o script Player no próprio collider
            Player playerScript = collision.collider.GetComponent<Player>();

            // Se não achou, tenta pegar no pai
            if (playerScript == null)
                playerScript = collision.collider.GetComponentInParent<Player>();

            // Se encontrou, dá dano
            if (playerScript != null)
                playerScript.TakeDamage(damage);

            // Reseta cooldown
            damageTimer = damageCooldown;
        }
    }
}





