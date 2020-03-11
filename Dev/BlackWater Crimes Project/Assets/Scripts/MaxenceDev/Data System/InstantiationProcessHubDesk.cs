using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationProcessHubDesk : InstantiationProcess
{
    public int amountInEachRow;
    public int amountInEachColumn;

    public float scaleAmount;
    
    private bool isLayoutSet;

    private List<Vector2> spawnPoints = new List<Vector2>();
    private List<Vector2> spawnScales = new List<Vector2>();

    private int index = 0;

    public GameObject snapColliderPrefab;
    public float snapDist;

    public override GameObject Instantiation()
    {
        if (!isLayoutSet) SetLayout();

        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(gameObject.transform, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[index];
        _prefab.GetComponent<RectTransform>().sizeDelta = spawnScales[index];

        _prefab.transform.GetChild(0).GetComponent<BoxCollider2D>().size = spawnScales[index];
        _prefab.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = spawnScales[index];

        index++;

        return _prefab;
    }

    [ContextMenu("Set Layout")]
    public void SetLayout()
    {
        //int amountOfCadrans = amountInEachColumn * amountInEachRow;
        float sizeX = GetComponent<RectTransform>().rect.width / amountInEachRow;
        float sizeY = GetComponent<RectTransform>().rect.height / amountInEachColumn;
        
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

                InstantiateSnapCollider(posX, posY);
            }
        }

        isLayoutSet = true;
    }

    void InstantiateSnapCollider(float _posX, float _posY)
    {
        GameObject collider = Instantiate(snapColliderPrefab) as GameObject;
        collider.transform.SetParent(gameObject.transform, false);

        collider.GetComponent<RectTransform>().anchoredPosition = new Vector2(_posX, -_posY);

        collider.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(snapDist, snapDist);
    }
}
