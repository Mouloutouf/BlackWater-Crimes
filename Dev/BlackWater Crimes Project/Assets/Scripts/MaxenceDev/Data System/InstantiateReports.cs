using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateReports : InstantiationProcess<Report>
{
    private FailedReportsManager failedReportsManager;

    public Transform contentHolder;
    public Transform failedContentHolder;

    public GameObject noPrefab;
    public GameObject FailedPrefab;

    private bool instantiateFailed; // Sad reacts Only

    private List<Report> reportsList = new List<Report>();

    private List<Report> failedReportsList = new List<Report>();

    void Start()
    {
        failedReportsManager = GetComponent<FailedReportsManager>();

        GetGameData();

        foreach (List<Report> _list in gameData.allReports.Values)
        {
            foreach (Report report in _list)
            {
                if (report.unlockedData)
                {
                    if (report.index != 0) reportsList.Add(report);

                    else failedReportsList.Add(report);
                }
            }
        }

        // Instantiation Reports

        if (reportsList.Count == 0) Instantiation(noPrefab);

        else
        {
            reportsList = reportsList.OrderBy(w => w.unlockOrderIndex).ToList();
            reportsList.Reverse();

            InstantiateDataOfType(type, reportsList);
        }

        // Instantiation Failed Reports

        if (failedReportsList.Count > 0)
        {
            foreach (Report _failed in failedReportsList)
            {
                GameObject failedReport = Instantiation(FailedPrefab);
                failedReport.GetComponent<ReportObject>().data = _failed;
                failedReport.transform.SetParent(failedContentHolder, false);

                failedReport.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { failedReportsManager.CloseFailedReport(failedReport); });
            }
        }
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(contentHolder, false);
        
        return _prefab;
    }
}
