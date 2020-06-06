using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFiles : MonoBehaviour
{
    private InstantiateEvidenceFiles instantiateEvidenceFiles = new InstantiateEvidenceFiles();
    private InstantiateReportFiles instantiateReportFiles = new InstantiateReportFiles();

    public Transform content;

    public int amountInEachRow;
    public float amountInEachColumn;

    public float scaleAmount;

    private List<Vector2> spawnPoints;
    private List<Vector2> spawnScales;

    private int spawnIndex;
    
    public GameObject evidenceFilePrefab;
    public GameObject reportFilePrefab;
    
    void Start()
    {
        instantiateReportFiles.GetGameData();

        //instantiateReportFiles.InstantiateDataOfType()
    }

    void SetLayout()
    {
        float sizeX = content.GetComponent<RectTransform>().rect.width / amountInEachRow;
        float sizeY = content.GetComponent<RectTransform>().rect.height / amountInEachColumn;

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
}

public class InstantiateEvidenceFiles : InstantiationProcess<Evidence>
{
    //kzzzzzzzzzzzz
}

public class InstantiateReportFiles : InstantiationProcess<Report>
{

}