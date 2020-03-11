using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPosition : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.GetComponent<PhotoObject>() == null) return;

        DragWithSnap drag = other.transform.parent.GetComponent<DragWithSnap>();
        Transform imageObject = other.transform.parent.parent.GetChild(2);

        if (drag.isHeld)
        {
            Debug.Log("askip je fonctionne");
            imageObject.GetComponent<RectTransform>().position = this.transform.parent.GetComponent<RectTransform>().position;
            drag.isSnapped = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.GetComponent<PhotoObject>() == null) return;

        other.transform.parent.GetComponent<DragWithSnap>().ResetAllPos();
        other.transform.parent.GetComponent<DragWithSnap>().isSnapped = false;
    }
}
