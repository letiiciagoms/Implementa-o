using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movimentação")]
    public Rigidbody2D playerRB;
    public Vector2 movimento;
    public float speed;

    [Header("Animação")]
    public Animator animator;
    public Transform spawnPoint;

    [Header("Sistema de Armas")]
    public int weaponType = 0;  
    public bool isAttacking = false;

    public bool axePermanentlyRemoved = false;

    [Header("Objetos das Armas na Mão")]
    public GameObject axeInHand;
    public GameObject swordInHand;

    [Header("Flash Overlay")]
    public SpriteRenderer flashOverlay; 
    public float flashSpeed = 0.1f;
    public int flashCount = 6;

    [Header("Sistema de Vida")]
    public int maxHealth = 5;
    public int currentHealth;
    public Image heartIcon;
    public TMP_Text healthText;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerRB.bodyType = RigidbodyType2D.Dynamic;
        playerRB.gravityScale = 0;
        playerRB.freezeRotation = true;

        if (axeInHand != null) axeInHand.SetActive(false);
        if (swordInHand != null) swordInHand.SetActive(false);

        if (flashOverlay != null)
        {
            Color c = flashOverlay.color;
            c.a = 0;
            flashOverlay.color = c;
        }

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movimento.x);
        animator.SetFloat("Vertical", movimento.y);
        animator.SetFloat("Speed", movimento.sqrMagnitude);

        if (movimento != Vector2.zero)
        {
            animator.SetFloat("HorizontalIdle", movimento.x);
            animator.SetFloat("VerticalIdle", movimento.y);
        }

        animator.SetInteger("WeaponType", weaponType);

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            StartAttack();
    }

    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movimento * (speed * Time.fixedDeltaTime));
    }

    void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);

        float attackTime = (weaponType == 1) ? 0.45f :
                           (weaponType == 2) ? 0.35f : 0.3f;

        Invoke(nameof(EndAttack), attackTime);
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    public void EquipAxe()
    {
        if (axePermanentlyRemoved)
        {
            Debug.Log("Machado removido permanentemente. Não pode equipar.");
            return;
        }

        UnequipAllWeapons();
        weaponType = 1;
        axeInHand.SetActive(true);
    }

    public void EquipSword()
    {
        UnequipAllWeapons();
        weaponType = 2;
        swordInHand.SetActive(true);

        axePermanentlyRemoved = true;
    }

    public void UnequipAllWeapons()
    {
        weaponType = 0;
        axeInHand.SetActive(false);
        swordInHand.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth == 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (heartIcon != null)
            heartIcon.enabled = true;

        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    private void Die()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        this.enabled = false;

        yield return StartCoroutine(FlashOverlayEffect());
        yield return new WaitForSeconds(0.3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    private IEnumerator FlashOverlayEffect()
    {
        for (int i = 0; i < flashCount; i++)
        {
            Color c = flashOverlay.color;
            c.a = 1f;
            flashOverlay.color = c;

            yield return new WaitForSeconds(flashSpeed);

            c.a = 0f;
            flashOverlay.color = c;

            yield return new WaitForSeconds(flashSpeed);
        }
    }

    void RespawnPlayer()
    {
        transform.position = spawnPoint.position;

        Animator anim = GetComponent<Animator>();
        anim.Rebind();
        anim.Update(0f);
    }

    // -------------------------
    // DETECTAR FIREBALL
    // -------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {
            TakeDamage(1);
        }
    }
}
