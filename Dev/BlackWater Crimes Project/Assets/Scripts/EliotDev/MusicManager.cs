using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class MusicManager : SerializedMonoBehaviour
{
    [Title ("Music References")]
    public Dictionary<Locations, AudioClip> researchSceneMusics = new Dictionary<Locations, AudioClip>();
    public AudioClip deskMusic;
    public AudioClip interrogationMusic;
    public AudioClip prosecutionMusic;
    public AudioClip mainMenuMusic;


    [Title ("Scene References")]
    public Dictionary<Locations,string> researchSceneNames = new Dictionary<Locations, string>();
    public List<string> deskSceneNames = new List<string>();
    public string interrogationSceneName;
    public string prosecutionSceneName;


    SoundSystem soundSystem;
    AudioSource audioSource;
    GameObject audioSourceGO;
    Locations locationKey;

    void Start()
    {
        MusicManager musicManagerCopy = GameObject.FindObjectOfType<MusicManager>();
        if(musicManagerCopy != null && musicManagerCopy != this) Destroy(musicManagerCopy.gameObject);

        DontDestroyOnLoad(this.gameObject);

        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnNewScene;
    }

    void OnNewScene(Scene oldScene, Scene newScene)
    {
        soundSystem = GameObject.FindObjectOfType<SoundSystem>();
        audioSource = soundSystem.transform.GetChild(0).GetComponent<AudioSource>();

        if (this.transform.childCount > 0 && !deskSceneNames.Contains(newScene.name)) Destroy(this.transform.GetChild(0).gameObject);

        if (researchSceneNames.ContainsValue(newScene.name)) LoadResearchSceneMusic(newScene.name);
        else if (deskSceneNames.Contains(newScene.name)) LoadDeskSceneMusic(newScene.name);
        else if (newScene.name == interrogationSceneName) LoadInterrogationSceneMusic();
        else if (newScene.name == prosecutionSceneName) LoadProsecutionSceneMusic();
    }  

    void LoadResearchSceneMusic(string sceneName)
    {
        foreach(KeyValuePair<Locations, string> pair in researchSceneNames)
        {
            if(sceneName == pair.Value)
            {
                locationKey = pair.Key;
            }
        }

        AudioClip musicToPlay = researchSceneMusics[locationKey];

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Scene_PlanqueAnna")
        {
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }
    }

    void LoadDeskSceneMusic(string sceneName)
    {
        if (this.transform.childCount == 0)
        {
            audioSourceGO = GameObject.FindObjectOfType<SoundSystem>().transform.GetChild(0).GetComponent<AudioSource>().gameObject;
            GameObject audioSourceClone =  Instantiate(audioSourceGO, this.transform);
            
            audioSourceClone.GetComponent<AudioSource>().clip = deskMusic;
            audioSourceClone.GetComponent<AudioSource>().Play();
        }

        audioSource.Stop();
    }

    void LoadInterrogationSceneMusic()
    {
        audioSource.clip = interrogationMusic;
        audioSource.Play();
    }

    void LoadProsecutionSceneMusic()
    {
        audioSource.clip = prosecutionMusic;
        audioSource.Play();
    }
}
