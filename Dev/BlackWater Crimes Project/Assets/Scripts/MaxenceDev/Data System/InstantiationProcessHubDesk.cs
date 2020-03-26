using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstantiationProcessHubDesk : InstantiationProcess
{
    public Camera cam;
    public GameObject zoomPanel;

    public int amountInEachRow;
    public int amountInEachColumn;

    public GameObject[] pages;

    public float scaleAmount;

    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();
    [HideInInspector] public List<Vector2> spawnScales = new List<Vector2>();

    private bool isLayoutSet;

    private int index = 0;
    private int pageIndex = 0;

    public GameObject snapColliderPrefab;
    public float snapDist;
    
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.parent.GetComponent<PhotoObject>() != null && Input.GetMouseButtonDown(0))
        {
            PhotoObject photoScript = hits[0].transform.parent.GetComponent<PhotoObject>();
            ZoomPhoto(photoScript.data.photo, photoScript.data.name);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            DeZoom();
        }
    }

    public override GameObject Instantiation()
    {
        if (!isLayoutSet)
        {
            SetLayout();
            isLayoutSet = true;
        }
        
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(pages[pageIndex].transform, false);

        //Debug.Log(index);
        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[index];
        _prefab.GetComponent<RectTransform>().sizeDelta = spawnScales[index];
        
        _prefab.transform.GetChild(0).GetComponent<BoxCollider2D>().size = spawnScales[index];
        _prefab.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = spawnScales[index];

        _prefab.GetComponent<PhotoObject>().photosBooklet = this.gameObject;
        _prefab.GetComponent<PhotoObject>().cam = this.cam;

        index++;
        if (index == amountInEachRow * amountInEachColumn) { pageIndex++; index = 0; }

        return _prefab;
    }

    [ContextMenu("Set Layout")]
    public void SetLayout()
    {
        float sizeX = pages[0].GetComponent<RectTransform>().rect.width / amountInEachRow;
        float sizeY = pages[0].GetComponent<RectTransform>().rect.height / amountInEachColumn;

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

                for (int n = 0; n < pages.Length; n++) InstantiateSnapCollider(posX, posY, n);
            }
        }
    }

    void InstantiateSnapCollider(float _posX, float _posY, int _pageIndex)
    {
        GameObject collider = Instantiate(snapColliderPrefab) as GameObject;
        collider.transform.SetParent(pages[_pageIndex].transform, false);

        collider.GetComponent<RectTransform>().anchoredPosition = new Vector2(_posX, -_posY);

        collider.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(snapDist, snapDist);

        collider.transform.GetChild(1).GetComponent<SnapToGrid>().pageLocation = _pageIndex;
    }

    public void ZoomPhoto(Sprite photoSprite, string photoName)
    {
        zoomPanel.SetActive(true);
        zoomPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = photoSprite;
        zoomPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = photoName;
    }

    public void DeZoom()
    {
        zoomPanel.SetActive(false);
    }
}
