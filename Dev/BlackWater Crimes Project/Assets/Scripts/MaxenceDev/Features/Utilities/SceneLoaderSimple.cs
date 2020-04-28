using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSimple : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] LoadSceneMode loadSceneMode;
    [SerializeField] bool withLoadingScreen;
    [SerializeField] GameObject loadingScreenPrefab;

    public void LoadScene()
    {
        if(withLoadingScreen == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, loadSceneMode);
        }
        else
        {
            GameObject tempPrefab = Instantiate(loadingScreenPrefab);
            tempPrefab.GetComponent<LoadingSceneScript>().sceneToLoad = sceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScreenScene", LoadSceneMode.Single);
        }
    }

    public void LoadScene(string name)
    {
        if(withLoadingScreen == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name, loadSceneMode);
        }
        else
        {
            GameObject tempPrefab = Instantiate(loadingScreenPrefab);
            tempPrefab.GetComponent<LoadingSceneScript>().sceneToLoad = name;
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScreenScene", LoadSceneMode.Single);
        }
    }
}
