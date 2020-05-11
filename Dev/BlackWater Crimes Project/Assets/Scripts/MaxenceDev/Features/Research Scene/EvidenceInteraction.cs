﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class EvidenceInteraction : MonoBehaviour
{
    private GameData gameData;

    public Locations thisSceneLocation;

    public Camera cam;
    [SerializeField] GameObject saveText;
    [SerializeField] GameObject fingerprintFilter;
    [SerializeField] Toggle fingerprintToggle;
    [SerializeField] Button returnButton;
    [SerializeField] Joystick joystick;

    public float rotateSpeed;

    public GameObject currentEvidenceHeld { get; set; }

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool canRotate = true;
    bool fingerprintMode = false;

    public AudioSource soundAudio;

    public AudioClip photoShotSound;
    public AudioClip photoSavedSound;
    public AudioClip photoReplacedSound;
    public AudioClip fingerprintDiscoveredSound;
    [SerializeField] Vector2 values;
    [SerializeField] Vector2 sizes;

    public VibrateSystem vibrateSystem;

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
        values.x = (Screen.width * values.x) / 1480f;
        values.y = (Screen.height * values.y) / 720;
    }

    void Update()
    {
        ObjectRotation();

        if (fingerprintMode == true && (Input.touchCount == 1 || Input.GetMouseButton(0)))
        {
            if (currentEvidenceHeld.GetComponent<EvidenceObject>().data.hasIntels) IntelReveal();
        }
    }

    void ObjectRotation()
    {
        if (currentEvidenceHeld != null && canRotate == true)
        {
            if (currentEvidenceHeld.GetComponent<ClueHolder>().blockHorizontalRotation == false)
            {
                horizontalMove = -joystick.Horizontal * rotateSpeed * Time.deltaTime;
                currentEvidenceHeld.transform.Rotate(cam.transform.up, horizontalMove, Space.World);
            }

            if (currentEvidenceHeld.GetComponent<ClueHolder>().blockVerticalRotation == false)
            {
                verticalMove = joystick.Vertical * rotateSpeed * Time.deltaTime;
                currentEvidenceHeld.transform.Rotate(cam.transform.right, verticalMove, Space.World);
            }
        }
    }

    void IntelReveal()
    {
        Vector3 inputPos;
        if(Input.touchCount > 0) 
        {
            inputPos = Input.GetTouch(0).position;
        }
        else inputPos = Input.mousePosition;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(inputPos);

        if (Physics.Raycast(ray, out hit, 500f))
        {
            if (hit.transform.gameObject.tag == "Clue")
            {
                string name = hit.transform.GetComponent<IntelObject>().myName;

                Evidence evidence = hit.transform.parent.gameObject.GetComponent<EvidenceObject>().data;

                if (evidence.hasIntels == true && (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
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

                                Debug.Log(intel.name + intel.revealed);
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
        fingerprintToggle.isOn = false;
        fingerprintMode = false;

        currentEvidenceHeld.GetComponent<EvidenceObject>().isZoomed = false;
        if (currentEvidenceHeld.GetComponent<EvidenceObject>().displayTextComponent != null) 
            currentEvidenceHeld.GetComponent<EvidenceObject>().displayTextComponent.transform.parent.gameObject.SetActive(false);
        
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
        // Check if Evidence has unrevealed Intels, if so returns

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

        // Unlocks the Evidence and displays Feedbacks

        StopAllCoroutines();

        if (!_evidence.photographed)
        {
            soundAudio.PlayOneShot(photoSavedSound);
            vibrateSystem.PhoneVibrate();
            StartCoroutine(DisplayText("Photo Saved"));

            _evidence.photographed = true;
            _evidence.unlockedData = true;
            gameData.allEvidences[thisSceneLocation].Add(_evidence); // IMPORTANT : This is where we unlock the evidence in the List

            gameData.newStuff = true;
        }
        else
        {
            soundAudio.PlayOneShot(photoReplacedSound);
            vibrateSystem.PhoneVibrate();
            StartCoroutine(DisplayText("Photo Replaced"));
            gameData.allEvidences[thisSceneLocation].Remove(_evidence);
            gameData.allEvidences[thisSceneLocation].Add(_evidence);
        }

        // Takes the Screenshot and saves it under the right File Path

        string filePath;
        string fileName = _hit.transform.parent.GetComponent<EvidenceObject>().data.codeName;
        fileName = fileName.Replace(" ", "");

        if (Application.platform == RuntimePlatform.WindowsEditor || (Application.platform == RuntimePlatform.Android && EditorApplication.isPlaying))
        {
            filePath = "Assets/Graphs/Sprites/Screenshots/" + fileName + ".png";
            Debug.Log("Using Editor Folder");

            if (File.Exists(filePath)) File.Delete(filePath);

            ScreenCapture.CaptureScreenshot(filePath);

            StartCoroutine(CheckFile(filePath, fileName, _evidence));
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            filePath = Application.persistentDataPath + fileName + ".png";
            Debug.Log("Using Windows Folder");

            if (File.Exists(filePath)) File.Delete(filePath);

            ScreenCapture.CaptureScreenshot(filePath);

            StartCoroutine(CheckFile(filePath, fileName, _evidence));
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            filePath = fileName + ".png";
            Debug.Log("Using Android Folder. If you're using Unity Editor, the photo saver won't work! Please check Is In Editor");

            if (File.Exists(Application.persistentDataPath + "/" + filePath)) File.Delete(Application.persistentDataPath + "/" + filePath);

            ScreenCapture.CaptureScreenshot(filePath);

            StartCoroutine(CheckFile(Application.persistentDataPath + "/" + filePath, fileName, _evidence));
        }
    }

    IEnumerator CheckFile(string filePath, string fileName, Evidence _evidence)
    {
        if (File.Exists(filePath))
        {
            CreateSprite(filePath, fileName, _evidence);
        }
        else
        {
            //returnButton.interactable = false;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(CheckFile(filePath, fileName, _evidence));
        }
    }

    void CreateSprite(string filePath, string fileName, Evidence _evidence)
    {
        Texture2D texture;
        byte[] fileBytes;
        fileBytes = File.ReadAllBytes(filePath);
        texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        texture.LoadImage(fileBytes);
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Sprite sp = Sprite.Create(texture, new Rect(values.x, values.y, texture.width / sizes.x, texture.height / sizes.y), new Vector2(0.5f, 0.5f));
        /*
        if(AssetDatabase.FindAssets(fileName + "Cropped.asset") != null)
        {
            AssetDatabase.DeleteAsset("Assets/Graphs/Sprites/Screenshots/CropedSprites/" + fileName + "Cropped.asset");
        }
        AssetDatabase.CreateAsset(sp, "Assets/Graphs/Sprites/Screenshots/CropedSprites/" + fileName + "Cropped.asset");
        */
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

            currentEvidenceHeld.GetComponent<EvidenceObject>().canShowText = false;
        }
        else
        {
            fingerprintFilter.SetActive(false);
            canRotate = true;

            currentEvidenceHeld.GetComponent<EvidenceObject>().canShowText = true;
        }
    }
}
