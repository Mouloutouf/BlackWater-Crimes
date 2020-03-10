using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithTouch : MonoBehaviour
{

    public float rotateSpeed = .001f;
    public Joystick joystick;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = -joystick.Horizontal * rotateSpeed;
        verticalMove = joystick.Vertical * rotateSpeed;
        transform.Rotate(Vector3.up, horizontalMove, Space.World);
        transform.Rotate(Vector3.right, verticalMove, Space.World);

        if (GetComponent<Renderer>().isVisible)
        {
            Debug.Log("Object is visible");
        }
        else Debug.Log("Object is no longer visible");
    }
}
