using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealth))]
public class CiclopeEnemy : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 2.5f;
    public Transform player;

    [Header("Distâncias de detecção")]
    public float idleRange = 5f;
    public float attackRange = 2f;

    [Header("Dano ao Player")]
    public int damage = 1;
    public float damageCooldown = 1f;
    private float damageTimer = 0f;

    private Animator animator;
    private EnemyHealth enemyHealth;

    private enum State { Walk, Idle, Attack }
    private State currentState = State.Walk;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
            currentState = State.Attack;
        else if (distance <= idleRange)
            currentState = State.Idle;
        else
            currentState = State.Walk;

        animator.SetBool("isWalking", currentState == State.Walk);
        animator.SetBool("isAttacking", currentState == State.Attack);

        if (currentState == State.Walk)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (damageTimer > 0f)
            damageTimer -= Time.fixedDeltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (currentState == State.Attack && damageTimer <= 0f)
        {
            Player playerScript = collision.collider.GetComponent<Player>();
            if (playerScript == null)
                playerScript = collision.collider.GetComponentInParent<Player>();

            if (playerScript != null)
                playerScript.TakeDamage(damage);

            damageTimer = damageCooldown;
        }
    }

    // ----------------------- DANO RECEBIDO -----------------------
    public void TakeDamage(int dmg)
    {
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(dmg);
            animator.SetTrigger("hit"); // se tiver trigger "hit" no Animator
        }
    }
}
