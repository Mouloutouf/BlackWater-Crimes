using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class EvidenceObject : ObjectData<Evidence>
{
    public int MyIndex { get { return data.index; } set => data.index = value; }

    [Title("PROPERTIES")]
    
    // Text \\
    public bool hasText;
    [ShowIf("hasText")]
    public Text displayTextComponent;
    [ShowIf("hasText")]
    public float timer = 0.3f;
    private float time;
    [ShowIf("hasText")]
    public Camera _cam;

    public bool hasIntel { get { return data.hasIntels; } }
    [ShowIf("hasIntel")]
    public List<IntelObject> intelObjects;

    [HideInInspector]
    public bool isZoomed { get; set; } = false;
    public bool canShowText { get; set; } = true;
    public bool isShown;

    private bool hit;

    void Start()
    {
        GetGameData();

        List<Evidence> myDataList = gameData.evidences[data.modeCategory.location];

        LoadDataOfType(myDataList);
    }

    public override void Protocol()
    {
        if (hasIntel)
        {
            for (int i = 0; i < intelObjects.Count; i++)
            {
                intelObjects[i].myIntelKey = data.intels[i].intelKey;
            }
        }
        
        base.Protocol();
    }

    public void ShowText()
    {
        if (isShown)
        {
            displayTextComponent.transform.parent.gameObject.SetActive(false);

            isShown = false;
        }
        else
        {
            displayTextComponent.gameObject.GetComponent<Localisation>().key = data.textKey;
            displayTextComponent.gameObject.GetComponent<Localisation>().RefreshText();
            displayTextComponent.transform.parent.gameObject.SetActive(true);

            isShown = true;
        }
    }
}
