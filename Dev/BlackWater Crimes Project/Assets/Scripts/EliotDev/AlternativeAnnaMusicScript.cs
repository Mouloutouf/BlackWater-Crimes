using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeAnnaMusicScript : MonoBehaviour
{
    public GameObject incriminatoryClue;
    public float fadeSpeed;
    public AudioSource classicAudio;
    public AudioSource alternativeAudio;
    public EvidenceInteraction evidenceInteraction;
    public GameData gameData;
    float classicTargetVolume;
    float alternativeTargetVolume;
    float maxVolume;
    bool hasChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        SetMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (evidenceInteraction.currentEvidenceHeld == incriminatoryClue && !hasChanged)
        {
            gameData.alternativeAnnaMusic = true;
            SetMusic();
        }
    }

    public void SetMusic()
    {
        if(gameData.alternativeAnnaMusic)
        {
            hasChanged = true;
            maxVolume = gameData.soundSettings.musicVolume.Volume;
            classicTargetVolume = classicAudio.volume;
            StartCoroutine(FadeMusic());
        }
    }

    IEnumerator FadeMusic()
    {
        if(classicAudio.volume != 0 || alternativeAudio.volume != maxVolume)
        {
            if (classicAudio.volume > 0)
            {
                classicTargetVolume -= fadeSpeed;
                classicAudio.volume = classicTargetVolume;
                if (alternativeAudio.volume < 0)
                {
                    alternativeAudio.volume = 0;
                }
            }

            if (alternativeAudio.volume < maxVolume)
            {
                alternativeTargetVolume += fadeSpeed;
                alternativeAudio.volume = alternativeTargetVolume;
                if (alternativeAudio.volume > maxVolume)
                {
                    alternativeAudio.volume = maxVolume;
                }
            }

            yield return new WaitForSeconds(.1f);
            StartCoroutine(FadeMusic());
        }
    }
}
