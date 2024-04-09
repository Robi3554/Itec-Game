using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public List<GameObject> powerUps = new List<GameObject>();

    private float currentHealth;

    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void LoseHealth(float ammount)
    {
        currentHealth -= ammount;

        if(currentHealth <= 0)
        {
            DropPowerUp();

            Destroy(gameObject);
        }
    }

    public void DropPowerUp()
    {
        int randomIndex = Random.Range(0, powerUps.Count);

        Debug.Log(randomIndex);

        Instantiate(powerUps[randomIndex], transform.position, transform.rotation);
    }
}
