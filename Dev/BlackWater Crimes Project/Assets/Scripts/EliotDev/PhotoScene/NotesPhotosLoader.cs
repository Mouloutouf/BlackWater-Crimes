using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotesPhotosLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
