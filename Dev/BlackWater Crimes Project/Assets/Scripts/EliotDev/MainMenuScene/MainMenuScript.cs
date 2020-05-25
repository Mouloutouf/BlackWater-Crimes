﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public SoundSystem soundSystem;

    public DataContainer dataContainer;
    private GameData gameData;
    
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

    public Sprite baseParameter;
    public Sprite activeParameter;

    private void Start()
    {
        gameStatusText.text = "Game 1 - " + DateTime.Today.ToString("M/d/yyyy");

        if (dataContainer != null) gameData = dataContainer.gameData;

        gameData.ManageData(Action.Load);

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
}
