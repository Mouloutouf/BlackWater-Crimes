using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InformationButton : MonoBehaviour
{
    public string informationText;
    public Text informationTextObject;
    public GameObject bulleTexte;
    public ScrollRect scrollRect;

    public void Start()
    {
        informationTextObject.text = informationText;
    }

    public void ShowBulle()
    {
        bulleTexte.SetActive(true);
        scrollRect.enabled = false;
    }

    public void HideBulle()
    {
        bulleTexte.SetActive(false);
        scrollRect.enabled = true;
    }
}
