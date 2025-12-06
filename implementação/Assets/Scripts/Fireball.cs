using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 8f;     // velocidade da bola
    public int damage = 1;       // dano no player
    public float lifeTime = 4f;  // tempo de vida da bola

    private Vector2 direction = Vector2.up;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // move a bola sempre na direção definida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // opcional: gira o sprite para ficar alinhado
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // -90 para sprite apontando para cima
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
        }
        
    }
}