using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDebug : MonoBehaviour
{
    public GameObject particleDebugObj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particleDebugObj.transform.position = Input.mousePosition;

            particleDebugObj.GetComponent<ParticleSystem>().Play();
        }
    }
}
