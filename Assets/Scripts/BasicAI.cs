using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is an example AI for the car, using the interface available.
    //It works surprisingly well
public class BasicAI : MonoBehaviour
{
    private CarInterface ci;

    // Start is called before the first frame update
    void Start()
    {
        ci = GetComponent<CarInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we're far from a wall directly in front of us, or travelling really slowly, accelerate
        if (ci.GetDistanceToWall(0) > 5 || ci.GetSpeed() <= 1)
        {
            ci.Accelerate(15);
        }

        //Roughly: then turn away from the nearest wall
            //We get the distance to the wall 30 degrees to our right, and subtract the distance from the wall 30 degrees to the left
            //We then know which wall is closer based on whether or not the remaining no is positive
        float predictedTurn = ci.GetDistanceToWall(30) - ci.GetDistanceToWall(-30);

        if (predictedTurn < 0)
        {
            ci.Turn(-1);
        }
        else
        {
            ci.Turn(1);
        }
    }
}
