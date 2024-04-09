using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Thief : MonoBehaviour
{
    private bool isFacingRight = true;
    private bool isWalking;
    private bool playerSpotted = false;

    private int facingDir = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;

    private PlayerController pc;

    private Vector2 movement;

    [SerializeField]
    protected Transform
        playerCheck,
        target;

    [SerializeField]
    protected LayerMask playerMask;

    public GameObject effect;

    public StolenStats st;

    public float speed;
    public float aggroRange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        anim.SetBool("idle", true);

        pc = FindObjectOfType<PlayerController>();

        player = GameObject.Find("Player");

        target = player.transform;
    }

    void Update()
    {
        if (CheckPlayerAggro())
        {
            playerSpotted = true;
        }

        if (playerSpotted)
        {
            movement.Set(speed * facingDir, rb.velocity.y);
            rb.velocity = movement;
            isWalking = true;
        }
    }

    private void FixedUpdate()
    {
        if (CheckPlayerAggro())
        {
            if (transform.position.x > target.transform.position.x && isFacingRight)
            {
                Flip();
                isFacingRight = false;
                facingDir = -1;
            }
            else if (transform.position.x < target.transform.position.x && !isFacingRight)
            {
                Flip();
                isFacingRight = true;
                facingDir = 1;
            }
        }

        ChangeWalkAnim();
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    protected bool CheckPlayerAggro()
    {
        return Physics2D.OverlapCircle(playerCheck.position, aggroRange, playerMask);
    }

    protected void ChangeWalkAnim()
    {
        anim.SetBool("run", isWalking);
        anim.SetBool("idle", !isWalking);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerCheck.position, aggroRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, transform.rotation);

            StealStats();

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void StealStats()
    {
        List<float> stats = new List<float>
        {
            pc.maxHealth,
            pc.speed,
            pc.playerDamage
        };

        int randomIndex = Random.Range(0, stats.Count);

        float randomChoice = stats[randomIndex];

        int randSub = Random.Range(1, 5);

       if(randomIndex == 0)
        {
            pc.maxHealth -= randSub;
            if(pc.maxHealth <= 0)
            {
                pc.maxHealth = 1;
            }

            st.health += randSub;
        }
       else if(randomIndex == 1)
        {
            pc.speed -= randSub;

            if (pc.speed <= 0)
            {
                pc.speed = 1;
            }

            st.speed += randSub;
        }
       else if (randomIndex == 2)
        {
            pc.playerDamage -= randSub;

            if (pc.playerDamage <= 0)
            {
                pc.playerDamage = 1;
            }

            st.damage += randSub;
        }

        st.stolenCount++;
    }
}
