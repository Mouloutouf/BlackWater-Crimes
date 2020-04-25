using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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

    public Dropdown languageSelection;

    private void Start() 
    {
        gameStatusText.text = "Game 1 - " + DateTime.Today.ToString("M/d/yyyy");

        if (dataContainer != null) gameData = dataContainer.gameData;
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
}
