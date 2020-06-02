using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProsecutionValidateButton : MonoBehaviour
{
    [SerializeField] GameObject disclaimer;

    public string confirmKey;
    public string engageKey;

    public void Validate()
    {
        if(disclaimer.activeSelf == false)
        {
            disclaimer.SetActive(true);
            GetComponentInChildren<Localisation>().key = confirmKey;
            GetComponentInChildren<Localisation>().RefreshText();
        }
    }

    public void Reset()
    {
        disclaimer.SetActive(false);
        GetComponentInChildren<Localisation>().key = engageKey;
        GetComponentInChildren<Localisation>().RefreshText();
    }
}
