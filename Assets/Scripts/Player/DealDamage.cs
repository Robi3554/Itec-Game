using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private float damage;

    private PlayerController pc;

    public bool hasTriggered;

    private void Start()
    {
        hasTriggered = false;

        pc = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        damage = pc.GetDamage();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if(!hasTriggered)
            {
                col.GetComponent<EnemyHealth>().LoseHealth(damage);
                gameObject.SetActive(false);
                hasTriggered = true;
            }
        }
        else if (col.CompareTag("Xamolses"))
        {
            if (!hasTriggered)
            {
                col.GetComponent<XamolsesHealth>().LoseHealth(damage);
                gameObject.SetActive(false);
                hasTriggered = true;
            }
        }
    }
}
