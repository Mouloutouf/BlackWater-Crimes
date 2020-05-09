using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedReportsManager : MonoBehaviour
{
    public GameData gameData;

    public Transform failedContent;

    private bool start = true;

    void Update()
    {
        if (start)
        {
            if (failedContent.childCount != 0) failedContent.gameObject.SetActive(true);
            
            start = false;
        }

        if (failedContent.childCount == 0 && failedContent.gameObject.activeSelf) failedContent.gameObject.SetActive(false);
    }

    public void CloseFailedReport(GameObject report)
    {
        report.GetComponent<ReportObject>().data.unlockedData = false;
        
        Destroy(report);
    }

    public void ClearAllFailedReports()
    {
        foreach (List<Report> reports in gameData.allReports.Values)
        {
            foreach (Report report in reports)
            {
                if (report.failed && report.index != 0)
                {
                    reports.Remove(report);
                }
            }
        }
    }

    #region zucc
    /*
     * for (int i = 2; i < transform.childCount; i++)
                {
                    reportIndex = transform.GetChild(i).GetComponent<ReportObject>().data.unlockIndex;
                }
        foreach (Transform tr in transform)
                {
                    tr.SetSiblingIndex(reportIndex);
                }
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
    #endregion
}
