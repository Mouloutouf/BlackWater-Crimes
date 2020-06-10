using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesRecommandationScript : MonoBehaviour
{
    public float timeToWait;
    public string nextScene;
    public SceneLoaderSimple sceneLoader;

    void Start()
    {
        StartCoroutine(WaitToNextScene());
    }

    IEnumerator WaitToNextScene()
    {
        yield return new WaitForSeconds(timeToWait);
        sceneLoader.LoadScene(nextScene);
    }
}
