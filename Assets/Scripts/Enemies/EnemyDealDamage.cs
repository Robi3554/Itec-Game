using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public int damage;

    public bool hasTriggered;

    void Start()
    {
        hasTriggered = false;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                col.GetComponent<PlayerController>().TakeDamage(damage);
                hasTriggered = true;
            }
        }
    }
}
