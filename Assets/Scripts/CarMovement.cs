using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{ 
    public GameObject[] wheels;
    public ParticleSystem[] particleSystems;

    public CameraBehaviour camInterface;

    private Rigidbody rb;
    private float currentTurningSpeed = 0;
    public float turningSpeed = 200;

    private bool hasTurned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Update visuals
        UpdateVelocityDisplay();

        hasTurned = false;
    }

    private void LateUpdate()
    {
        if (!hasTurned)
        {
            currentTurningSpeed = Mathf.Lerp(currentTurningSpeed, 0, turningSpeed * Time.deltaTime);
        }
    }

    public void Turn(float speed)
    {
        //Have some delay in changing rotation speed
        currentTurningSpeed = Mathf.Lerp(currentTurningSpeed, speed, turningSpeed * Time.deltaTime);

        //Turn the car by the current rotation speed
        transform.Rotate(new Vector3(0, 135 * currentTurningSpeed * Time.deltaTime, 0));

        //Update rotation of each wheel 
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.LookAt(wheel.transform.position + transform.forward * 2 + transform.right * currentTurningSpeed);
        }

        hasTurned = true;
    }

    public void Accelerate(float speed)
    {
        rb.velocity += transform.forward * speed * Time.deltaTime;
    }

    private void UpdateVelocityDisplay()
    {
        //Create trail particles if we're travelling fast enough
        foreach (ParticleSystem ps in particleSystems)
        {
            if (rb.velocity.magnitude > 5f && !ps.isPlaying)
            {
                ps.Play();
            }
            else if (rb.velocity.magnitude <= 5f)
            {
                ps.Stop();
            }
        }

        //Increase FoV based on speed
        camInterface.SetFov(60 + (rb.velocity.magnitude));
    }
}
