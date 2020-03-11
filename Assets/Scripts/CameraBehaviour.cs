using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera script for smoothly following a fixed point
public class CameraBehaviour : MonoBehaviour
{
    public int minFov;
    public int maxFov;

    //What object to follow, what object to look directly at
    public GameObject cameraTarget;
    public float moveSpeed;

    public GameObject toLookAt;
    public float rotationSpeed = 45f;


    //Using FixedUpdate to avoid jitteryness
    void FixedUpdate()
    {
        //Get the angle representing directly looking at the object, rotate towards this
        var lookRot = Quaternion.LookRotation(toLookAt.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);

        //Smoothly move towards the target position
        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position, moveSpeed * Time.deltaTime);
    }

    //Set the field of view of the camera
    public void SetFov(float value)
    {
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, Mathf.Clamp(value, minFov, maxFov), Time.deltaTime*10);
    }
}
