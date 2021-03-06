﻿using UnityEngine;
using System.Collections;

public class alienScript : MonoBehaviour
{
    //declare the transform of our goal (where the navmesh agent will move towards) and our navmesh agent (in this case our alien)
    private Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //set the navmesh agent's desination equal to the main camera's position (our first person character)
        agent.destination = goal.position;
        //start the running animation
        GetComponent<Animation>().Play("Run");
    }


    //for this to work both need colliders, one must have rigid body, and the alien must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {
        //first disable the alien's collider so multiple collisions cannot occur
        GetComponent<CapsuleCollider>().enabled = false;
        //destroy the bullet
        Destroy(col.gameObject);
        //stop the alien from moving forward by setting its destination to it's current position
        agent.destination = gameObject.transform.position;
        //stop the walking animation and play the falling back animation
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play("Idle");
        //destroy this alien in six seconds.
        Destroy(gameObject, 3);
        //instantiate a new alien
        GameObject alien = Instantiate(Resources.Load("Alien", typeof(GameObject))) as GameObject;

        //set the coordinates for a new vector 3
        float randomX = randomPos();
        float constantY = .01f;
        float randomZ = randomPos();
        //set the aliens position equal to these new coordinates
        alien.transform.position = new Vector3(randomX, constantY, randomZ);

        //if the alien gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
        //so keep repositioning the alien until it is greater than 3 scene units away. 
        while (Vector3.Distance(alien.transform.position, Camera.main.transform.position) <= 5)
        {
            randomX = randomPos();
            randomZ = randomPos();

            alien.transform.position = new Vector3(randomX, constantY, randomZ);
        }
    }

    float randomPos()
    {
        return (UnityEngine.Random.Range(0, 2) == 0 ? -1f : 1f) * UnityEngine.Random.Range(10f, 15f);
    }
}
