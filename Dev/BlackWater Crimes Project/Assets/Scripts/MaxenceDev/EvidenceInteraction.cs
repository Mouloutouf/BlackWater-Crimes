using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceInteraction : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject saveText;
    [SerializeField] Toggle fingerprintToggle;
    [SerializeField] Joystick joystick;
    [SerializeField] float rotateSpeed;

    public GameObject currentClueHolder;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool canRotate = true;
    bool fingerprintMode = false;

    public AudioClip photoShotSound;
    public AudioClip photoSavedSound;
    public AudioClip photoReplacedSound;
    public AudioClip fingerprintDiscoveredSound;

    [ExecuteInEditMode]
    void OnEnable()
    {
        fingerprintToggle.isOn = false;
        fingerprintMode = false;
        saveText.SetActive(false);
    }
    
    void Update()
    {
        ObjectRotation();

        if (fingerprintMode == true && Input.touchCount == 1)
        {
            ClueReveal();
        }
    }

    void ObjectRotation()
    {
        if (currentClueHolder != null && canRotate == true)
        {
            if (currentClueHolder.GetComponent<ClueHolder>().blockHorizontalRotation == false)
            {
                horizontalMove = -joystick.Horizontal * rotateSpeed * Time.deltaTime;
                currentClueHolder.transform.Rotate(Vector3.up, horizontalMove, Space.World);
            }

            if (currentClueHolder.GetComponent<ClueHolder>().blockVerticalRotation == false)
            {
                verticalMove = joystick.Vertical * rotateSpeed * Time.deltaTime;
                currentClueHolder.transform.Rotate(Vector3.right, verticalMove, Space.World);
            }
        }
    }

    void ClueReveal()
    {
        Touch touch = Input.GetTouch(0);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.transform.gameObject.tag == "Clue")
            {
                Evidence evidence = hit.transform.parent.gameObject.GetComponent<EvidenceObject>().data;
                EvidenceObject evidenceObject = hit.transform.parent.gameObject.GetComponent<EvidenceObject>();

                if (evidence.hasIntel == true && evidence.intelRevealed == false && touch.phase == TouchPhase.Moved)
                {
                    if (evidenceObject.intelAlpha < 1f)
                    {
                        evidenceObject.intelAlpha += .8f * Time.deltaTime;
                    }
                    else
                    {
                        evidence.intelRevealed = true;
                        hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                        GetComponent<AudioSource>().PlayOneShot(fingerprintDiscoveredSound);
                    }
                }
            }
        }
    }

    public void TakePhoto()
    {
        GetComponent<AudioSource>().PlayOneShot(photoShotSound);

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.transform.gameObject.tag == "Clue")
            {
                Evidence evidence = hit.transform.parent.gameObject.GetComponent<EvidenceObject>().data;

                TakeScreenshot(evidence, hit);
            }
        }
    }

    void TakeScreenshot(Evidence _evidence, RaycastHit _hit)
    {
        if (_evidence.hasIntel && !_evidence.intelRevealed) return;

        StopAllCoroutines();

        if (!_evidence.photographed)
        {
            GetComponent<AudioSource>().PlayOneShot(photoSavedSound);
            StartCoroutine(DisplayText("Photo Saved"));
            _evidence.photographed = true;
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(photoReplacedSound);
            StartCoroutine(DisplayText("Photo Replaced"));
        }

        ScreenCapture.CaptureScreenshot(_hit.transform.gameObject.name + ".png");
    }

    IEnumerator DisplayText(string textToDisplay)
    {
        saveText.SetActive(true);
        saveText.GetComponent<Text>().text = textToDisplay;
        yield return new WaitForSeconds(2f);
        saveText.SetActive(false);
    }

    public void FingerprintMode()
    {
        fingerprintMode = fingerprintToggle.isOn;
        if (fingerprintMode == true)
        {
            canRotate = false;
        }
        else
        {
            canRotate = true;
        }
    }
}
