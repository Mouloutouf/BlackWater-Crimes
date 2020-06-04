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
    public GameObject failedPrefab;

    [Title("Holders")]
    
    public List<Holder> holders = new List<Holder>();
    private Transform reportsContent;
    private GameObject bindedObject;
    private Holder bindedHolder;

    public Transform failedContentHolder;
    private FailedReportsManager failedReportsManager;

    void Start()
    {
        GetGameData();
        failedReportsManager = GetComponent<FailedReportsManager>();
        
        Initialize();
    }

    void Initialize()
    {
        foreach ((List<Report>, List<Report>) _list in gameData.megaReports.Values)
        {
            foreach (Report report in _list.Item1)
            {
                if (report.unlockedData) CreateReport(report);
            }

            foreach (Report fReport in _list.Item2)
            {
                if (fReport.index != 0) CreateFailedReport(fReport);
            }
        }
    }

    void CreateReport(Report report)
    {
        foreach (Holder holder in holders)
        {
            foreach (Element element in holder.elements)
            {
                if (element.name == report.elementName)
                {
                    holder.reports.Add(report);

                    bindedObject = element.elementObject;
                    bindedHolder = holder;

                    reportsContent = holder.holderContent;

                    InstantiateObjectOfType(report, this.prefab);
                }
            }
        }
    }

    void CreateFailedReport(Report report)
    {
        InstantiateObjectOfType(report, failedPrefab);
    }
    
    public void InstantiateByElement(Transform content, GameObject element, Report report, DisplaySystem display)
    {
        bindedObject = null; // Debug

        reportsContent = content;

        GameObject reportPrefab = InstantiateObjectOfType(report, this.prefab);
        
        element.GetComponent<Button>().onClick.AddListener(delegate { display.DisplayElement(reportPrefab); });
        reportPrefab.GetComponent<ElementHolder>().bind = element;
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == this.prefab) // Instantiation Process for Reports
        {
            GameObject _original = Instantiate(prefab) as GameObject;
            _original.transform.SetParent(reportsContent, false);

            _original.GetComponent<RectTransform>().anchorMin = Vector2.zero; // sets the mode (stretch)
            _original.GetComponent<RectTransform>().anchorMax = Vector2.one;
            _original.GetComponent<RectTransform>().sizeDelta = Vector2.zero; // sets the size (offsets to 0)
            
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
}
