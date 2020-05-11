using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSpecialist : MonoBehaviour
{
    public GameData gameData;

    public GameObject specialistPrefab;
    public GameObject headHunterPrefab;
    public GameObject neighborhoodPrefab;

    public Image characterImage;
    public float factor;
    public Text characterName;
    public Text characterJob;
    
    void Start()
    {
        foreach (Indics indic in gameData.indics.Keys)
        {
            if (indic == gameData.currentIndic)
            {
                Set(indic);
            }
        }
    }

    void Set(Indics _indic)
    {
        characterImage.sprite = gameData.indics[_indic].image;
        characterImage.SetNativeSize();
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x * factor, rt.sizeDelta.y * factor);

        characterName.text = gameData.indics[_indic].name;

        characterJob.text = gameData.indics[_indic].job;

        GameObject prefabToUse = (_indic == Indics.James_Walker) ? headHunterPrefab : (_indic == Indics.Thomas_Maxwell) ? neighborhoodPrefab : specialistPrefab;

        GameObject _prefab = Instantiate(prefabToUse);
        _prefab.transform.SetParent(this.transform, false);
    }
}
