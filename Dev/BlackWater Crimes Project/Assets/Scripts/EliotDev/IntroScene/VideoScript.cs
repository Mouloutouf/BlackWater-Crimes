using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    public string nextSceneName;
    [SerializeField] SceneLoaderSimple sceneLoader;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        videoPlayer.Prepare();
        StartCoroutine(WaitForPrepare());
    }

    IEnumerator WaitForPrepare()
    {
        if(videoPlayer.isPrepared == false)
        {
            yield return new WaitForSeconds(.1f);
            StartCoroutine(WaitForPrepare());
        }
        else
        {
            videoPlayer.Play();
            audioSource.Play();
            videoPlayer.loopPointReached += CheckOver;
        }
    }
    void CheckOver(VideoPlayer vp)
    {
        sceneLoader.WithLoadingScreen(true);
        sceneLoader.LoadScene(nextSceneName);
    }

    public void Skip()
    {
        sceneLoader.WithLoadingScreen(true);
        sceneLoader.LoadScene(nextSceneName);
    }
}
