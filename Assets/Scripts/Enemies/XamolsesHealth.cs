using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XamolsesHealth : MonoBehaviour
{
    public Xamolses xa;

    private float currentHealth;

    public float maxHealth;

    void Start()
    {
        maxHealth = xa.maxHealth;

        currentHealth = maxHealth;
    }

    public void LoseHealth(float ammount)
    {
        currentHealth -= ammount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("EndScene");
        }
    }
}
