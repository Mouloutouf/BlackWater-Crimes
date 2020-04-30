using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestForClueScroll : MonoBehaviour
{
    public int numberOfClues;
    public GameObject clueTest;
    int index;

    void OnEnable()
    {
        index = 0;
        transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(720, 1480);
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

        if(index % 2 == 0) //index is even
        {
            int xPos = -150;
            int yPos = -(150*index) - 200;
            clueClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            if(index == 8)
            {
                transform.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 150);
            }
            else if(index > 8)
            {
                transform.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 300);
            }
        } 
        else //index is odd
        {
            int xPos = 150;
            int yPos = -(150*(index-1)) - 200;
            clueClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
        }

        index++;
    }
}
