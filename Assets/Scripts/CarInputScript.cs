using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputScript : MonoBehaviour
{
    private CarMovement cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            cm.Turn(Input.GetAxis("Horizontal"));
        }

        if (Input.GetKey(KeyCode.W))
        {
            cm.Accelerate(15);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cm.Accelerate(-10);
        }
    }
}
