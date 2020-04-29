using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateQuestions : InstantiationProcess<Question>
{
    int index;

    void Start()
    {
        GetGameData();

        InstantiateDataOfType(type, gameData.questions[gameData.currentSuspect]);
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab);
        _prefab.transform.SetParent(transform, false);

        int questionIndex = index;
        _prefab.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<InterrogateScript>().Question(questionIndex); } );

        int yPos = -(100*index) + 300;

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos);

        index++;

        return _prefab;
    }
}
