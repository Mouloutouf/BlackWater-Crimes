﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] string introSceneName;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider voicesSlider;
    [SerializeField] Text musicValue;
    [SerializeField] Text voicesValue;
    [SerializeField] Text gameStatusText;

    private void Start() 
    {
        gameStatusText.text = "Game 1 - " + DateTime.Today.ToString("M/d/yyyy");
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(introSceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateText(GameObject sender)
    {
        if(sender == musicSlider.gameObject)
        {
            musicValue.text = musicSlider.value.ToString();
        }
        else if(sender == voicesSlider.gameObject)
        {
            voicesValue.text = voicesSlider.value.ToString();
        }
    }   
}
