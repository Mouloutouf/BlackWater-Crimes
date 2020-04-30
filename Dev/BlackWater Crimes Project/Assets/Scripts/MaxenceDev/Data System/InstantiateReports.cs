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

    [Title("Buttons")]

    public Transform buttonContentHolder;
    public GameObject buttonPrefab;
    public int amountInEachPage;
    public float buttonOffset;

    private float currentOffset;
    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();

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

        CreateNoReport();
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

    void SpawnReports()
    {
        foreach (Holder holder in holders)
        {
            if (holder.reports.Count == 0) Instantiation(noPrefab); // Instantiate No Report

            else
            {
                //holder.reports = holder.elements.OrderBy(w => w.index) as List<Report>;

                currentContent = holder.holderContent;
                
                InstantiateDataOfType(type, holder.reports); // Instantiate Reports
            }
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

            DisplaySystem disp = bindedHolder.display as DisplaySystem;
            if (bindedObject != null) bindedObject.GetComponent<Button>().onClick.AddListener(delegate { disp.DisplayElement(_original); });
            bindedObject = null;

            return _original;
        }
        else if (prefab == buttonPrefab) // Instantiation Process for Button Reports
        {
            GameObject _button = Instantiate(prefab) as GameObject;
            _button.transform.SetParent(buttonContentHolder, false);
            _button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, currentOffset);
            spawnPoints.Add(new Vector2(0, currentOffset));

            currentOffset += buttonOffset;

            return _button;
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

            currentOffset = -35;

            if (buttonPrefab != null) InstantiateDataOfType(type, reportsList, buttonPrefab); // Instantiate Button Reports
        }

        // Instantiation Failed Reports

        if (failedReportsList.Count > 0) 
        {
            InstantiateDataOfType(type, failedReportsList, failedPrefab);
        }
    }
    #endregion
}
