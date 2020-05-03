using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public SoundSystem soundSystem;
    public DataContainer dataContainer;
    private GameData gameData;

    [SerializeField] string introSceneName;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider voicesSlider;
    [SerializeField] Text musicValue;
    [SerializeField] Text voicesValue;
    [SerializeField] Text gameStatusText;
    [SerializeField] GameObject parameters;
    [SerializeField] GameObject bgDesktopImage;
    [SerializeField] GameObject title;
    [SerializeField] GameObject menuOptions;
    bool shouldFadeTitle = false;
    float titleAlpha = 1f;
    float menuOptionsAlpa = 0f;
    public Dropdown languageSelection;

    private void Start() 
    {
        gameStatusText.text = "Game 1 - " + DateTime.Today.ToString("M/d/yyyy");

        if (dataContainer != null) gameData = dataContainer.gameData;
    }

    private void Update() 
    {
        if((Input.touchCount > 0 && title.activeSelf) && (Input.GetMouseButtonDown(0) && title.activeSelf))
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
            titleAlpha -= .005f;
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
            menuOptionsAlpa += .008f;
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

    public void UpdateLanguage()
    {
        if (languageSelection.value == 0)
        {
            gameData.gameLanguage = Languages.English;
        }
        else if (languageSelection.value == 1)
        {
            gameData.gameLanguage = Languages.French;
        }
    }

    public void OpenParameters()
    {
        if (!parameters.activeSelf)
        {
            parameters.SetActive(true);
            bgDesktopImage.SetActive(false);
        }
        else
        {
            parameters.SetActive(false);
            bgDesktopImage.SetActive(true);
        }
    }
}
