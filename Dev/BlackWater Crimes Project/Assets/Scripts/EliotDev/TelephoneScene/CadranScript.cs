using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CadranScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float rotateSpeed;

    Quaternion initRotation;
    Quaternion lastRotation;
    float startAngle;
    float angle;
    float blockedAngle;
    float rotateAlpha;

    bool shouldReturn = false;
    bool isBlockedLeft = false;
    bool isBlockedRight = false;

    public bool numberShouldBeRecorded = false;

    [SerializeField] GameObject[] numbers;
    [SerializeField] GameObject blockL;
    [SerializeField] GameObject blockR;
    [SerializeField] GameObject placeHolderNumber;
    [SerializeField] Text dialingText;

    List<int> currentDialing = new List<int>();
    int currentNumber;
    

    // Start is called before the first frame update
    void Start()
    {
        currentNumber = 11;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && shouldReturn == false)
        {
            CadranRotation();      
        }

        if(shouldReturn == true)
        {
            CadranReturn();
        }
    }

    void CadranRotation()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                bool isNumber = ArrayContains(numbers, hit.transform.gameObject);
                if (isNumber == true)
                {
                    currentNumber = FindNumber(numbers, hit.transform.gameObject);
                    placeHolderNumber.transform.position = hit.transform.position;
                }

            }
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 vector = new Vector3(Input.GetTouch(0).position.x - screenPos.x, Input.GetTouch(0).position.y - screenPos.y, 0 - screenPos.z);
            startAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }

        else if (touch.phase == TouchPhase.Moved)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 vector = new Vector3(Input.GetTouch(0).position.x - screenPos.x, Input.GetTouch(0).position.y - screenPos.y, 0 - screenPos.z);
            angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.AngleAxis(angle - startAngle, this.transform.forward);
            newRotation.eulerAngles = new Vector3(0, 0, newRotation.eulerAngles.z);

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);

            if (isBlockedLeft == false && isBlockedRight == false)
            {
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform.gameObject == blockL)
                    {
                        isBlockedLeft = true;
                        blockedAngle = angle;
                    }
                    else if (hit.transform.gameObject == blockR)
                    {
                        isBlockedRight = true;
                        blockedAngle = angle;
                    }
                    else if (ArrayContains(numbers, hit.transform.gameObject) == true)
                    {
                        transform.rotation = initRotation * newRotation;
                    }
                }
                else
                {
                    transform.rotation = initRotation * newRotation;
                }
            }
            else if (isBlockedLeft == true || isBlockedRight == true)
            {
                if (isBlockedLeft && angle < blockedAngle && Mathf.Abs(blockedAngle - angle) > 20)
                {
                    isBlockedLeft = false;
                }
                else if (isBlockedRight && angle > blockedAngle && Mathf.Abs(blockedAngle - angle) > 20)
                {
                    isBlockedRight = false;
                }
            }
        }

        else if (touch.phase == TouchPhase.Ended)
        {
            if (currentNumber != 11)
            {
                RecordNumber();
            }
            lastRotation = transform.rotation;
            rotateAlpha = 0f;
            shouldReturn = true;
            isBlockedLeft = false;
            isBlockedRight = false;
        }
    }

    void CadranReturn()
    {
        if (rotateAlpha < 1f)
        {
            rotateAlpha += rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(lastRotation, initRotation, rotateAlpha);
        }
        else
        {
            shouldReturn = false;
        }
    }

    void RecordNumber()
    {
        if (numberShouldBeRecorded == true)
        {
            if (currentDialing.Count < 4)
            {
                currentDialing.Add(currentNumber);
            }
            else
            {
                currentDialing.Clear();
                currentDialing.Add(currentNumber);
            }
            currentNumber = 11;
            UpdateText();
        }
        placeHolderNumber.transform.localPosition = new Vector3(0, 0, 10);
    }

    void UpdateText()
    {
        if (currentDialing.Count == 1)
        {
            dialingText.text = currentDialing[0].ToString() + " _ _ _";
        }
        else if (currentDialing.Count == 2)
        {
            dialingText.text = currentDialing[0].ToString() + " " + currentDialing[1].ToString() + " _ _";
        }
        else if (currentDialing.Count == 3)
        {
            dialingText.text = currentDialing[0].ToString() + " " + currentDialing[1].ToString() + " " + currentDialing[2].ToString() + " _";
        }
        else if (currentDialing.Count == 4)
        {
            dialingText.text = currentDialing[0].ToString() + " " + currentDialing[1].ToString() + " " + currentDialing[2].ToString() + " " + currentDialing[3].ToString();
        }
    }

    bool ArrayContains(GameObject[] array, GameObject gO)
    {
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == gO)
            {
                return true;
            }
        }
        return false;
    }

    int FindNumber(GameObject[] array, GameObject gO)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == gO)
            {
                return i;
            }
        }
        return -1;
    }
}
