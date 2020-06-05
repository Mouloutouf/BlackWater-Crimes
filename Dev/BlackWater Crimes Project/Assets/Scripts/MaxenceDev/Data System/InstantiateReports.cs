using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateReports : InstantiationProcess<Report>
{
    private Transform content;

    [Title("Failed Settings")]

    public GameObject failedPrefab;
    public Transform failedContent;
    private FailedReportsManager failedReportsManager;

    void Start()
    {
        GetGameData();

        failedReportsManager = GetComponent<FailedReportsManager>();
        
        InstantiateFailedReports();
    }

    void InstantiateFailedReports()
    {
        foreach ((List<Report>, List<Report>) _list in gameData.reports.Values)
        {
            foreach (Report fReport in _list.Item2)
            {
                if (fReport.index != 0) CreateFailedReport(fReport);
            }
        }
    }
    
    void CreateFailedReport(Report report)
    {
        InstantiateObjectOfType(report, failedPrefab);
    }
    
    public void CreateAssociatedReport(Transform content, GameObject element, Report report, DisplaySystem display)
    {
        this.content = content;

        GameObject reportPrefab = InstantiateObjectOfType(report, this.prefab);
        
        element.GetComponent<Button>().onClick.AddListener(delegate { display.DisplayElement(element, reportPrefab); });
        
        if (element.TryGetComponent<NotificationReport>(out NotificationReport obj)) element.GetComponent<NotificationReport>().informationObject = reportPrefab;
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == this.prefab) // Instantiation Process for Reports
        {
            GameObject _original = Instantiate(prefab) as GameObject;
            _original.transform.SetParent(content, false);

            _original.GetComponent<RectTransform>().anchorMin = Vector2.zero; // sets the mode (stretch)
            _original.GetComponent<RectTransform>().anchorMax = Vector2.one;
            _original.GetComponent<RectTransform>().sizeDelta = Vector2.zero; // sets the size (offsets to 0)
            
            return _original;
        }

        else if (prefab == failedPrefab) // Instantiation Process for Failed Reports
        {
            GameObject _failed = Instantiate(failedPrefab) as GameObject;
            _failed.transform.SetParent(failedContent, false);

            _failed.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { failedReportsManager.CloseFailedReport(_failed); });

            return _failed;
        }

        else
        {
            return prefab;
        }
    }
}
