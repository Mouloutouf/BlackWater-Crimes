using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateReports : InstantiationProcess<Report>
{
    private FailedReportsManager failedReportsManager;

    public Transform contentHolder;
    public Transform buttonContentHolder;
    public Transform failedContentHolder;

    public GameObject buttonPrefab;

    public float buttonOffset;
    private float currentOffset;

    public GameObject noPrefab;
    public GameObject failedPrefab;

    private bool instantiateFailed; // Sad reacts Only

    private List<Report> reportsList = new List<Report>();

    private List<Report> failedReportsList = new List<Report>();

    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();

    void Start()
    {
        failedReportsManager = GetComponent<FailedReportsManager>();

        GetGameData();

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

        if (reportsList.Count == 0) Instantiation(noPrefab);

        else
        {
            reportsList = reportsList.OrderBy(w => w.unlockOrderIndex).ToList();
            reportsList.Reverse();

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

    public override GameObject Instantiation(GameObject prefab)
    {
        if (prefab == this.prefab || prefab == noPrefab) // Instantiation Process for Reports
        {
            GameObject _original = Instantiate(prefab) as GameObject;
            _original.transform.SetParent(contentHolder, false);

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
            GameObject _failed = Instantiation(failedPrefab) as GameObject;
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
