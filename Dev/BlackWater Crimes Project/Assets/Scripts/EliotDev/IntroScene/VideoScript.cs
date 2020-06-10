using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class VideoScript : SerializedMonoBehaviour
{
    public GameData gameData;
    
    public string nextSceneName;
    [SerializeField] SceneLoaderSimple sceneLoader;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioSource audioSource;

    public Dictionary<Languages, (VideoClip, AudioClip)> videos;
    
    void Start()
    {
        videoPlayer.clip = videos[gameData.gameLanguage].Item1; // Video

        audioSource.clip = videos[gameData.gameLanguage].Item2; // Audio

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
