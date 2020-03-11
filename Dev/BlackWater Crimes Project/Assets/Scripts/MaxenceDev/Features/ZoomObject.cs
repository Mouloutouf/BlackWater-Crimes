using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomObject : MonoBehaviour
{
    public Transform image;

    public Transform darkPanel;

    public float alphaAmount;
    private bool holding;
    private bool zooming;

    public float zoomSpeed;
    public float zoomAmount;

    void Update()
    {
        if (zooming) Zoom();
    }

    void OnMouseDown()
    {
        StartCoroutine(CheckTime());
    }

    void OnMouseUp()
    {
        if (!holding) zooming = true;
    }

    void Zoom()
    {
        Vector2 initialScale = GetComponent<RectTransform>().sizeDelta;
        Vector2 finalScale = new Vector2(initialScale.x * zoomAmount, initialScale.y * zoomAmount);

        Vector2 scale = Vector2.Lerp(initialScale, finalScale, zoomSpeed);
        image.GetComponent<RectTransform>().sizeDelta = scale;

        Darken();
    }

    void Darken()
    {
        Color alphaColor = darkPanel.GetComponent<Image>().color;
        float alphaVal = alphaColor.a;

        float alpha = Mathf.Lerp(alphaVal, alphaAmount, 1f);
        Color finalAlphaColor = new Color(0, 0, 0, alpha);
        darkPanel.GetComponent<Image>().color = finalAlphaColor;
    }

    IEnumerator CheckTime()
    {
        yield return new WaitForSeconds(0.5f);

        holding = true;
    }
}
