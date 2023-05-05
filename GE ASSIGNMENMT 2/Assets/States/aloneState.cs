//state for when boid is on its own
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class aloneState : boidBaseState
{

    public List<Vector3> potentialPoints = new List<Vector3>();
    public GameObject travelPoint;
    public bool needPoint = true;
    public float timePassed = 3f;

    public GameObject boidManager;

    float seconds = 3f;

    public Transform self;

    public override void EnterState(boidStateManager boidState)
    {
        //get transform of self
        self = boidState.transform;

        if (travelPoint == null)
        {
            travelPoint = new GameObject();
        }
        boidManager = GameObject.FindGameObjectWithTag("boidManager");

    }

    public override void UpdateState(boidStateManager boidState)
    {
        //check time passed, done here as co-routine would run correctly waiting for 3 seconds, then run every frame
        timePassed += Time.deltaTime;
        if (timePassed >= 3f)
        {
            //generate 5 points in a circle, then pick one at random
            for (int i = 0; i < 5; i++)
            {
                int radius = 25;
                float angle = i * Mathf.PI * 2 / 5;
                float x = Mathf.Sin(angle) * radius;
                float z = Mathf.Cos(angle) * radius;

                potentialPoints.Add(new Vector3(x, UnityEngine.Random.Range(5, 15), z));

            }

            if (self.position.y >= 25)
            {
                travelPoint.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)] + self.position + new Vector3(0, -10, 0));

            }

            else
            {
                travelPoint.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)] + self.position);

            }
            potentialPoints.Clear();
            timePassed = 0;
        }

        else
        {
            //check for nearby boids and change state if necessary
            for (int i = 0; i < boidManager.GetComponent<boidList>().allBoidsList.Count; i++)
            {
                if (boidState.transform.position != boidManager.GetComponent<boidList>().allBoidsList[i].transform.position)
                {
                    float dist = Vector3.Distance(boidState.transform.position, boidManager.GetComponent<boidList>().allBoidsList[i].transform.position);
                    if (dist <= 25)
                    {
                        boidState.SwitchState(boidState.group);
                    }
                }

            }
            //travel to randomly set point
            if (travelPoint != null)
            {
                Vector3 targetDir = travelPoint.transform.position - boidState.transform.position;

                Vector3 newDir = Vector3.RotateTowards(boidState.transform.forward, targetDir, 3 * Time.deltaTime, 0.0f);


                if (boidState.transform.position.x >= 150 || boidState.transform.position.x <= -150 || boidState.transform.position.z >= 150 || boidState.transform.position.z <= -150 || boidState.transform.position.y >= 100 || boidState.transform.position.y <= 5)
                {
                    boidState.SwitchState(boidState.returning);
                }

                else
                {
                    boidState.transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
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

