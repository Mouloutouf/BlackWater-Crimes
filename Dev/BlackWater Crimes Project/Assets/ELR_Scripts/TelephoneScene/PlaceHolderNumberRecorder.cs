using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderNumberRecorder : MonoBehaviour
{
    [SerializeField] GameObject blockR;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == blockR)
        {
            transform.parent.GetComponent<CadranScript>().numberShouldBeRecorded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == blockR)
        {
            transform.parent.GetComponent<CadranScript>().numberShouldBeRecorded = false;
        }
    }

}
