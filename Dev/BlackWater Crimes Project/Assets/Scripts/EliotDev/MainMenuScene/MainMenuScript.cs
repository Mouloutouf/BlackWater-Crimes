using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public SoundSystem soundSystem;
    public LanguageSystem languageSystem;

    public DataContainer dataContainer;
    public GameData gameData;
    
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider voicesSlider;

    [SerializeField] Text musicValue;
    [SerializeField] Text voicesValue;

    [SerializeField] Text gameStatusText;

    [SerializeField] GameObject title;
    bool shouldFadeTitle = false;
    float titleAlpha = 1f;

    [SerializeField] GameObject parameters;
    [SerializeField] GameObject menuOptions;
    float menuOptionsAlpa = 0f;

    [SerializeField] GameObject bgDesktopImage;
    [SerializeField] GameObject bgDesktopImageEffect;
    
    public Dropdown languageSelection;

    public GameObject parameterButton;
    public Toggle tutoToggle;

    public Sprite baseParameter;
    public Sprite activeParameter;

    public Button playButton;
    public SceneLoaderSimple sceneLoader;
    private string sceneToLoad;

    private void Start()
    {
        gameStatusText.text = "Game 1 - " + DateTime.Today.ToString("M/d/yyyy");
        
        tutoToggle.isOn = gameData.firstTimeInTuto;

        SetGame();

        SetLanguage();
    }

    private void Update()
    {
        if((Input.touchCount > 0 && title.activeSelf) || (Input.GetMouseButtonDown(0) && title.activeSelf))
        {
            shouldFadeTitle = true;
        }
        
        if(shouldFadeTitle)
        {
            TitleFade();
        }
    }

    void TitleFade()
    {
        if(titleAlpha > -0.3f)
        {
            titleAlpha -= .025f;
            if(titleAlpha > 0)
            {
                Color tempColor = title.GetComponent<Image>().color;
                tempColor.a = titleAlpha;
                title.GetComponent<Image>().color = tempColor;
            }
        }
        else if(menuOptionsAlpa < 1f)
        {
            title.SetActive(false);
            menuOptions.SetActive(true);
            menuOptionsAlpa += .025f;
            Color tempColor = menuOptions.GetComponentInChildren<Image>().color;
            tempColor.a = menuOptionsAlpa;
            foreach(Image image in menuOptions.GetComponentsInChildren<Image>())
            {
                image.GetComponent<Image>().color = tempColor;
            }
        }
        else
        {       
            shouldFadeTitle = false;
        }
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateParameter(GameObject sender)
    {
        if (sender == musicSlider.gameObject)
        {
            musicValue.text = musicSlider.value.ToString();
            gameData.soundSettings.musicVolume.Volume = (musicSlider.value) / musicSlider.maxValue;
            soundSystem.SetVolume();
        }
        else if (sender == voicesSlider.gameObject)
        {
            voicesValue.text = voicesSlider.value.ToString();
            gameData.soundSettings.voiceVolume.Volume = (voicesSlider.value) / voicesSlider.maxValue;
            gameData.soundSettings.soundVolume.Volume = (voicesSlider.value) / voicesSlider.maxValue;

            soundSystem.SetVolume();
        }
    }

    void SetLanguage()
    {
        if (gameData.gameLanguage == Languages.French)
        {
            languageSelection.captionText.text = "Français";
            languageSelection.value = 0; // French is First
        }
        else if (gameData.gameLanguage == Languages.English)
        {
            languageSelection.captionText.text = "English";
            languageSelection.value = 1; // English is Second
        }
    }

    public void UpdateLanguage()
    {
        if (languageSelection.captionText.text == "English")
        {
            gameData.gameLanguage = Languages.English;
        }
        else if (languageSelection.captionText.text == "Français")
        {
            gameData.gameLanguage = Languages.French;
        }

        languageSystem.SetKeys();
    }

    public void Tutorial(bool enabled)
    {
        gameData.firstTimeInTuto = enabled;
    }

    public void OpenParameters()
    {
        if (!parameters.activeSelf)
        {
            parameters.SetActive(true);
            bgDesktopImage.SetActive(false);
            bgDesktopImageEffect.GetComponent<Image>().fillAmount = .8f;

            parameterButton.GetComponent<Image>().sprite = activeParameter;
        }
        else
        {
            parameters.SetActive(false);
            bgDesktopImage.SetActive(true);
            bgDesktopImageEffect.GetComponent<Image>().fillAmount = 1f;

            parameterButton.GetComponent<Image>().sprite = baseParameter;
        }
    }

    public void SetGame()
    {
        if (PlayerPrefs.GetString(nameof(gameData.playGame)) == "Play Game !")
        {
            LoadGame();
        }
        else
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        gameData.ResetPrefs();

        PlayerPrefs.SetString(nameof(gameData.playGame), "Play Game !");
        
        sceneToLoad = "ChooseEpisode";

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(delegate { sceneLoader.LoadScene(sceneToLoad); });

        gameData.ManageData(Action.Save);

        Debug.Log("This is a New Game !");
    }

    public void LoadGame()
    {
        gameData.ManageData(Action.Load);

        sceneToLoad = "MenuDeskScene";

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(delegate { sceneLoader.LoadScene(sceneToLoad); });

        gameData.ManageData(Action.Save);

        Debug.Log("The Current Game has been Loaded !");
    }
}
