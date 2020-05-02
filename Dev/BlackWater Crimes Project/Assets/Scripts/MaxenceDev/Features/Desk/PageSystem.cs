using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSystem : MonoBehaviour
{
    public Button rightButton;
    public Button leftButton;

    public Transform content;

    public int startIndex;
    private int currentIndex = 0;
    
    void Start()
    {
        currentIndex = startIndex;

        CheckIndex();

        if (content.childCount != 0)
        {
            for (int n = startIndex; n < content.childCount; n++) content.GetChild(n).gameObject.SetActive(false);
            content.GetChild(currentIndex).gameObject.SetActive(true);
        }
    }
    
    public void TurnPage(int value)
    {
        content.GetChild(currentIndex).gameObject.SetActive(false);
        content.GetChild(currentIndex + value).gameObject.SetActive(true);
        currentIndex += value;

        CheckIndex();
    }

    void CheckIndex()
    {
        if (currentIndex == content.childCount -1 || content.childCount == 0) rightButton.gameObject.SetActive(false);
        else rightButton.gameObject.SetActive(true);

        if (currentIndex == startIndex || content.childCount == 0) leftButton.gameObject.SetActive(false);
        else leftButton.gameObject.SetActive(true);
    }
}
