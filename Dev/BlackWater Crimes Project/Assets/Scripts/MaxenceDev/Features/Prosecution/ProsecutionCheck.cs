using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProsecutionCheck : MonoBehaviour
{
    public GameData gameData;

    public Character suspect { get; set; }

    public Localisation suspectName;
    public Localisation suspectJob;
    public Image suspectImage;
    public float factor;

    public GameObject validateButton;
    public string missingKey;

    public FileDisplayer fileDisplayer;
    
    public string endSceneName;
    
    void Start()
    {
        SetSuspect();
    }

    void SetSuspect()
    {
        foreach (Character character in gameData.characters)
        {
            if (character.isSuspect && character.suspect == gameData.currentSuspect)
            {
                suspect = character;

                suspectImage.sprite = character.sprite;
                suspectImage.SetNativeSize();
                RectTransform rect = suspectImage.gameObject.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(rect.rect.width / factor, rect.rect.height / factor);

                suspectJob.key = character.jobKey;
                suspectJob.RefreshText();

                suspectName.key = character.nameKey;
                suspectName.RefreshText();
            }
        }
    }

    public void Validate()
    {
        Check();

        StartCoroutine(EndThisSuspectLife(2.0f));
    }

    void Check()
    {
        foreach (FileCategory category in fileDisplayer.currentFilesDisplayed.Keys)
        {
            bool match = false;

            foreach (Incriminate incriminate in suspect.incriminates)
            {
                if (fileDisplayer.currentFilesDisplayed[category].GetComponent<FileObject>().codeName == incriminate.elementName)
                {
                    if (category == incriminate.category) match = true;
                    // Bravo, you checked an element that is in the incriminate elements of the suspect and put it in the right receiver
                }
            }

            if (match)
            {

            }
            else
            {

            }
        }
    }
    
    IEnumerator EndThisSuspectLife(float time)
    {
        yield return new WaitForSeconds(time);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(endSceneName, LoadSceneMode.Single);
    }
}
