using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] string demoExplanationsSceneName;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(demoExplanationsSceneName, LoadSceneMode.Single);
    }

    public void Skip()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(demoExplanationsSceneName, LoadSceneMode.Single);
    }
}
