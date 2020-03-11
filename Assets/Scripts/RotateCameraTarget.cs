using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for rotating the camera's viewpoint relative to the car
public class RotateCameraTarget : MonoBehaviour
{
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the change of the mouse's position
        Vector3 change = Input.mousePosition - lastPos;
        lastPos = Input.mousePosition;

        //If rightclick is held, rotate the viewpoint by the change in mouse x position
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(change.y * Time.deltaTime * -0, change.x * Time.deltaTime * 15, 0);
        }

    }
}
