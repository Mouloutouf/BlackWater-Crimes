using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistCheck : Checker
{
    [SerializeField] SpecialistEvidenceDisplayer specialistEvidenceDisplayer;

    private Evidence EvidenceHeld { get { return specialistEvidenceDisplayer.currentEvidenceDisplayed.GetComponent<PhotoSpecialistObject>().data; } }

    public override void SendEvent()
    {
        Send(EvidenceHeld.intels);
    }

    public override void GetCheckedElements()
    {
        checkedName = EvidenceHeld.codeName;

        checkedImage = EvidenceHeld.photo;
    }

    public override void ResetField()
    {
        base.ResetField();

        specialistEvidenceDisplayer.ResetClue();

        validateButton.interactable = false;
        validateButton.GetComponentInChildren<Text>().text = "Missing elements";
    }
}
