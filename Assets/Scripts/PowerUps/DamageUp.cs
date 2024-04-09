using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{
    private BoxCollider2D bc;

    public PlayerController playerController;

    public GameObject effect;

    public float addDamage;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        bc = GetComponent<BoxCollider2D>();

        StartCoroutine(StartUp());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        Instantiate(effect, transform.position, transform.rotation);

        playerController.playerDamage += addDamage;

        Destroy(gameObject);
    }

    public IEnumerator StartUp()
    {
        yield return new WaitForSeconds(1.5f);

        bc.enabled = true;
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
