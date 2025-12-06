using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackRange = 1f;        // alcance do ataque
    public float attackOffset = 0.8f;     // distância à frente do player
    public LayerMask enemyLayer;          // escolha a layer Enemy
    public float attackCooldown = 0.3f;

    private float timer;
    private Vector2 lastDir = Vector2.down;

    public Player player;  // arraste o Player aqui (para pegar direção)

    void Update()
    {
        // pega a direção que o player está se movendo
        if (player.movimento.magnitude > 0.1f)
        {
            lastDir = player.movimento.normalized;
        }

        if (timer > 0)
            timer -= Time.deltaTime;

        // ataque ao apertar espaço
        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        {
            Attack();
            timer = attackCooldown;
        }
    }

    void Attack()
    {
        Vector2 attackPos = (Vector2)transform.position + lastDir * attackOffset;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, attackRange, enemyLayer);

        foreach (var hit in hits)
        {
            // 1) Inimigos simples (EnemyHealth)
            EnemyHealth eh = hit.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damage);
                Debug.Log("Acertou inimigo comum!");
            }

            // 2) Ciclope
            CiclopeEnemy ce = hit.GetComponent<CiclopeEnemy>();
            if (ce != null)
            {
                ce.TakeDamage(damage);
                Debug.Log("Acertou ciclope!");
            }

            // 3) DRAGÃO — integração oficial
            DragonEnemy dr = hit.GetComponent<DragonEnemy>();
            if (dr != null)
            {
                dr.TakeDamage(damage);
                Debug.Log("Acertou o DRAGÃO!");
            }
        }

        Debug.DrawLine(transform.position, attackPos, Color.red, 0.2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 pos = (Vector2)transform.position + lastDir * attackOffset;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
