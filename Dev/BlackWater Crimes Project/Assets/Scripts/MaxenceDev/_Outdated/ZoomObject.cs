using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomObject : MonoBehaviour
{
    public Transform image;

    public Transform darkPanel;

    public float alphaAmount;
    private bool holding = false;
    private bool zooming;

    public float zoomSpeed;
    public float zoomAmount;

    private bool started;

    void Update()
    {
        if (zooming) Zoom();

        if (started)
        {
            float time = 0f;
            time += Time.deltaTime;

            if (time > 0.5f)
            {
                holding = true;
                started = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) started = true;
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            started = false;

            if (!holding)
            {
                //Debug.Log("ouais je chie dans la colle");
                zooming = true;
            }
        }
    }

    void Zoom()
    {
        Vector2 initialScale = GetComponent<RectTransform>().sizeDelta;
        //Debug.Log(initialScale);
        Vector2 finalScale = new Vector2(initialScale.x * zoomAmount, initialScale.y * zoomAmount);
        Debug.Log(finalScale);
        Vector2 scale = Vector2.Lerp(initialScale, finalScale, Time.deltaTime * zoomSpeed);
        
        image.GetComponent<RectTransform>().sizeDelta = scale;

        Darken();
    }

    void Darken()
    {
        Color alphaColor = darkPanel.GetComponent<Image>().color;
        float alphaVal = alphaColor.a;
        
        float alpha = Mathf.Lerp(alphaVal, alphaAmount, Time.deltaTime * zoomSpeed);
        Color finalAlphaColor = new Color(0, 0, 0, alpha);
        darkPanel.GetComponent<Image>().color = finalAlphaColor;
    }

    IEnumerator CheckTime()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        
        holding = true;
    }
}