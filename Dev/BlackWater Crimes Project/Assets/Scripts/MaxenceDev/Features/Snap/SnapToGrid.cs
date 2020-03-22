using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        //if (other.GetComponent<PhotoObject>() == null) return;

        DragWithSnap drag = other.transform.parent.GetComponent<DragWithSnap>();
        Transform parentObject = other.transform.parent.parent;

        if (!drag.isHeld)
        {
            parentObject.transform.SetParent(this.transform.parent.parent);
            parentObject.GetComponent<RectTransform>().anchoredPosition = this.transform.parent.GetComponent<RectTransform>().anchoredPosition;

            foreach(Transform tr in parentObject)
            {
                tr.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                foreach (Transform _tr in tr)
                {
                    _tr.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            } 
        }
    }
}
