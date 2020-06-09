using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SetSpecialist : MonoBehaviour
{
    public GameData gameData;

    public GameObject specialistPrefab;
    public GameObject headHunterPrefab;
    public GameObject neighborhoodPrefab;

    public Image characterImage;
    public float factor;
    public Localisation characterName;
    public Localisation characterJob;

    [Header("For Debug Only")]
    [SerializeField] private bool debug;
    [ShowIf("debug")]
    [SerializeField] private Indics indicToTest;
    
    void Start()
    {
        if (!debug)
        {
            foreach (Indics indic in gameData.indics.Keys)
            {
                if (indic == gameData.currentIndic)
                {
                    Set(indic);
                }
            }
        }
        else
        {
            Set(indicToTest);
        }
    }

    void Set(Indics _indic)
    {
        characterImage.sprite = gameData.indics[_indic].image;
        characterImage.SetNativeSize();
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x / factor, rt.sizeDelta.y / factor);

        characterName.key = gameData.indics[_indic].nameKey;
        characterName.RefreshText();

        characterJob.key = gameData.indics[_indic].jobKey;
        characterJob.RefreshText();

        GameObject prefabToUse = (_indic == Indics.James_Walker) ? headHunterPrefab : (_indic == Indics.Thomas_Maxwell) ? neighborhoodPrefab : specialistPrefab;

        GameObject _prefab = Instantiate(prefabToUse);
        _prefab.transform.SetParent(this.transform, false);
    }
}
