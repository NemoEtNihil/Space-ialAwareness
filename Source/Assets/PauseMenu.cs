using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject gameUI;

    void Start()
    {
        Resume();

    }
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.Joystick1Button7)))
            {
             if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

             if (Input.GetKeyDown(KeyCode.Joystick1Button1) && GameIsPaused)
            {
                Resume();
            }
            }
	}

   public void Resume()
    {
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && GameIsPaused)
        {
            Resume();
        }
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
