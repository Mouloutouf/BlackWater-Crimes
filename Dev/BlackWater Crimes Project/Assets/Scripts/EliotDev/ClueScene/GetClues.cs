using System.Collections;
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
    [SerializeField] AudioMixerGroup lowPassMixer;

    public AudioSource musicAudio;
    public AudioSource soundAudio;
    public AudioClip zoomSound;

    private bool isPhotoDisplayed;

    Vector3 zoomTransformInitPos;

    public bool clueIsZoomed = false;
    public bool canZoom = true;

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
        if (Input.touchCount == 1 && clueIsZoomed == false && actualClue == null && canZoom == true)
        {
            CastRay(Input.touches[0].position);
        }
        else if(Input.GetMouseButtonDown(0) && clueIsZoomed == false && actualClue == null && canZoom == true)
        {
            CastRay(Input.mousePosition);
        }
    }

    void CheckForClueHolderMove()
    {
        if (actualClue != null) actualClue.GetComponent<ClueHolder>().specificZoomRotationQuaternion = Quaternion.Euler(actualClue.GetComponent<ClueHolder>().specificZoomRotation);

        // Zoom Movement
        if (actualClue != null && clueIsZoomed == true)
        {
            // Position
            if (actualClue.transform.position != actualZoomTransform.position)
            {
                zoomAlpha += .01f * zoomSpeed * Time.deltaTime;
                actualClue.transform.position = Vector3.Lerp(initCluePos, actualZoomTransform.position, zoomAlpha);
            }
            else actualClue.GetComponent<EvidenceObject>().isZoomed = true; // Allow the Text to be displayed

            // Rotation
            if (actualClue.GetComponent<ClueHolder>().hasSpecificRotation == true && actualClue.transform.rotation != actualClue.GetComponent<ClueHolder>().specificZoomRotationQuaternion)
            {
                rotateAlpha += .01f * rotateSpeed * Time.deltaTime;
                actualClue.transform.rotation = Quaternion.Slerp(initClueRot, actualClue.GetComponent<ClueHolder>().specificZoomRotationQuaternion, rotateAlpha);
            }
        }

        // Dezoom Movement
        else if (actualClue != null & clueIsZoomed == false)
        {
            // Position
            if (actualClue.transform.position != initCluePos)
            {
                zoomAlpha += .01f * zoomSpeed * Time.deltaTime;
                actualClue.transform.position = Vector3.Lerp(actualZoomTransform.position, initCluePos, zoomAlpha);
            }

            // Rotation
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

    void CastRay(Vector2 rayPos) 
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(rayPos);

        if(Physics.Raycast(ray, out hit, 2000f))
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
        overlayClueCanvas.GetComponent<EvidenceInteraction>().currentEvidenceHeld = actualClue;
        
        musicAudio.outputAudioMixerGroup = lowPassMixer;
        soundAudio.PlayOneShot(zoomSound);
    }

    public void DezoomClue()
    {
        lastRot = actualClue.transform.rotation;

        clueIsZoomed = false;

        zoomAlpha = 0f;
        rotateAlpha = 0f;

        sceneCanvas.SetActive(true);
        cameraCanvas.SetActive(false);
        overlayClueCanvas.GetComponent<EvidenceInteraction>().currentEvidenceHeld = null;
        actualClue.GetComponent<EvidenceObject>().isZoomed = false;
        if(actualClue.GetComponent<EvidenceObject>().displayTextComponent != null) actualClue.GetComponent<EvidenceObject>().displayTextComponent.transform.parent.gameObject.SetActive(false);
        overlayClueCanvas.SetActive(false);
        musicAudio.outputAudioMixerGroup = null;
    }
}
