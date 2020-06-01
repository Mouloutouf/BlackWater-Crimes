using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuoteLoadingScreen : MonoBehaviour
{
    public Text DYKText;

    public List<string> randomFunFact;

    // Start is called before the first frame update
    void Start()
    {
        int randomNumber;

        randomNumber = Random.Range(0, randomFunFact.Count);

        DYKText.text = "Le Saviez-vous : " + randomFunFact[randomNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
