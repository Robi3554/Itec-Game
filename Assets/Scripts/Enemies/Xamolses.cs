using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Xamolses : MonoBehaviour
{
    private bool isFacingRight = true;
    private bool chargingAttack = false;
    private bool inPhaseOne = false;

    private int facingDir = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;

    [SerializeField]
    private GameObject
        attack,
        explosion;

    private Vector2 movement;

    [SerializeField]
    protected Transform
        playerCheck,
        attackCheck,
        target;

    [SerializeField]
    protected LayerMask playerMask;

    public StolenStats stolenStats;

    public float speed;
    public float aggroRange;
    public float attackRange;
    public float damage;
    public float maxHealth;

    private void Awake()
    {
        AddStats();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        anim.SetBool("idle", true);

        player = GameObject.Find("Player");

        target = player.transform;
    }

    void Update()
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

    private void FixedUpdate()
    {
        if (CheckPlayerAggro())
        {
            Debug.Log("PlayeDetected");
        }

        if (inPhaseOne)
        {
            movement.Set(speed * facingDir, rb.velocity.y);
            rb.velocity = movement;
        }

        if (CheckAttackRange())
        {
            ChangePhase();
        }
    }

    private void AddStats()
    {
        maxHealth += stolenStats.health;

        damage += stolenStats.damage;

        speed += stolenStats.speed;
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
        return Physics2D.Raycast(playerCheck.position, new Vector2(facingDir, 0), aggroRange, playerMask);
    }

    protected bool CheckAttackRange()
    {
        return Physics2D.OverlapCircle(attackCheck.position, attackRange, playerMask);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawRay(playerCheck.position, new Vector2(facingDir, 0));
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
    }

    public IEnumerator Attacking()
    {
        if (!chargingAttack)
        {
            chargingAttack = true;

            yield return new WaitForSeconds(3f);

            if (CheckPlayerAggro())
            {
                if (CheckAttackRange())
                {
                    BeginAttack1Phase2();
                }
                else
                {
                    BeginAttack1Phase1();
                }
            }
            else
            {
                BeginAttack2();
            }
        }
    }

    public void StartingCorutine()
    {
        StartCoroutine(Attacking());
    }

    public void BeginAttack1Phase1()
    {
        Debug.Log("Attack1");
        anim.SetBool("run", true);
        inPhaseOne = true;
    }

    public void StartHitbox()
    {
        attack.SetActive(true);
    }

    public void StopHitbox()
    {
        attack.SetActive(false);
    }

    public void ChangePhase()
    {
        inPhaseOne = false;
        BeginAttack1Phase2();
    }

    public void BeginAttack1Phase2()
    {
        anim.SetBool("attack1", true);
        anim.SetBool("run", false);
    }

    public void ReturnToIdle()
    {
        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
        anim.SetBool("idle", true);
        chargingAttack = false;
    }

    public void BeginAttack2()
    {
        anim.SetBool("attack2", true);
    }

    public void DoExplosion()
    {
        Instantiate(explosion, target.transform.position, transform.rotation);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}