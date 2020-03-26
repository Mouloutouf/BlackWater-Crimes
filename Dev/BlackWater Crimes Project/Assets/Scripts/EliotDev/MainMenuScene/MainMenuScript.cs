using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] string introSceneName;

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(introSceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
