using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InformationButton : MonoBehaviour
{
    public string infoKey;
    public string buttonName;
    public Localisation informationKey;
    public GameObject bulleTexte;
    public ScrollRect scrollRect;

    public void Start()
    {
        informationKey.key = infoKey;
        informationKey.RefreshText();
        
        buttonName = gameObject.name;
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
