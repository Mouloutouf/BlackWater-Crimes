using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesPhotosLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
