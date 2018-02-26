using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class gameOverScript : MonoBehaviour {
    public GameObject GameOverScreen;
    public GameObject gameUI;
    public GameObject player;
    public int Health;
    
    // Update is called once per frame
    void Update()
    {
        Health = player.GetComponent<Shootable>().currentHealth;
        
        if (Health <= 0)
        {
            GameOverScreen.SetActive(true);
            gameUI.SetActive(false);
        };
    }


    public void ReloadLevel()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
