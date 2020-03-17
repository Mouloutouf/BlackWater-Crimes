using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhotosDisplayer : MonoBehaviour
{
    public Content content;

    [SerializeField] string sceneName;
    [SerializeField] GameObject[] imagePlaceHolder;
    string[] files = null;

    List<Sprite> photoSprites = new List<Sprite>();
    
    void Start()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        if (files.Length > 0)
        {
            GetPictureAndShowIt();
        }
    }

    void GetPictureAndShowIt()
    {
        int index = 0;

        foreach (string file in files)
        {
            Texture2D texture = GetScreenshotTexture(files[index]);
            photoSprites.Add(Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
            imagePlaceHolder[index].GetComponent<Image>().sprite = photoSprites[index];

            // Saves each Photo into the Evidence instance of each Evidence Objects
            foreach (Transform tr in content.contentObject.transform)
            {
                if ((tr.GetComponent<EvidenceObject>().data.code + ".png") == files[index])
                {
                    tr.GetComponent<EvidenceObject>().data.intel = photoSprites[index];
                }
            }

            index += 1;
        }
    }

    Texture2D GetScreenshotTexture(string filePath)
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
}
