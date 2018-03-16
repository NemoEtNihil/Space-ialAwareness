using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialLoader : MonoBehaviour
{
    public GameObject loadingSlider;
    public Slider slider;
    public float progress;
    public AsyncOperation operation;
    public GameObject Button;

    public void ButtonPressed()
    {
        
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        Button.SetActive(false);
        loadingSlider.SetActive(true);
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }

    public void Update()
    {
        progress = Mathf.Clamp01(operation.progress / .9f);
        slider.value = progress;
    }
}