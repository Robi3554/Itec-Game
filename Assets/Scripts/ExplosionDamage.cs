using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    private Xamolses xa;

    private BoxCollider2D bc;

    private float damage;

    private bool hasTriggered;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();

        xa = FindObjectOfType<Xamolses>(); 
        
        damage = xa.damage;

        hasTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                collision.GetComponent<PlayerController>().TakeDamage(damage);
                hasTriggered = true;
            }
        }
    }
}
