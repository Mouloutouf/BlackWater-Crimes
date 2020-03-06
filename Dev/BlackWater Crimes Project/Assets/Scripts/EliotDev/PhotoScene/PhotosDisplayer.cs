using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhotosDisplayer : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject[] imagePlaceHolder;
    string[] files = null;

    // Start is called before the first frame update
    void Start()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        if(files.Length > 0)
        {
            GetPictureAndShowIt();
        }
    }

    void GetPictureAndShowIt()
    {
        int index = 0;
        foreach(string file in files)
        {
            Texture2D texture = GetScreenshotImage(files[index]);
            Sprite sp = Sprite.Create (texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            imagePlaceHolder[index].GetComponent<Image>().sprite = sp;
            index += 1;
        }
    }

    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    public void QuitScene()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("NotesPhotosSceneProto");
    }

    public void LoadNoteScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
