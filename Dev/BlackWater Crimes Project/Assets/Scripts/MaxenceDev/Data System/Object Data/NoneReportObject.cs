using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoneReportObject : MonoBehaviour
{
    public Localisation nameComponent;
    public Localisation messageComponent;

    [HideInInspector] public string nameKey;
    [HideInInspector] public string messageKey;

    void Start()
    {
        nameComponent.key = nameKey;
        nameComponent.RefreshText();

        messageComponent.key = messageKey;
        messageComponent.RefreshText();
    }
}
