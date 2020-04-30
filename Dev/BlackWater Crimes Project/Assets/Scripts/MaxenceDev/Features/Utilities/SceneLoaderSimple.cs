using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSimple : MonoBehaviour
{
    public delegate void Delegate();
    public Delegate methodToCall;
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
            methodToCall = LoadingScene;
            StartCoroutine(WaitForFrame(methodToCall));
        }
    }

    void LoadingScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScreenScene", LoadSceneMode.Single);
    }

    public void LoadScene(string name)
    {
        sceneName = name;
        methodToCall = LoadScene;
        StartCoroutine(WaitForFrame(methodToCall));
    }

    public void WithLoadingScreen(bool addLoadingScreen)
    {
        withLoadingScreen = addLoadingScreen;
    }

    IEnumerator WaitForFrame(Delegate method)
    {
        yield return new WaitForEndOfFrame();
        method();
    }
}
