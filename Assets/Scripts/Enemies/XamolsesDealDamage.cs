using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XamolsesDealDamage : MonoBehaviour
{
    public Xamolses xa;

    public float damage;

    public bool hasTriggered;

    void Start()
    {
        hasTriggered = false;

        xa = GetComponentInParent<Xamolses>();  

        damage = xa.damage;
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
