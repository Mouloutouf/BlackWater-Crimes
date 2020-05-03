using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[Serializable]
public class Element
{
    public string name;
    public int index;

    public GameObject elementObject;
}

[Serializable]
public class Holder
{
    public DisplaySystem display;

    public Transform holderContent;
    
    public List<Element> elements = new List<Element>();

    public List<Report> reports;
}

public class InstantiateReports : InstantiationProcess<Report>
{
    public GameObject noPrefab;
    public GameObject failedPrefab;

    [Title("Holders")]
    
    public Transform failedContentHolder;
    private List<Report> failedReportsList = new List<Report>();

    public List<Holder> holders = new List<Holder>();
    private Transform currentContent;
    private GameObject bindedObject;
    private Holder bindedHolder;
    
    public bool useOld;
    [ShowIf("useOld")]
    public Transform content;
    private List<Report> reportsList = new List<Report>();
    
    private FailedReportsManager failedReportsManager;

    void Start()
    {
        GetGameData();
        failedReportsManager = GetComponent<FailedReportsManager>();

        if (useOld) { OldInstantiate(); return; }
        
        Initialize();
    }

    void Initialize()
    {
        foreach (List<Report> _list in gameData.allReports.Values)
        {
            foreach (Report report in _list)
            {
                if (report.unlockedData)
                {
                    if (report.index == 0) CreateFailedReport(report);

                    else CreateReport(report);
                }
            }
        }

        //CreateNoReport();
    }

    void CreateReport(Report report)
    {
        foreach (Holder holder in holders)
        {
            foreach (Element element in holder.elements)
            {
                if (element.name == report.elementName && report.unlockedData)
                {
                    holder.reports.Add(report);

                    bindedObject = element.elementObject;
                    bindedHolder = holder;

                    currentContent = holder.holderContent;

                    InstantiateObjectOfType(report, this.prefab);
                }
            }
        }
    }

    void CreateFailedReport(Report report)
    {
        InstantiateObjectOfType(report, failedPrefab);
    }

    void CreateNoReport()
    {
        foreach (Holder holder in holders)
        {
            if (holder.reports.Count == 0) Instantiation(noPrefab); // Instantiate No Report
        }
    }
    
    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == this.prefab || prefab == noPrefab) // Instantiation Process for Reports
        {
            GameObject _original = Instantiate(prefab) as GameObject;
            _original.transform.SetParent(currentContent, false);

            _original.GetComponent<RectTransform>().anchorMin = Vector2.zero; // sets the mode (stretch)
            _original.GetComponent<RectTransform>().anchorMax = Vector2.one;
            _original.GetComponent<RectTransform>().sizeDelta = Vector2.zero; // sets the size (offsets to 0)

            if (useOld) return _original;
            
            if (bindedObject != null)
            {
                DisplaySystem disp = bindedHolder.display;
                bindedObject.GetComponent<Button>().onClick.AddListener(delegate { disp.DisplayElement(_original); });
                //GameObject obj = bindedObject.transform.GetChild(0).GetChild(bindedObject.transform.GetChild(0).childCount - 1).gameObject;
                //bindedObject.GetComponent<Button>().onClick.AddListener(delegate { disp.SelectElement(obj); });
                _original.GetComponent<ElementHolder>().bind = bindedObject;
            }
            
            bindedObject = null;
            
            return _original;
        }

        else if (prefab == failedPrefab) // Instantiation Process for Failed Reports
        {
            GameObject _failed = Instantiate(failedPrefab) as GameObject;
            _failed.transform.SetParent(failedContentHolder, false);

            _failed.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(delegate { failedReportsManager.CloseFailedReport(_failed); });

            return _failed;
        }

        else
        {
            return prefab;
        }
    }

    #region Old
    void OldInstantiate()
    {
        // Check Reports validity (put the Unlocked Report in either Lists)

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

        // Instantiation Reports & Button Reports

        if (reportsList.Count == 0) Instantiation(noPrefab); // Instantiate No Report

        else
        {
            reportsList = reportsList.OrderBy(w => w.unlockOrderIndex).ToList();
            reportsList.Reverse();

            currentContent = content;

            InstantiateDataOfType(type, reportsList); // Instantiate Reports
        }

        // Instantiation Failed Reports

        if (failedReportsList.Count > 0) 
        {
            InstantiateDataOfType(type, failedReportsList, failedPrefab);
        }
    }
    #endregion
}
