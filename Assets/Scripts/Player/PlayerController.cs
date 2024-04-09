using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int extraJumpsValue;
    public float maxHealth;
    public int numberOfFlashes;

    public float speed;
    public float jumpForce;
    public float checkRadius;
    public float flashDuration;
    public float playerDamage;

    public Color flashColor;
    public Color regularColor;

    public HealthBar healthBar;

    public GameObject weapon;

    public Transform groundCheck;
    public Transform playerSpawn;

    public BoxCollider2D triggerCol;

    public LayerMask groundLayer;

    private float moveInput;
    private float currentHealth;

    private int extraJumps;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isAttacking;

    [SerializeField]
    private bool isGrounded;

    private Rigidbody2D rb;

    private Animator anim;

    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        extraJumps = extraJumpsValue;

        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        transform.position = playerSpawn.transform.position;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            MovePlayer();
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        Jump();
    }

    void FixedUpdate()
    {
        if(isFacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(isFacingRight == true && moveInput < 0)
        {
            Flip();
        }

        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        ChangeWalkAnim();

        CheckGround();

        Attack();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void MovePlayer()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void ChangeWalkAnim()
    {
        anim.SetBool("walk", isWalking);
        anim.SetBool("idle", !isWalking);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void Attack()
    {
        if (Input.GetButton("Fire1"))
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("attack", true);
            isAttacking = true;
            weapon.SetActive(true);
        }
    }

    public void TakeDamage(float ammount)
    {
        currentHealth -= ammount;

        healthBar.SetHealth(currentHealth);

        StartCoroutine(FlashCo());

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddLife(float ammount)
    {
        currentHealth += ammount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void ModifyMaxHealth(float ammount)
    {
        maxHealth += ammount;

        healthBar.SetMaxHealth(maxHealth);
    }

    public void StopAttack()
    {
        anim.SetBool("attack", false);
        isAttacking = false;
        weapon.SetActive(false);
    }

    public void ReactivateTrigger()
    {
        weapon.GetComponent<DealDamage>().hasTriggered = false;
    }

    public float GetDamage()
    {
        return playerDamage;
    }

    public IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCol.enabled = false;
        while(temp < numberOfFlashes)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sr.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCol.enabled = true;
    }
}
