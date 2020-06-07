using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuoteLoadingScreen : MonoBehaviour
{
    public Text DYKText;

    public string didYouKnow;
    public string defaultKeySentence;
    public List<string> randomFunFact;

    // Start is called before the first frame update
    void Start()
    {
        RefreshText();
    }

    public virtual void RefreshText()
    {
        int randomNumber;

        randomNumber = Random.Range(0, randomFunFact.Count);
        string key = string.Concat(defaultKeySentence, randomNumber.ToString());

        if (string.IsNullOrEmpty(key)) return;

        string displayText = LanguageManager.instance.Translate(key);

        DYKText.text = displayText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
