using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribs : MonoBehaviour
{

    //TO DO
    // 1. Move movespeed/rotationspeed etc to playerattribs so they can be varied
    // 2. Add awake/asleep functions
    // 2. Add experience levels (overall performance boost)
    // 3. Add skills (fitness, climbing)

    [HideInInspector]
    public bool isMoving;

    public float Energy;


    void Update()
    {
        IsMoving();
        Energy = GetComponent<EnergyManager>().Energy;

    }

    public void IsMoving()
    {
        if (GetComponent<CharacterController>().velocity.magnitude > 0.2)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

}
