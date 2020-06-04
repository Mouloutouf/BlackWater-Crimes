using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateElements<T> : InstantiationProcess<T> where T : Data
{
    public List<List<T>> allElements { get { return GetAllElements(); } }

    protected List<T> allData;

    public InstantiateReports instantiateReports;

    [Title("Settings")]

    public int amountInEachColumn;
    public int amountInEachRow;

    public float scaleAmount;

    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();
    [HideInInspector] public List<Vector2> spawnScales = new List<Vector2>();
    
    [Title("Contents", horizontalLine: false)]
    
    public List<Transform> contents;
    private Transform currentContent;
    private Transform pageContent;
    
    private int spawnIndex = 0;
    
    public Transform reportsContent;
    public DisplaySystem display;

    protected virtual List<List<T>> GetAllElements()
    {
        return new List<List<T>>();
    }

    #region Instantiation Process
    protected virtual bool Check(T data)
    {
        return data.unlockedData;
    }

    protected virtual void OrderElements() {}

    void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        GetGameData();
        
        int local = 0;

        SetLayout();
        
        spawnIndex = 0;

        foreach (List<T> list in allElements)
        {
            currentContent = contents[local];
            CreatePage(currentContent);
            
            foreach (T data in list)
            {
                if (Check(data))
                {
                    allData.Add(data);
                }
            }

            OrderElements();

            foreach (T data in allData)
            {
                GameObject element = InstantiateObjectOfType(data, this.prefab);

                InstantiateReport(data, element);
            }

            local++;
        }
    }
    #endregion

    #region Instantiate Prefab
    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == this.prefab)
        {
            GameObject _prefab = Instantiate(prefab) as GameObject;
            _prefab.transform.SetParent(pageContent, false);

            _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];
            
            AdditionalSettings(_prefab);
            
            spawnIndex++;
            if (spawnIndex == amountInEachRow * amountInEachColumn) { CreatePage(currentContent); }

            return _prefab;
        }
        else
        {
            return prefab;
        }
    }

    protected virtual void AdditionalSettings(GameObject __prefab) {}
    #endregion

    #region Instantiate Report
    void InstantiateReport(T _data, GameObject _element)
    {
        foreach ((List<Report>, List<Report>) megaList in gameData.megaReports.Values)
        {
            foreach (Report _report in megaList.Item1)
            {
                if (_report.elementName == GetDataName(_data))
                {
                    instantiateReports.CreateAssociatedReport(reportsContent, _element, _report, display);
                }
            }
        }
    }
    
    protected virtual string GetDataName(T data) { return ""; }
    #endregion

    #region Layout
    protected virtual void SetLayout()
    {
        float sizeX = contents[0].GetComponent<RectTransform>().rect.width / amountInEachRow;
        float sizeY = contents[0].GetComponent<RectTransform>().rect.height / amountInEachColumn;

        float posX;
        float posY;

        float scaleX;
        float scaleY;

        for (int w = 0; w < amountInEachColumn; w++)
        {
            posY = (sizeY / 2) + sizeY * w;

            for (int v = 0; v < amountInEachRow; v++)
            {
                posX = (sizeX / 2) + sizeX * v;

                spawnPoints.Add(new Vector2(posX, -posY));

                if (sizeX > sizeY) { scaleX = sizeY - scaleAmount; scaleY = sizeY - scaleAmount; } // Scale with Height
                else { scaleX = sizeX - scaleAmount; scaleY = sizeX - scaleAmount; } // Scale with Width

                spawnScales.Add(new Vector2(scaleX, scaleY));
            }
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
    #endregion
}
