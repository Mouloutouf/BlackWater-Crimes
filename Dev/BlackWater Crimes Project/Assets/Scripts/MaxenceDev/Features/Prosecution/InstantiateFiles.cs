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

    public Transform content;

    public int amountPerRow;

    public float verticalPosition;
    public float offset;
    private float ofst = 0;

    private List<Vector2> spawnPoints = new List<Vector2>();
    
    private int spawnIndex;
    
    public GameObject evidenceFilePrefab;
    public GameObject reportFilePrefab;
    
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
                dataList.Add(report);
            }
        }

        foreach (List<Evidence> list in gameData.evidences.Values)
        {
            foreach (Evidence evidence in list)
            {
                dataList.Add(evidence);
            }
        }

        dataList = dataList.OrderBy(x => Random.value).ToList();

        foreach (Data data in dataList)
        {
            if (data.GetType() == typeof(Report)) instantiateReportFiles.InstantiateObjectOfType(data as Report, reportFilePrefab);
            else if (data.GetType() == typeof(Evidence)) instantiateEvidenceFiles.InstantiateObjectOfType(data as Evidence, evidenceFilePrefab);
            else Debug.Log("Data is not a valid Type container");
        }
    }

    public GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(content, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];
        
        _prefab.GetComponent<Button>().onClick.AddListener( delegate { fileDisplayer.SelectFile(_prefab); } );

        spawnIndex++;
        if (spawnIndex == amountPerRow) { ofst += offset; SetLayout(ofst); }

        return _prefab;
    }

    void SetLayout(float _offset)
    {
        float sizeX = content.GetComponent<RectTransform>().rect.width / amountPerRow;
        
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
        GameObject _prefab = instantiateFiles.Instantiation(prefab);

        return _prefab;
    }
}

public class InstantiateReportFiles : InstantiationProcess<Report>
{
    public InstantiateFiles instantiateFiles { get; set; }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = instantiateFiles.Instantiation(prefab);

        return _prefab;
    }
}