using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportsManager : MonoBehaviour
{
    private int reportIndex;

    private bool start = true;

    void Update()
    {
        /*
        if (start)
        {
            if (transform.childCount > 2) // if there are instantiated reports
            {
                GameObject noReport = transform.GetChild(1).gameObject; // Saves the No Report object for destruction
                Destroy(noReport); // Destroys it;
            }

            start = false;
        }
        */
    }

    /*
     * for (int i = 2; i < transform.childCount; i++)
                {
                    reportIndex = transform.GetChild(i).GetComponent<ReportObject>().data.unlockIndex;
                }
        foreach (Transform tr in transform)
                {
                    tr.SetSiblingIndex(reportIndex);
                }
    */
}
