using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailScript : MonoBehaviour
{
    [SerializeField] Dropdown detailDropdown; 
    [SerializeField] List<string> eyeColorOptions;
    [SerializeField] List<string> facialHairOptions;
    [SerializeField] List<string> hairColorOptions;
    [SerializeField] List<string> particularPhysicalTraitOptions;
    public List<string> categories = new List<string>();
    bool firstClick = true;

    private void Start() 
    {
        GetComponent<Dropdown>().enabled = false;
        GetComponent<Dropdown>().captionText.GetComponent<Text>().fontStyle = FontStyle.Italic;
        Color tempColor = GetComponent<Dropdown>().captionText.GetComponent<Text>().color;
        tempColor.a = .7f;
        GetComponent<Dropdown>().captionText.GetComponent<Text>().color = tempColor;
    }

    public void UpdateDetailDropdown()
    {
        int category = GetComponent<Dropdown>().value;
        switch(category)
        {
            case 0:
                detailDropdown.ClearOptions();
                detailDropdown.AddOptions(eyeColorOptions);
                return;
            
            case 1:
                detailDropdown.ClearOptions();
                detailDropdown.AddOptions(facialHairOptions);
                return;

            case 2:
                detailDropdown.ClearOptions();
                detailDropdown.AddOptions(hairColorOptions);
                return;

            case 3:
                detailDropdown.ClearOptions();
                detailDropdown.AddOptions(particularPhysicalTraitOptions);
                return;
        }
    }

    public void ErasePlaceHolderText()
    {
        if(firstClick)
        {
            firstClick = false;

            GetComponent<Dropdown>().enabled = true;
            GetComponent<Dropdown>().captionText.GetComponent<Text>().fontStyle = FontStyle.Normal;
            Color tempColor = GetComponent<Dropdown>().captionText.GetComponent<Text>().color;
            tempColor.a = 1f;
            GetComponent<Dropdown>().captionText.GetComponent<Text>().color = tempColor;

            GetComponent<Dropdown>().ClearOptions();
            GetComponent<Dropdown>().AddOptions(categories);
            GetComponent<Dropdown>().RefreshShownValue();
            GetComponent<Dropdown>().Show();

            detailDropdown.gameObject.SetActive(true);
            detailDropdown.RefreshShownValue();
        }
    }
}
