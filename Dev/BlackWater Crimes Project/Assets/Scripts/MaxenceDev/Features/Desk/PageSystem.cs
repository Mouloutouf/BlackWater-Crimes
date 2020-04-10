using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSystem : MonoBehaviour
{
    public Button rightButton;
    public Button leftButton;

    public Transform content;
    
    private int currentIndex = 1;

    private bool start = true;
    
    void Update()
    {
        if (start)
        {
            CheckIndex();

            for (int n = 1; n < content.childCount; n++) content.GetChild(n).gameObject.SetActive(false);
            content.GetChild(currentIndex).gameObject.SetActive(true);

            start = false;
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
        if (currentIndex == content.childCount -1) rightButton.gameObject.SetActive(false);
        else rightButton.gameObject.SetActive(true);

        if (currentIndex == 1) leftButton.gameObject.SetActive(false);
        else leftButton.gameObject.SetActive(true);
    }
}
