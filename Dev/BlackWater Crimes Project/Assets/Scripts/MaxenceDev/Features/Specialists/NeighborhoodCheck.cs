using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborhoodCheck : Checker
{
    [SerializeField] Dropdown dropdown;

    public override void SendEvent()
    {
        GetCheckedElements();

        Send();
    }

    public override void GetCheckedElements()
    {
        foreach (Location location in gameData.locations)
        {
            if (dropdown.GetComponentInChildren<Text>().text == location.locationAdress)
            {
                checkedName = location.locationAdress;

                checkedImage = location.locationCroppedImage;
            }
        }
    }

    public override void ResetField()
    {
        base.ResetField();

        dropdown.value = 0;
    }
}
