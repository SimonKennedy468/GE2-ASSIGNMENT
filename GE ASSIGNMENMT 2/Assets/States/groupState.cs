//State to control boids in a flock

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class groupState : boidBaseState
{
    public GameObject centerPoint;

    public GameObject boidManager;

    public override void EnterState(boidStateManager boidState)
    {
        //Create centre of flock
        if (centerPoint == null)
        {
            centerPoint = new GameObject();
        }
        boidManager = GameObject.FindGameObjectWithTag("boidManager");

    }
    public override void UpdateState(boidStateManager boidState)
    {
        Vector3 posSum = new Vector3(0, 0, 0);
        int count = 0;

        //iterate thorough all boids to check distances
        for (int i = 0; i < boidManager.GetComponent<boidList>().allBoidsList.Count; i++)
        {
            float dist = Vector3.Distance(boidState.transform.position, boidManager.GetComponent<boidList>().allBoidsList[i].transform.position);
            if (dist <= 25)
            {
                count++;
                posSum += boidManager.GetComponent<boidList>().allBoidsList[i].transform.position;
            }
        }
        //reset centre point each frame
        Object.Destroy(centerPoint);
        centerPoint = new GameObject();

        //get average vector of nearby boids
        Vector3 avgPos = posSum / count;
        centerPoint.transform.Translate(avgPos);

        //get direction to centre and rotate boid to it
        Vector3 centerDir = centerPoint.transform.position - boidState.transform.position;
        Vector3 newDir = Vector3.RotateTowards(boidState.transform.forward, centerDir, 1f * Time.deltaTime, 0.0f);


        //set bounds
        if (boidState.transform.position.x >= 150 || boidState.transform.position.x <= -150 || boidState.transform.position.z >= 150 || boidState.transform.position.z <= -150 || boidState.transform.position.y <= 5)
        {
            boidState.SwitchState(boidState.returning);
        }

        else
        {
            boidState.transform.rotation = Quaternion.LookRotation(newDir); //+ allignDir
        }

        //check energy levels
        if (boidState.GetComponent<energy>().boidEnergy <= 10)
        {
            boidState.SwitchState(boidState.landing);
        }

    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {



    }
}
