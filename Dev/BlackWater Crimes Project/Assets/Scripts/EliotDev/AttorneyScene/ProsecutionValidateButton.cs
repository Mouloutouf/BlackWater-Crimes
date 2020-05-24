using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProsecutionValidateButton : MonoBehaviour
{
    [SerializeField] GameObject disclaimer;
    public void Validate()
    {
        if(disclaimer.activeSelf == false)
        {
            disclaimer.SetActive(true);
            GetComponentInChildren<Text>().text = "Confirm";
        }
    }

    public void Reset()
    {
        disclaimer.SetActive(false);
        GetComponentInChildren<Text>().text = "Engage a prosecution";
    }
}
