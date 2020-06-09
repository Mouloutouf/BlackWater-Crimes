using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleFolder : MonoBehaviour
{

    public float startingValue;
    private float xPosition;
    public RectTransform contentHolderPosition;
    public float valeurCalibrage = 401.755f;
    public float ecartDossiers = 358;
    public bool isDebugging;

    private Image image;

    void Start()
    {
        xPosition = startingValue;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
    }

    void ChangeScale()
    {
        float comparativeValue = xPosition - valeurCalibrage;

        if (isDebugging)
        {
            Debug.Log(Mathf.Abs(contentHolderPosition.offsetMin.x));
        }

        if (comparativeValue >= Mathf.Abs(contentHolderPosition.offsetMin.x))
        {
            //transform.localScale = new Vector3(0.5f + (((Mathf.Abs(contentHolderPosition.offsetMin.x)) / ecartDossiers) * 0.5f), 0.5f + (((Mathf.Abs(contentHolderPosition.offsetMin.x)) / ecartDossiers) * 0.5f), 0.5f + (((Mathf.Abs(contentHolderPosition.offsetMin.x)) / ecartDossiers) * 0.5f));

            transform.localScale = new Vector3(1 + ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 + ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 + ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f));

            image.color = new Color(1 + ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 + ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 + ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f));

            if (transform.localScale.x > 1)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }

            if (transform.localScale.y > 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            }

            if (transform.localScale.z > 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            }
        }
        else
        {

            transform.localScale = new Vector3(1 - ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 - ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 - ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f));

            image.color = new Color(1 - ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 - ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f), 1 - ((((Mathf.Abs(contentHolderPosition.offsetMin.x)) + valeurCalibrage - startingValue) / ecartDossiers) * 0.5f));

            if (transform.localScale.x < .5f)
            {
                transform.localScale = new Vector3(.5f, transform.localScale.y, transform.localScale.z);
            }

            if (transform.localScale.y < .5f)
            {
                transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
            }

            if (transform.localScale.z < .5f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, .5f);
            }
        }
    }
}
