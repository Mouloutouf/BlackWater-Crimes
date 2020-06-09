using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateFiles : MonoBehaviour
{
    private GameData gameData;

    public FileDisplayer fileDisplayer;

    private InstantiateEvidenceFiles instantiateEvidenceFiles = new InstantiateEvidenceFiles();
    private InstantiateReportFiles instantiateReportFiles = new InstantiateReportFiles();

    public int amountPerRow;
    private int amount;

    public float verticalPosition;
    public float offset;
    private float ofst = 0;

    private List<Vector2> spawnPoints = new List<Vector2>();
    
    private int spawnIndex;

    public Transform folderContent;
    public GameObject evidenceFilePrefab;
    public GameObject reportFilePrefab;
    
    public Transform displayContent;
    public GameObject evidenceElementPrefab;
    public GameObject reportElementPrefab;

    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
        
        SetLayout(ofst);

        instantiateReportFiles.instantiateFiles = this;
        instantiateEvidenceFiles.instantiateFiles = this;

        List<Data> dataList = new List<Data>();

        foreach ((List<Report>, List<Report>) list in gameData.reports.Values)
        {
            foreach (Report report in list.Item1)
            {
                if (report.unlockedData) dataList.Add(report);
            }
        }

        foreach (List<Evidence> list in gameData.evidences.Values)
        {
            foreach (Evidence evidence in list)
            {
                if (evidence.unlockedData) dataList.Add(evidence);
            }
        }

        dataList = dataList.OrderBy(x => Random.value).ToList();

        foreach (Data data in dataList)
        {
            if (data.GetType() == typeof(Report))
            {
                GameObject rFile = instantiateReportFiles.InstantiateObjectOfType(data as Report, reportFilePrefab);

                GameObject rElement = instantiateReportFiles.InstantiateObjectOfType(data as Report, reportElementPrefab);
                
                rFile.GetComponent<Button>().onClick.AddListener(delegate { fileDisplayer.DisplayFile(rElement, rFile); });
            }

            else if (data.GetType() == typeof(Evidence))
            {
                GameObject eFile = instantiateEvidenceFiles.InstantiateObjectOfType(data as Evidence, evidenceFilePrefab);

                GameObject eElement = instantiateEvidenceFiles.InstantiateObjectOfType(data as Evidence, evidenceElementPrefab);
                
                eFile.GetComponent<Button>().onClick.AddListener(delegate { fileDisplayer.DisplayFile(eElement, eFile); });
            }
        }
    }

    public GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(folderContent, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];
        
        spawnIndex++;
        if (spawnIndex == amount) { ofst += offset; SetLayout(ofst); }

        return _prefab;
    }

    public GameObject CreateAssociatedElement(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(displayContent, false);

        return _prefab;
    }

    void SetLayout(float _offset)
    {
        amount += amountPerRow;

        float sizeX = folderContent.GetComponent<RectTransform>().rect.width / amountPerRow;
        
        float posX;
        float posY = verticalPosition + _offset;
        
        for (int v = 0; v < amountPerRow; v++)
        {
            posX = (sizeX / 2) + sizeX * v;

            spawnPoints.Add(new Vector2(posX, posY));
        }
    }
}

public class InstantiateEvidenceFiles : InstantiationProcess<Evidence>
{
    public InstantiateFiles instantiateFiles { get; set; }

    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == instantiateFiles.evidenceFilePrefab)
        {
            GameObject filePrefab = instantiateFiles.Instantiation(prefab);

            return filePrefab;
        }
        else if (prefab == instantiateFiles.evidenceElementPrefab)
        {
            GameObject elementPrefab = instantiateFiles.CreateAssociatedElement(prefab);

            return elementPrefab;
        }
        else
        {
            return prefab;
        }
    }
}

public class InstantiateReportFiles : InstantiationProcess<Report>
{
    public InstantiateFiles instantiateFiles { get; set; }

    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == instantiateFiles.reportFilePrefab)
        {
            GameObject filePrefab = instantiateFiles.Instantiation(prefab);

            return filePrefab;
        }
        else if (prefab == instantiateFiles.reportElementPrefab)
        {
            GameObject elementPrefab = instantiateFiles.CreateAssociatedElement(prefab);

            return elementPrefab;
        }
        else
        {
            return prefab;
        }
    }
}