using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuoteLoadingScreen : MonoBehaviour
{
    public Localisation didYouKnowKey;
    
    public string defaultKeySentence;
    public List<string> randomFunFact;
    
    void Start()
    {
        RandomText();
    }

    public void RandomText()
    {
        int randomNumber;

        randomNumber = Random.Range(0, randomFunFact.Count);
        string key = string.Concat(defaultKeySentence, randomNumber.ToString());

        Debug.Log(key);
        
        didYouKnowKey.key = key;
        didYouKnowKey.RefreshText();
    }
}
