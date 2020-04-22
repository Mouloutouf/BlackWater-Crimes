using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class EvidenceInteraction : MonoBehaviour
{
    private GameData gameData;

    public Locations thisSceneLocation;

    [SerializeField] Camera cam;
    [SerializeField] GameObject saveText;
    [SerializeField] GameObject fingerprintFilter;
    [SerializeField] Toggle fingerprintToggle;
    [SerializeField] Button returnButton;
    [SerializeField] Joystick joystick;
    [SerializeField] float rotateSpeed;

    public GameObject currentClueHolder;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool canRotate = true;
    bool fingerprintMode = false;

    public AudioSource soundAudio;

    public AudioClip photoShotSound;
    public AudioClip photoSavedSound;
    public AudioClip photoReplacedSound;
    public AudioClip fingerprintDiscoveredSound;

    [SerializeField] bool isInEditor;
    [SerializeField] bool windowsBuild;

    [SerializeField] Vector2 values;
    [SerializeField] Vector2 sizes;

    [ExecuteInEditMode]
    void OnEnable()
    {
        fingerprintToggle.isOn = false;
        fingerprintMode = false;
        saveText.SetActive(false);
    }
    
    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
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
                string name = hit.transform.GetComponent<IntelObject>().myName;

                Evidence evidence = hit.transform.parent.gameObject.GetComponent<EvidenceObject>().data;

                if (evidence.hasIntels == true && touch.phase == TouchPhase.Moved)
                {
                    foreach (Intel intel in evidence.intels)
                    {
                        if (intel.name == name && !intel.revealed)
                        {
                            if (intel.intelAlpha < 1f)
                            {
                                intel.intelAlpha += .8f * Time.deltaTime;
                                Color tempColor = hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().color;
                                tempColor.a = intel.intelAlpha + .2f;
                                hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().color = tempColor;
                            }
                            else
                            {
                                intel.revealed = true;
                                hit.transform.gameObject.GetComponentsInChildren<ParticleSystem>()[0].Play();
                                hit.transform.gameObject.GetComponentsInChildren<ParticleSystem>()[1].Stop();
                                soundAudio.PlayOneShot(fingerprintDiscoveredSound);
                            }
                        }
                    }
                }
            }
        }
    }

    public void TakePhoto()
    {
        soundAudio.PlayOneShot(photoShotSound);

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
        if (!_evidence.intelSelf && _evidence.hasIntels)
        {
            int val = 0;

            foreach (Intel _intel in _evidence.intels)
            {
                val = _evidence.intels.Count;

                if (!_intel.revealed) val--;
            }

            if (val == 0) return;
        }

        StopAllCoroutines();

        if (!_evidence.photographed)
        {
            soundAudio.PlayOneShot(photoSavedSound);
            Handheld.Vibrate();
            StartCoroutine(DisplayText("Photo Saved"));

            _evidence.photographed = true;
            _evidence.unlockedData = true;
            gameData.allEvidences[thisSceneLocation].Add(_evidence); // IMPORTANT : This is where we unlock the evidence in the List
        }
        else
        {
            soundAudio.PlayOneShot(photoReplacedSound);
            Handheld.Vibrate();
            StartCoroutine(DisplayText("Photo Replaced"));
            gameData.allEvidences[thisSceneLocation].Remove(_evidence);
            gameData.allEvidences[thisSceneLocation].Add(_evidence);
        }

        string filePath;

        if (isInEditor)
        {
            filePath = "Assets/Graphs/Sprites/Screenshots/" + _hit.transform.gameObject.name + ".png";
            Debug.Log("Using Editor Folder");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        else if (windowsBuild)
        {
            filePath = Application.persistentDataPath + _hit.transform.gameObject.name + ".png";
            Debug.Log("Using Windows Folder");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        else
        {
            filePath = _hit.transform.gameObject.name + ".png";
            Debug.Log("Using Android Folder. If you're using Unity Editor, the photo saver won't work! Please check Is In Editor");

            if (File.Exists(Application.persistentDataPath + "/" + filePath))
            {
                File.Delete(Application.persistentDataPath + "/" + filePath);
            }
        }    

        ScreenCapture.CaptureScreenshot(filePath);

        if (isInEditor)
        {
            StartCoroutine(CheckFile(filePath, _evidence));
        }
        if (windowsBuild)
        {
            StartCoroutine(CheckFile(filePath, _evidence));
        }
        else
        {
            StartCoroutine(CheckFile(Application.persistentDataPath + "/" + filePath, _evidence));
        }
    }

    IEnumerator CheckFile(string filePath, Evidence _evidence)
    {
        if (File.Exists(filePath))
        {
            CreateSprite(filePath, _evidence);
        }
        else
        {
            //returnButton.interactable = false;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(CheckFile(filePath, _evidence));
        }
    }

    void CreateSprite(string filePath, Evidence _evidence)
    {
        Texture2D texture;
        byte[] fileBytes;
        fileBytes = File.ReadAllBytes(filePath);
        texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        texture.LoadImage(fileBytes);
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Sprite sp = Sprite.Create(texture, new Rect(values.x, values.y, texture.width / sizes.x, texture.height / sizes.y), new Vector2(0.5f, 0.5f));
        _evidence.photo = sp;
        returnButton.interactable = true;
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
            fingerprintFilter.SetActive(true);
            canRotate = false;
        }
        else
        {
            fingerprintFilter.SetActive(false);
            canRotate = true;
        }
    }
}
