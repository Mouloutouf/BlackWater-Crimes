using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] string demoExplanationsSceneName;

    void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += CheckOver;
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
