using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InstantiatePhotoElements : InstantiationProcess<Evidence>
{
    [Title("Settings")]

    public int amountInEachColumn;
    public int amountInEachRow;

    public float scaleAmount;

    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();
    [HideInInspector] public List<Vector2> spawnScales = new List<Vector2>();

    [Title("Contents", horizontalLine: false)]

    public InstantiateReports mainContentScript;
    private int mainIndex = 0;

    public List<Transform> contents;
    private Transform currentContent;
    
    private Transform pageContent;
    
    private int spawnIndex = 0;
    
    public List<GameObject> elementsList { get; private set; } = new List<GameObject>();
    
    void Start()
    {
        GetGameData();
        
        int local = 0;

        SetLayout();

        foreach (List<Evidence> _list in gameData.allEvidences.Values)
        {
            currentContent = contents[local];
            CreatePage(currentContent);
            
            InstantiateDataOfType(type, _list);

            foreach (Evidence _evidence in _list) if (_evidence.unlockedData) { SetElement(_evidence); mainIndex++; }

            local++;
        }
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(pageContent, false);
        
        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];
        
        elementsList.Add(_prefab);

        spawnIndex++;
        if (spawnIndex == amountInEachRow * amountInEachColumn) { CreatePage(currentContent); }
        
        return _prefab;
    }

    void SetElement(Evidence evidence)
    {
        int ind = mainIndex;
        mainContentScript.holders[3].elements.Add(new Element { index = ind, name = evidence.codeName, elementObject = elementsList[ind] });
    }

    void SetLayout()
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
}
