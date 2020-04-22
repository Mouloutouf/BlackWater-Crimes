using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    private bool isSetAtPage;

    public int pageLocation { get; set; }

    void OnTriggerStay2D(Collider2D other)
    {
        //if (other.GetComponent<PhotoObject>() == null) return;

        DragWithSnap dragScript = other.transform.parent.GetComponent<DragWithSnap>();
        Transform parentObject = other.transform.parent.parent;

        if (!dragScript.isHeld)
        {
            if (!isSetAtPage)
            {
                // set the photo's transform to the right page in hierarchy (either page 1 or page 2)
                //parentObject.transform.SetParent(this.transform.parent.parent);
                parentObject.GetComponent<PhotoObject>().pageNumber = pageLocation;
                isSetAtPage = true;
            }
                
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

        if (dragScript.isHeld) isSetAtPage = false;
    }
}
