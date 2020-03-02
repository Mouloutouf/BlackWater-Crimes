using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryDrag : MonoBehaviour
{
    [SerializeField] float minSwipeScreenPercentage;
    float minSwipeDistance;
    Vector3 fPos;
    Vector3 lPos;
    float yPos;
    float dragOffset;
    bool isDraging = false;
    // Start is called before the first frame update
    void Start()
    {
        minSwipeDistance = Screen.height * minSwipeScreenPercentage / 100;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSwipe();
    }

    public void StartDrag()
    {
        if (Input.touchCount == 1)
        {
            yPos = Input.touches[0].position.y;
            dragOffset = yPos - transform.position.y;
        }
        isDraging = true;
    }

    public void Drag()
    {
        if (Input.touchCount == 1 && GetComponent<RectTransform>().localPosition.y >= -1100f && GetComponent<RectTransform>().localPosition.y <= 0f)
        {
            transform.position = new Vector3(transform.position.x , Input.touches[0].position.y - dragOffset, transform.position.z);
        }

        if (GetComponent<RectTransform>().localPosition.y < -1090f)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, -1090f, 0);
        }
        if(GetComponent<RectTransform>().localPosition.y > 0)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }

    public void EndDrag()
    {
        if(GetComponent<RectTransform>().localPosition.y <= -1100f)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, -1090f, 0);
        }
        if (GetComponent<RectTransform>().localPosition.y > 0)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        StartCoroutine(WaitForEndOfFrame());
    }

    void CheckForSwipe()
    {
        if (Input.touchCount == 1 && GetComponent<RectTransform>().localPosition.y > -1090f && isDraging == false)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fPos = touch.position;
                lPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lPos = touch.position;
                if (Mathf.Abs(lPos.y - fPos.y) > minSwipeDistance && lPos.y < fPos.y)
                {
                    GetComponent<RectTransform>().localPosition = new Vector3(0, -1090f, 0);
                    fPos = Vector3.zero;
                    lPos = Vector3.zero;
                }
            }
        }
    }

    IEnumerator WaitForEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        isDraging = false;
    }
}
