using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private bool isFacingRight = true;
    private bool isWalking;
    private bool isAttacking;

    private int facingDir = 1;

    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;

    [SerializeField]
    private GameObject attack;

    private Vector2 movement;

    [SerializeField]
    protected Transform
        playerCheck,
        attackCheck,
        target;

    [SerializeField]
    protected LayerMask playerMask;

    public float speed;
    public float aggroRange;
    public float attackRange;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 

        anim = GetComponent<Animator>();

        anim.SetBool("idle", true);

        isAttacking = false;

        player = GameObject.Find("Player");

        target = player.transform;
    }

    protected void Update()
    {
        if (CheckPlayerAggro() && !isAttacking)
        {
            movement.Set(speed * facingDir, rb.velocity.y);
            rb.velocity = movement;
            isWalking = true;
        }
        else
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y);
            isWalking = false;
        }
    }

    protected void FixedUpdate()
    {
        if (CheckPlayerAggro())
        {
            if(transform.position.x > target.transform.position.x && isFacingRight)
            {
                Flip();
                isFacingRight = false;
                facingDir = -1;
            }
            else if(transform.position.x < target.transform.position.x && !isFacingRight)
            {
                Flip();
                isFacingRight = true;
                facingDir = 1;
            }
        }

        ChangeWalkAnim();

        Attacking();
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

    protected bool CheckAttackRange()
    {
        return Physics2D.OverlapCircle(attackCheck.position, attackRange, playerMask);
    }

    protected void ChangeWalkAnim()
    {
        anim.SetBool("walk", isWalking);
        anim.SetBool("idle", !isWalking);
    }

    protected void Attacking()
    {
        if (CheckAttackRange())
        {
            StartCoroutine(StartAttacking());
        }
    }

    public IEnumerator StartAttacking()
    {
        rb.velocity = Vector2.zero;
        isWalking = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("attack", true);
        isAttacking = true;
    }

    public void ActivateAttack()
    {
        attack.SetActive(true);
    }

    protected void StopAttack()
    {
        anim.SetBool("attack", false);
        isAttacking = false;
        attack.GetComponent<EnemyDealDamage>().hasTriggered = false;
    }

    protected void StopCollider()
    {
        attack.SetActive(false);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerCheck.position, 7f);
        Gizmos.DrawWireSphere(attackCheck.position, 0.5f);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
