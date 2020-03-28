using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteClick : MonoBehaviour
{
    public GameObject UICanvas;

    public float offset;

    private bool visible = false;

    public void ShowNotesMenu()
    {
        float ofst = 0;

        if (!visible)
        {
            ofst = offset;

            visible = true;
        }

        else if (visible)
        {
            ofst = -offset;

            visible = false;
        }

        foreach (Transform tr in UICanvas.transform)
        {
            Vector2 vec = new Vector2(tr.GetComponent<RectTransform>().anchoredPosition.x, tr.GetComponent<RectTransform>().anchoredPosition.y + ofst);

            tr.GetComponent<RectTransform>().anchoredPosition = vec;
        }
    }
}
