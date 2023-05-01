using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour
{

    public GameObject[] allBoids;

    public GameObject currCentre;

    public float centreStr = 3;
    public float avoidStr = 0.025f;
    public float alignStr = 0.3f;

    public float detectDistCheck = 75;
    public float avoidDistCheck = 3;
    public float alignmentDistCheck = 15;

    public Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        allBoids = GameObject.FindGameObjectsWithTag("Bird");
    }

    // Update is called once per frame
    void Update()
    {
      

        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, vel);
        transform.rotation = toRotate;

        Cohesion();
        //Debug.Log("velocity is " + vel);
        transform.position += vel * Time.deltaTime;



    }

    void Cohesion()
    {
        int boidCount = 0;
        Vector3 boidPosSum = transform.position;

        for (int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, transform.position);

            if (dist <= detectDistCheck)
            {
                boidPosSum += allBoids[i].transform.position;
                boidCount++;
            }
        }

        
        
        if (boidCount > 1)
        {


            Vector3 avg = boidPosSum / boidCount;
            avg = avg.normalized;

            Vector3 turn = (avg - transform.position).normalized;

            float deltaTimeStr = centreStr * Time.deltaTime;
            vel = vel + deltaTimeStr * turn / (deltaTimeStr + 1);
            vel = vel.normalized;

            Avoid();
            Allign();

        }
    }

    


    void Avoid()
    {
        Vector3 turnAway = Vector3.zero;

        for(int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, transform.position);

            if(dist <= avoidDistCheck)
            {
                turnAway = turnAway + (transform.position - allBoids[i].transform.position);
            }
        }

        turnAway = turnAway.normalized;
        vel = vel + avoidStr * turnAway / (avoidStr + 1);
        vel = vel.normalized;
    }

    void Allign()
    {
        Vector3 dirSum = Vector3.zero;
        int count = 0;

        for(int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, transform.position);
            if(dist <= alignmentDistCheck)
            {
                dirSum = dirSum + allBoids[i].transform.up;
                count++;
            }
        }

        Vector3 avg = dirSum / count;
        avg = avg.normalized;

        float deltaTimeStr = alignStr * Time.deltaTime;

        vel = vel + deltaTimeStr * avg / (deltaTimeStr + 1);
        vel = vel.normalized;        
    }
}