using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public List<GameEvent> events = new List<GameEvent>();

    public Color hoverColor = Color.yellow;
    private Color baseColor = Color.white;

    void Start()
    {
        
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = hoverColor;
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
    }

    void OnMouseDown()
    {
        GetComponent<EvidenceObject>().evidence.taken = true;
    }
}
