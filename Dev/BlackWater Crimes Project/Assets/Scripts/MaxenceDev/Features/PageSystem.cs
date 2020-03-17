using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSystem : MonoBehaviour
{
    public Button rightButton;
    public Button leftButton;

    public Transform reportContent;
    
    private int currentIndex = 1;

    void Start()
    {
        CheckIndex();

        for (int n = 1; n < reportContent.childCount; n++) reportContent.GetChild(n).gameObject.SetActive(false);
        reportContent.GetChild(currentIndex).gameObject.SetActive(true);
    }

    public void TurnPage(int value)
    {
        reportContent.GetChild(currentIndex).gameObject.SetActive(false);
        reportContent.GetChild(currentIndex + value).gameObject.SetActive(true);
        currentIndex += value;

        CheckIndex();
    }

    void CheckIndex()
    {
        if (currentIndex == reportContent.childCount -1) rightButton.gameObject.SetActive(false);
        else rightButton.gameObject.SetActive(true);

        if (currentIndex == 1) leftButton.gameObject.SetActive(false);
        else leftButton.gameObject.SetActive(true);
    }
}
