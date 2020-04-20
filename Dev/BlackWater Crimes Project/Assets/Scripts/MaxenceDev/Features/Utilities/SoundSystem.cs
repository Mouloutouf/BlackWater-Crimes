using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public GameData gameData;

    public AudioSource musicAudio;
    public AudioSource soundAudio;
    public AudioSource voiceAudio;
    
    void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        musicAudio.volume = gameData.soundSettings.musicVolume.Volume;
        soundAudio.volume = gameData.soundSettings.soundVolume.Volume;
        voiceAudio.volume = gameData.soundSettings.voiceVolume.Volume;
    }
}
