using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class EvidenceObject : ObjectData<Evidence>
{
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

    [HideInInspector]
    public bool isZoomed { get; set; } = false;
    public bool canShowText { get; set; } = true;
    private bool isShown;

    private bool hit;

    void Start()
    {
        GetGameData();

        List<Evidence> myDataList = gameData.allEvidences[data.modeCategory.location];

        if (!instantiate) LoadDataOfType(myDataList);
    }

    public override void Protocol()
    {
        base.Protocol();
    }

    void Update()
    {
        /*if (isZoomed && canShowText && data.hasText)
        {
            DisplayText();
        }
    }

    void DisplayText()
    {
        if (Input.GetMouseButtonDown(0))
        {
            time = timer;

            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 2000f))
            {
                if (hit.transform.GetComponent<EvidenceObject>() == this || hit.transform.parent.GetComponent<EvidenceObject>() == this)
                {
                    this.hit = true;
                }
            }
        }

        time -= Time.deltaTime;

        if (hit && Input.GetMouseButtonUp(0) && time > 0)
        {
            if (isShown)
            {
                displayTextComponent.transform.parent.gameObject.SetActive(false);

                isShown = false;
            }
            else
            {
                displayTextComponent.text = data.descriptionText;
                displayTextComponent.transform.parent.gameObject.SetActive(true);

                isShown = true;
            }
        }*/
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
            displayTextComponent.text = data.descriptionText;
            displayTextComponent.transform.parent.gameObject.SetActive(true);

            isShown = true;
        }
    }
}
