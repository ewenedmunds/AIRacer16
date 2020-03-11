using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInterface : MonoBehaviour
{
    private CarMovement cm;

    //Limits on acceleration
    public float maxSpeedAcceleration;
    public float minSpeedDeceleration;

    //The layer wall objects on
    public LayerMask wallLayer;

    //Track whether a user tries to turn/accelerate twice in one update
    public bool isAbleToAccelerate;
    public bool isAbleToTurn;

    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //When a player is using a script, they should only be able to accelerate/turn once per frame
        isAbleToAccelerate = true;
        isAbleToTurn = true;
    }

    //Returns distance in in-game units to the nearest wall, at a given angle from the car (in degrees)
        //If no wall is detected in this direction, we return 999 (an arbitrary large number)
    public float GetDistanceToWall(float angle)
    {
        Vector3 dir = (Quaternion.AngleAxis(angle, transform.up) * transform.forward)*15;

        //Create a raycast out from the car in the correct direction
        RaycastHit hit;
        Physics.Raycast(transform.position, dir, out hit, 999, wallLayer);

        //Debug information
        Debug.DrawRay(transform.position, dir, new Color(0, 0, 0));
        Debug.Log("Direction to wall at "+angle.ToString()+" degrees is : "+hit.distance.ToString());

        if (hit.collider == null) { return 999; }

        return (hit.distance);
    }

    public float GetSpeed()
    {
        return cm.GetComponent<Rigidbody>().velocity.magnitude;
    }
    
    //todo: make these callthroughs better/neater
    //todo: methods to allow the user to debug: e.g. print a message to screen, maybe show a physical flag on car?
    public void Turn(float amount)
    {
        if (Mathf.Abs(amount) <= 1 && isAbleToTurn)
        {
            cm.Turn(amount);
            isAbleToTurn = false;
        }
    }

    public void Accelerate(float amount)
    {
        if (amount >= minSpeedDeceleration && amount <= maxSpeedAcceleration && isAbleToAccelerate)
        {
            cm.Accelerate(amount);
            isAbleToAccelerate = false;
        }
    }
}
