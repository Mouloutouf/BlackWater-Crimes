using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    string sceneToLoad;
    public Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = Object.FindObjectOfType<LoadingSceneScript>().GetComponent<LoadingSceneScript>().sceneToLoad;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        Destroy(Object.FindObjectOfType<LoadingSceneScript>().gameObject);
        AsyncOperation asyncLoad =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            progressBar.value = progress;
            yield return null;
        }
    }
}
