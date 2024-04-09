using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        SpawnRandomEnemy();
    }

    private void SpawnRandomEnemy()
    {
        if (enemies.Count > 0)
        {
            int randomIndex = Random.Range(0, enemies.Count);

            Instantiate(enemies[randomIndex], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("There are no enemies in the list!");
        }
    }
}
