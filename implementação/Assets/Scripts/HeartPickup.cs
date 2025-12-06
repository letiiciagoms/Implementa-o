using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public GameObject heart;
    public int healAmount = 1; // cura 1 ponto de vida

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.Heal(healAmount);
            }
            
            GameObject preFab = Instantiate(heart, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            Destroy(preFab.gameObject, 1f);
            Destroy(gameObject); // some ao coletar
        }
    }
}

