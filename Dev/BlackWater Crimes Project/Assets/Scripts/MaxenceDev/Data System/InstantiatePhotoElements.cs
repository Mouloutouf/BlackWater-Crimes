using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiatePhotoElements : InstantiateElements<Evidence>
{
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

        contents.Add(contentObject.transform);
    }

    protected override void AdditionalSettings(GameObject __prefab)
    {
        __prefab.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { zoomPhoto.ZoomObject(__prefab); });
        
        notificationSystem.groups[NotificationType.Photo].notifications.Add(__prefab.GetComponent<NotificationPhoto>());
        __prefab.GetComponent<NotificationPhoto>().seenEvent.AddListener(delegate { notificationSystem.Seen(NotificationType.Photo); });
    }

    protected override string GetDataName(Evidence data)
    {
        return data.codeName;
    }
}
