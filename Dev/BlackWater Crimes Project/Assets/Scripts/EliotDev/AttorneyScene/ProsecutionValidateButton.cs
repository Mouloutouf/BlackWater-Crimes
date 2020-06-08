using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProsecutionValidateButton : MonoBehaviour
{
    public GameData gameData;
    
    public Text inputText;
    
    public string prosecutionSceneName;

    public void Validate()
    {
        foreach (Character character in gameData.characters)
        {
            if (character.isSuspect && inputText.text == character.name)
            {
                gameData.currentSuspect = character.suspect;

                StartCoroutine(DelayToProsecution(2.0f));
            }
        }
    }

    public void Reset()
    {
        inputText.GetComponentInParent<InputField>().text = "";
        inputText.color = Color.black;
    }

    IEnumerator DelayToProsecution(float time)
    {
        yield return new WaitForSeconds(time);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(prosecutionSceneName, LoadSceneMode.Single);
    }
}
