using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiatePhotoElements : InstantiateElements<Evidence>
{
    [Title("Photo References", horizontalLine: false)]

    public ZoomPhoto zoomPhoto;

    public NotificationSystem notificationSystem;

    [Title("Create Contents")]

    public Transform contentParent;
    public GameObject contentPrefab;

    protected override List<List<Evidence>> GetAllElements()
    {
        List<List<Evidence>> mainList = new List<List<Evidence>>();

        foreach (Location location in gameData.locations)
        {
            if (location.known)
            {
                mainList.Add(GetAllEvidences(location));

                InstantiateContent(location);
            }
        }
        
        return mainList;
    }

    private List<Evidence> GetAllEvidences(Location location)
    {
        List<Evidence> evidences = new List<Evidence>();

        foreach (Locations _location in gameData.evidences.Keys)
        {
            if (location.myLocation == _location)
            {
                evidences = gameData.evidences[_location];
            }
        }

        return evidences;
    }
    
    void InstantiateContent(Location location)
    {
        GameObject contentObject = Instantiate(contentPrefab);
        contentObject.transform.SetParent(contentParent, false);
        
        contentObject.name = location.myLocation.ToString() + " List";

        contentObject.GetComponentInChildren<Localisation>().key = location.nameKey;
        contentObject.GetComponentInChildren<Localisation>().RefreshText();

        contents.Add(contentObject.transform.GetChild(1));
    }

    protected override void AdditionalSettings(GameObject __prefab)
    {
        __prefab.GetComponent<Button>().onClick.AddListener(delegate { zoomPhoto.ZoomObject(__prefab); });

        __prefab.GetComponent<NotificationPhoto>().notificationSystem = notificationSystem;
    }

    protected override string GetDataName(Evidence data)
    {
        return data.codeName;
    }

    protected override string GetDataName(Evidence data, bool getKey)
    {
        return data.nameKey;
    }
}
