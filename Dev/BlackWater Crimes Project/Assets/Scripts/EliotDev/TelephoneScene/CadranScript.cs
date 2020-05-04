using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CadranScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float rotateSpeed;

    Vector3 inputPos;
    Quaternion initRotation;
    Quaternion lastRotation;
    float startAngle;
    float angle;
    float blockedAngle;
    float rotateAlpha;

    bool shouldReturn = false;
    bool isBlockedLeft = false;
    bool isBlockedRight = false;
    bool passedByBegan = false;
    bool hasFoundANumber = false;

    [SerializeField] GameObject backgroundCadran;
    [SerializeField] GameObject[] numbers;
    [SerializeField] LayerMask numberLayer;
    [SerializeField] GameObject blockL;
    [SerializeField] GameObject blockR;
    [SerializeField] GameObject placeHolderNumber;

    [SerializeField] CurrentDialingScript dialingScript;
    int currentNumber;

    public SoundSystem soundSystem;
    public AudioClip clip;

    void Start()
    {
        currentNumber = 11;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldReturn) //Android Touch
        {
            if(Input.touchCount == 1) inputPos = Input.touches[0].position;
            else inputPos = Input.mousePosition;

            RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(inputPos), cam.transform.forward, 100f);
            
            if(hits.Length > 0)
            {
                foreach(RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        if (hit.transform.gameObject == backgroundCadran)
                        {
                            CadranRotation();   
                        }
                    }
                    else
                    {
                        CadranReturn();
                    }
                }
            }
            else
            {
                CadranReturn();
            }
        }

        if(shouldReturn)
        {
            ReturnRotation();
        }
    }

    void CadranRotation()
    {
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            passedByBegan = true;

            RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(inputPos), cam.transform.forward, 100f);
        
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    bool isNumber = ArrayContains(numbers, hit.transform.gameObject);

                    if (isNumber)
                    {
                        currentNumber = FindNumber(numbers, hit.transform.gameObject);
                        placeHolderNumber.transform.position = hit.transform.position;
                        hasFoundANumber = true;
                    }
                    else if(hit == hits[hits.Length -1] && !hasFoundANumber)
                    {
                        placeHolderNumber.transform.position = hit.transform.position;
                    }
                }
            }

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 vector = new Vector3(inputPos.x - screenPos.x, inputPos.y - screenPos.y, 0 - screenPos.z);

            startAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }

        else if (((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)) && passedByBegan)
        {

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 vector = new Vector3(inputPos.x - screenPos.x, inputPos.y - screenPos.y, 0 - screenPos.z);
            
            angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

            Quaternion newRotation = Quaternion.AngleAxis(angle - startAngle, this.transform.forward);
            newRotation.eulerAngles = new Vector3(0, 0, newRotation.eulerAngles.z);

            if (!isBlockedLeft && !isBlockedRight)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(placeHolderNumber.transform.position, .6f);

                foreach(Collider2D collider in hitColliders)
                {
                    if(collider.gameObject == blockL)
                    {
                        isBlockedLeft = true;
                        blockedAngle = newRotation.eulerAngles.z;
                        return;
                    }
                    else if(collider.gameObject == blockR)
                    {
                        isBlockedRight = true;
                        blockedAngle = newRotation.eulerAngles.z;
                        return;
                    }
                }

                transform.rotation = initRotation * newRotation;
            }

            else if (isBlockedLeft || isBlockedRight)
            {
                if(isBlockedLeft && newRotation.eulerAngles.z < blockedAngle)
                {
                    isBlockedLeft = false;
                    transform.rotation = initRotation * newRotation;
                }
                else if(isBlockedRight && newRotation.eulerAngles.z > blockedAngle)
                {
                    isBlockedRight = false;
                    transform.rotation = initRotation * newRotation;
                }
            }
        }

        else if ((Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Canceled)) || Input.GetMouseButtonUp(0))
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(placeHolderNumber.transform.position, .6f);

            foreach(Collider2D collider in hitColliders)
            {
                if(collider.gameObject == blockR && currentNumber != 11)
                {
                    dialingScript.RecordNumber(currentNumber);

                    soundSystem.PlaySound(clip);
                }
            }

            CadranReturn();
        }
    }

    void CadranReturn()
    {
        passedByBegan = false;
        placeHolderNumber.transform.localPosition = new Vector3(0, 0, 10);
        lastRotation = transform.rotation;
        rotateAlpha = 0f;
        shouldReturn = true;
        isBlockedLeft = false;
        shouldReturn = true;
    } 

    void ReturnRotation()
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
