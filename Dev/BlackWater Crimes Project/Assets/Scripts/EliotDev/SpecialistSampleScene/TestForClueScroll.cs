using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestForClueScroll : MonoBehaviour
{
    public int numberOfClues;
    public GameObject clueTest;
    public int cluesPerRow;
    int index;
    int indexInRow;
    int rowNumber;
    float yPos;
    float xPos;

    void OnEnable()
    {
        index = 0;
        indexInRow = 0;
        rowNumber = -1;
        transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(1480, 720);
        foreach(Transform children in transform)
        {
            Destroy(children.gameObject);
        }
        for (int i = 0; i < numberOfClues; i++)
        {
            CreateClue();
        }
    }

    public void CreateClue()
    {
        GameObject clueClone = Instantiate(clueTest) as GameObject;

        clueClone.transform.SetParent(transform);

        if(index % cluesPerRow == 0)
        {
            indexInRow = 0;
            rowNumber ++;
            yPos = -(300*rowNumber) - 200;
            if(rowNumber >= 2)
            {
                transform.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 300);
            }
        }

        xPos = (350 * indexInRow) - 500;

        clueClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

        index++;
        indexInRow ++;
    }
}
