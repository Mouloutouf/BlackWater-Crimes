using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = Object.FindObjectOfType<LoadingSceneScript>().GetComponent<LoadingSceneScript>().sceneToLoad;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(5);
        AsyncOperation asyncLoad =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
