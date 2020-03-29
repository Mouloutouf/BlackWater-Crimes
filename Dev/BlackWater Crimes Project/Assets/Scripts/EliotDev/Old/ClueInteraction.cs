using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueInteraction : MonoBehaviour
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

    [SerializeField] AudioClip photoShotSound;
    [SerializeField] AudioClip photoSavedSound;
    [SerializeField] AudioClip photoReplacedSound;
    [SerializeField] AudioClip fingerprintDiscoveredSound;

    [ExecuteInEditMode]
    void OnEnable()
    {
        fingerprintToggle.isOn = false;
        fingerprintMode = false;
        saveText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentClueHolder != null && canRotate == true)
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

        if (fingerprintMode == true && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.transform.gameObject.tag == "Clue" && hit.transform.gameObject.GetComponent<Clue>().fingerprint == true && touch.phase == TouchPhase.Moved)
                {
                    if (hit.transform.gameObject.GetComponent<Clue>().fingerprintDiscovered == false)
                    {
                        if (hit.transform.gameObject.GetComponent<Clue>().fingerprintAlpha < 1f)
                        {
                            hit.transform.gameObject.GetComponent<Clue>().fingerprintAlpha += .8f * Time.deltaTime;
                        }
                        else
                        {
                            hit.transform.gameObject.GetComponent<Clue>().fingerprintDiscovered = true;
                            hit.transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                            GetComponent<AudioSource>().PlayOneShot(fingerprintDiscoveredSound);
                        }
                    }
                }
            }
        }
    }

    public void TakePhoto()
    {
        GetComponent<AudioSource>().PlayOneShot(photoShotSound);

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));

        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.transform.gameObject.tag == "Clue")
            {
                if (hit.transform.gameObject.GetComponent<Clue>().fingerprint == false && hit.transform.gameObject.GetComponent<Clue>().photographed == false)
                {
                    hit.transform.gameObject.GetComponent<Clue>().photographed = true;
                    GetComponent<AudioSource>().PlayOneShot(photoSavedSound);
                    StopAllCoroutines();
                    StartCoroutine(TextDisplay("Photo Saved"));
                    TakeScreenshot(hit.transform.gameObject.name + ".png");
                }
                else if (hit.transform.gameObject.GetComponent<Clue>().fingerprint == false && hit.transform.gameObject.GetComponent<Clue>().photographed == true)
                {
                    GetComponent<AudioSource>().PlayOneShot(photoReplacedSound);
                    StopAllCoroutines();
                    StartCoroutine(TextDisplay("Photo Replaced"));
                    TakeScreenshot(hit.transform.gameObject.name + ".png");
                }
                else if (hit.transform.gameObject.GetComponent<Clue>().fingerprint == true && hit.transform.gameObject.GetComponent<Clue>().fingerprintDiscovered && hit.transform.gameObject.GetComponent<Clue>().photographed == false)
                {
                    hit.transform.gameObject.GetComponent<Clue>().photographed = true;
                    GetComponent<AudioSource>().PlayOneShot(photoSavedSound);
                    StopAllCoroutines();
                    StartCoroutine(TextDisplay("Photo Saved"));
                    TakeScreenshot(hit.transform.gameObject.name + ".png");
                }
                else if (hit.transform.gameObject.GetComponent<Clue>().fingerprint == true && hit.transform.gameObject.GetComponent<Clue>().fingerprintDiscovered && hit.transform.gameObject.GetComponent<Clue>().photographed == true)
                {
                    GetComponent<AudioSource>().PlayOneShot(photoReplacedSound);
                    StopAllCoroutines();
                    StartCoroutine(TextDisplay("Photo Replaced"));
                    TakeScreenshot(hit.transform.gameObject.name + ".png");
                }
            }
        }
    }

    public void FingerprintMode()
    {
        fingerprintMode = fingerprintToggle.isOn;
        if(fingerprintMode == true)
        {
            canRotate = false;
        }
        else
        {
            canRotate = true;
        }
    }

    IEnumerator TextDisplay(string textToDisplay)
    {
        saveText.SetActive(true);
        saveText.GetComponent<Text>().text = textToDisplay;
        yield return new WaitForSeconds(2f);
        saveText.SetActive(false);
    }

    void TakeScreenshot(string imageName)
    {
        ScreenCapture.CaptureScreenshot(imageName);
    }
}
