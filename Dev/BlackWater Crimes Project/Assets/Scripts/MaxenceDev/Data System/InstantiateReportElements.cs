using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateReportElements : InstantiationProcess<Report>
{
    [Title("Settings")]

    public int amountInEachPage;
    
    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();
    
    [Title("Contents", horizontalLine: false)]

    public InstantiateReports mainContentScript;
    private int mainIndex = 0;

    public List<Transform> contents;
    private Transform currentContent;

    private Transform pageContent;

    private int spawnIndex = 0;

    public List<GameObject> elementsList { get; private set; } = new List<GameObject>();

    private List<Report> reportsList = new List<Report>();

    public DisplaySystem reportDisplay;

    void Start()
    {
        GetGameData();
        
        SetLayout();

        int local = 0;
        currentContent = contents[local];
        CreatePage(currentContent);
        
        foreach (List<Report> _list in gameData.allReports.Values)
        {
            foreach (Report report in _list)
            {
                if (report.unlockedData && report.index != 0)
                {
                    reportsList.Add(report);
                }
            }
        }

        reportsList = reportsList.OrderBy(w => w.unlockOrderIndex).ToList();
        reportsList.Reverse();

        foreach (Report _report in reportsList)
        {
            InstantiateObjectOfType(_report, this.prefab);

            SetElement(_report);
            mainIndex++;
        }
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(pageContent, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];

        _prefab.GetComponent<RectTransform>().offsetMin = new Vector2(15, _prefab.GetComponent<RectTransform>().offsetMin.y);
        _prefab.GetComponent<RectTransform>().offsetMax = new Vector2(-15, _prefab.GetComponent<RectTransform>().offsetMax.y);

        GameObject obj = _prefab.transform.GetChild(0).GetChild(_prefab.transform.GetChild(0).childCount - 1).gameObject;
        _prefab.GetComponent<Button>().onClick.AddListener(delegate { reportDisplay.SelectElement(obj); });

        elementsList.Add(_prefab);

        spawnIndex++;
        if (spawnIndex == amountInEachPage) { CreatePage(currentContent); }

        return _prefab;
    }

    void SetElement(Report report)
    {
        int ind = mainIndex;
        mainContentScript.holders[0].elements.Add(new Element { index = ind, name = report.elementName, elementObject = elementsList[ind] });
    }

    void SetLayout()
    {
        float sizeX = contents[0].GetComponent<RectTransform>().rect.width;
        float sizeY = contents[0].GetComponent<RectTransform>().rect.height / amountInEachPage;

        float posX;
        float posY;
        
        for (int w = 0; w < amountInEachPage; w++)
        {
            posY = (sizeY / 2) + sizeY * w;
            posX = (sizeX / 2);

            spawnPoints.Add(new Vector2(posX, -posY));
        }
    }

    void CreatePage(Transform content)
    {
        GameObject page = Instantiate(new GameObject());
        page.transform.SetParent(content, false);

        page.AddComponent<RectTransform>();
        page.GetComponent<RectTransform>().anchorMin = Vector2.zero; // sets the mode (stretch)
        page.GetComponent<RectTransform>().anchorMax = Vector2.one;
        page.GetComponent<RectTransform>().sizeDelta = Vector2.zero; // sets the size (offsets to 0)

        page.name = "Page Content";

        pageContent = page.transform;

        spawnIndex = 0;
    }
}
