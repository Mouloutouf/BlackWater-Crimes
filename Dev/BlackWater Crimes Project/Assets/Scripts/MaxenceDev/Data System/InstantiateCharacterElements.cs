using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateCharacterElements : InstantiationProcess<Character>
{
    public GameObject placeHolderPrefab;

    [Title("Settings")]

    public int amountInEachColumn;
    public int amountInEachRow;

    public float scaleAmount;

    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();
    [HideInInspector] public List<Vector2> spawnScales = new List<Vector2>();

    [Title("Contents", horizontalLine: false)]

    public InstantiateReports mainContentScript;
    private int mainIndex = 0;

    public Transform placeHolderContent;

    public List<Transform> contents;
    private Transform currentContent;

    private Transform pageContent;

    private int spawnIndex = 0;

    public List<GameObject> elementsList { get; private set; } = new List<GameObject>();

    public DisplaySystem characterDisplay;

    void Start()
    {
        GetGameData();
        
        SetLayout();

        for (int i = 0; i < spawnPoints.Count; i++) // Instantiate Place Holder Prefabs
        {
            Instantiation(placeHolderPrefab);
        }

        spawnIndex = 0;

        int local = 0;
        currentContent = contents[local];
        CreatePage(currentContent);

        InstantiateDataOfType(type, gameData.characters);

        foreach (Character _character in gameData.characters) if (_character.unlockedData) { SetElement(_character); mainIndex++; }
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == this.prefab)
        {
            GameObject _prefab = Instantiate(prefab) as GameObject;
            _prefab.transform.SetParent(pageContent, false);

            _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];

            GameObject obj = _prefab.transform.GetChild(0).GetChild(_prefab.transform.GetChild(0).childCount - 1).gameObject;
            _prefab.GetComponent<Button>().onClick.AddListener(delegate { characterDisplay.SelectElement(obj); });

            elementsList.Add(_prefab);

            spawnIndex++;
            if (spawnIndex == amountInEachRow * amountInEachColumn) { CreatePage(currentContent); }

            return _prefab;
        }
        else if (prefab == placeHolderPrefab)
        {
            GameObject __prefab = Instantiate(prefab) as GameObject;
            __prefab.transform.SetParent(placeHolderContent, false);

            __prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];

            spawnIndex++;

            return __prefab;
        }
        else
        {
            return prefab;
        }
    }

    void SetElement(Character character)
    {
        int ind = mainIndex;
        mainContentScript.holders[2].elements.Add(new Element { index = ind, name = character.name, elementObject = elementsList[ind] });
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
