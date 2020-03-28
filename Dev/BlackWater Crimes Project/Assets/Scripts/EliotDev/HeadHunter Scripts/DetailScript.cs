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
}
