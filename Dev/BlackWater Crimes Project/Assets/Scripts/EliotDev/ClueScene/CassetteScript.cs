using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteScript : MonoBehaviour
{
    public SoundSystem soundSystem;
    public AudioClip cassetteAudioClip;

    public void PlayAudio()
    {
        soundSystem.PlayVoice(cassetteAudioClip);
    }
}
