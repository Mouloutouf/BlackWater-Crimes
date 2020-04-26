using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class EvidenceObject : ObjectData<Evidence>
{
    private Evidence myType = new Evidence();

    [Title("PROPERTIES")]

    // Text \\
    public bool hasText;
    [ShowIf("hasText")]
    public Text displayTextComponent;
    [ShowIf("hasText")]
    [Title("Description Text", bold: false, HorizontalLine = false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string descriptionText;

    [HideInInspector]
    public bool isZoomed;
    private bool isShown;

    public float timer = 0.3f;
    private float time;
    
    void Start()
    {
        GetGameData();

        List<Evidence> myDataList = gameData.allEvidences[data.modeCategory.location];

        if (!instantiate) LoadDataOfType(myType, myDataList);
    }

    public override void Protocol()
    {
        base.Protocol();
    }

    void Update()
    {
        if (isZoomed && hasText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                time = timer;
            }

            time -= Time.deltaTime;

            if (Input.GetMouseButtonUp(0) && time > 0)
            {
                if (isShown)
                {
                    displayTextComponent.transform.parent.gameObject.SetActive(false);

                    isShown = false;
                }
                else
                {
                    displayTextComponent.text = descriptionText;
                    displayTextComponent.transform.parent.gameObject.SetActive(true);

                    isShown = true;
                }
            }
        }
    }
}
