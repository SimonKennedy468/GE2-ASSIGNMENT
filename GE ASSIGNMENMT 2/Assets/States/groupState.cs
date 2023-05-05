using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groupState : boidBaseState
{

    public GameObject[] allBoids;
    public GameObject centerPoint;

    bool returning;



    public override void EnterState(boidStateManager boidState)
    {
        allBoids = GameObject.FindGameObjectsWithTag("Bird");

        if(centerPoint == null)
        {
            centerPoint = new GameObject();
        }
    }
    public override void UpdateState(boidStateManager boidState)
    {

        /*
        Vector3 posSum = new Vector3(0, 0, 0);

        //Vector3 dirSum = new Vector3(0, 0, 0);
        int count = 0;

        for (int i = 0; i < allBoids.Length; i++)
        {
            if (boidState.transform.position != allBoids[i].transform.position)
            {
                float dist = Vector3.Distance(boidState.transform.position, allBoids[i].transform.position);
                if (dist <= 25)
                {
                    count++;
                    posSum += allBoids[i].transform.position;
                    //dirSum += allBoids[i].transform.position;
                }
            }
            
        }

        if (count <= 1)
        {
            boidState.SwitchState(boidState.alone);
        }

        Vector3 avgPos = posSum / count;
        //Vector3 avgDir = dirSum / count;

        centerPoint.transform.Translate(avgPos);

        Vector3 centerDir = centerPoint.transform.position - boidState.transform.position;

        Vector3 newDir = Vector3.RotateTowards(boidState.transform.forward, centerDir, 1f * Time.deltaTime, 0.0f);
        // allignDir = Vector3.RotateTowards(boidState.transform.forward, avgDir, 3f * Time.deltaTime, 0.0f);

        */


        Vector3 posSum = new Vector3(0, 0, 0);
        int count = 0;

        for (int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(boidState.transform.position, allBoids[i].transform.position);
            if (dist <= 25)
            {
                count++;
                posSum += allBoids[i].transform.position;
            }
        }

        Vector3 avgPos = posSum / count;



        Object.Destroy(centerPoint);
        centerPoint = new GameObject();

        centerPoint.transform.Translate(avgPos);
        Vector3 centerDir = centerPoint.transform.position - boidState.transform.position;
        Vector3 newDir = Vector3.RotateTowards(boidState.transform.forward, centerDir, 1f * Time.deltaTime, 0.0f);



        if (boidState.transform.position.x >= 150 || boidState.transform.position.x <= -150 || boidState.transform.position.z >= 150 || boidState.transform.position.z <= -150 || boidState.transform.position.y <= 5)
        {
            boidState.SwitchState(boidState.returning);
        }

        else
        {
            boidState.transform.rotation = Quaternion.LookRotation(newDir); //+ allignDir
        }


        if (boidState.GetComponent<energy>().boidEnergy <= 10)
        {
            boidState.SwitchState(boidState.landing);
        }

    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {



    }
}
