using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformControl : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D playerCol;

    private GameObject currentOneWayPlatform;
    
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCol());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    public IEnumerator DisableCol()
    {
        CompositeCollider2D platCol = currentOneWayPlatform.GetComponent<CompositeCollider2D>();

        Physics2D.IgnoreCollision(playerCol, platCol);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCol, platCol, false);
    }
}
