using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public GameObject mainMenu;
    public GameObject settingsMenu;
	public void SetVolume(float vol)
    {
        Debug.Log(vol);
        audioMixer.SetFloat("MasterVol", vol);
    }

    public void ButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);

        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Fullscreen settings changed");
    }
}
