using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InformationButton : MonoBehaviour
{
    public string informationText;
    public string buttonName;
    public Text informationTextObject;
    public GameObject bulleTexte;
    public Camera _camera;

    public void Start()
    {
        informationTextObject.text = informationText;
        buttonName = gameObject.name;
    }

    public void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(_camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.GetComponent<BoxCollider2D>() != null && Input.GetMouseButton(0) && hits[0].transform.gameObject.name == buttonName)
        {
            bulleTexte.SetActive(true);
        }
        else
        {
            bulleTexte.SetActive(false);
        }
    }
}
