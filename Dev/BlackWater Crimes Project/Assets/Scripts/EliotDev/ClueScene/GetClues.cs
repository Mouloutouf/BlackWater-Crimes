﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GetClues : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] Transform zoomTransform;
    [SerializeField] GameObject sceneCanvas;
    [SerializeField] GameObject cameraCanvas;
    [SerializeField] GameObject overlayClueCanvas;
    [SerializeField] GameObject photoCanvas;
    [SerializeField] AudioMixerGroup lowPassMixer;

    private bool isPhotoDisplayed;

    Vector3 zoomTransformInitPos;

    bool clueIsZoomed = false;

    float zoomAlpha = 0f;
    float rotateAlpha = 0f;

    GameObject actualClue;
    Transform actualZoomTransform;
    Quaternion lastRot;
    Vector3 initCluePos;
    Quaternion initClueRot;

    [SerializeField] float zoomSpeed;
    [SerializeField] float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        zoomTransformInitPos = zoomTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCast();

        CheckForClueHolderMove();
    }

    void CheckForCast()
    {
        if (Input.touchCount == 1 && clueIsZoomed == false && actualClue == null)
        {
            CastRay();
        }
    }

    void CheckForClueHolderMove()
    {
        if (actualClue != null && clueIsZoomed == true)
        {
            if (actualClue.transform.position != actualZoomTransform.position)
            {
                zoomAlpha += .01f * zoomSpeed * Time.deltaTime;
                actualClue.transform.position = Vector3.Lerp(initCluePos, actualZoomTransform.position, zoomAlpha);
            }

            if (actualClue.GetComponent<ClueHolder>().hasSpecificRotation == true && actualClue.transform.rotation != actualClue.GetComponent<ClueHolder>().specificZoomRotationQuaternion)
            {
                rotateAlpha += .01f * rotateSpeed * Time.deltaTime;
                actualClue.transform.rotation = Quaternion.Slerp(initClueRot, actualClue.GetComponent<ClueHolder>().specificZoomRotationQuaternion, rotateAlpha);
            }
        }

        else if (actualClue != null & clueIsZoomed == false)
        {
            if (actualClue.transform.position != initCluePos)
            {
                zoomAlpha += .01f * zoomSpeed * Time.deltaTime;
                actualClue.transform.position = Vector3.Lerp(actualZoomTransform.position, initCluePos, zoomAlpha);
            }

            if (actualClue.transform.rotation != initClueRot)
            {
                rotateAlpha += .01f * rotateSpeed * Time.deltaTime;
                actualClue.transform.rotation = Quaternion.Slerp(lastRot, initClueRot, rotateAlpha);
            }

            if ((actualClue.transform.position == initCluePos && actualClue.transform.rotation == initClueRot) || rotateAlpha > 1f)
            {
                zoomTransform.position = zoomTransformInitPos;
                actualZoomTransform = null;
                initCluePos = Vector3.zero;
                initClueRot = Quaternion.identity;
                lastRot = Quaternion.identity;
                actualClue = null;
            }
        }
    }

    void CastRay() 
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.touches[0].position);

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.transform.gameObject.tag == "ClueHolder")
            {
                ZoomClue(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.tag == "Clue")
            {
                ZoomClue(hit.transform.parent.gameObject);
            }
        }
    }

    void ZoomClue(GameObject clue)
    {
        clueIsZoomed = true;

        zoomAlpha = 0f;
        rotateAlpha = 0f;

        initCluePos = clue.transform.position;
        initClueRot = clue.transform.rotation;
        actualClue = clue;

        if (clue.GetComponent<ClueHolder>() != null)
        {
            switch (clue.GetComponent<ClueHolder>().size)
            {
                case ClueHolderSize.Small:
                    actualZoomTransform = zoomTransform;
                    actualZoomTransform.position += new Vector3(0, 0, -1);
                    break;

                case ClueHolderSize.Medium:
                    actualZoomTransform = zoomTransform;
                    break;

                case ClueHolderSize.Large:
                    actualZoomTransform = zoomTransform;
                    actualZoomTransform.position += new Vector3(0, 0, 1);
                    break;

                case ClueHolderSize.Other:
                    actualZoomTransform = zoomTransform;
                    actualZoomTransform.position = clue.GetComponent<ClueHolder>().specificZoomPosition;
                    break;
            }
        }

        else
        {
            Debug.Log("This clue has no Clue Holder Script, please add one and specify the size of this clue");
        }

        cameraCanvas.SetActive(true);
        sceneCanvas.SetActive(false);
        overlayClueCanvas.SetActive(true);
        overlayClueCanvas.GetComponent<ClueInteraction>().currentClueHolder = actualClue;
        cam.GetComponent<AudioSource>().outputAudioMixerGroup = lowPassMixer;
    }

    public void DezoomClue()
    {
        lastRot = actualClue.transform.rotation;

        clueIsZoomed = false;

        zoomAlpha = 0f;
        rotateAlpha = 0f;

        sceneCanvas.SetActive(true);
        cameraCanvas.SetActive(false);
        overlayClueCanvas.GetComponent<ClueInteraction>().currentClueHolder = null;
        overlayClueCanvas.SetActive(false);
        cam.GetComponent<AudioSource>().outputAudioMixerGroup = null;
    }

    //Photo UI
    public void DisplayUIPhoto()
    {
        if (!isPhotoDisplayed)
        {
            photoCanvas.SetActive(true);
            isPhotoDisplayed = true;
        }
        else
        {
            photoCanvas.SetActive(false);
            isPhotoDisplayed = false;
        }
    }
}
