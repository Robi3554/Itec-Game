using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    private GameObject player;

    public GameObject deathMenu;

    private void Update()
    {
        player = GameObject.Find("Player");

        if(player == null)
        {
            deathMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
