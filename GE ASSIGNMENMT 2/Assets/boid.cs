using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour
{

    public GameObject[] allBoids;

    public GameObject[] obstacle;

    public int boidCount;
    

    public float centreStr = 0.5f;
    public float avoidStr = 0.025f;
    public float alignStr = 0.3f;

    public float treeStr = 3;

    public float detectDistCheck = 20;
    public float avoidDistCheck = 3;
    public float alignmentDistCheck = 15;

    public float detectTree = 33;

    public Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        allBoids = GameObject.FindGameObjectsWithTag("Bird");
        obstacle = GameObject.FindGameObjectsWithTag("Tree");
    }

    // Update is called once per frame
    void Update()
    {
        Cohesion();

        if(boidCount > 1)
        {
            Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, vel);
            transform.rotation = toRotate;
        }

        else
        {
            Quaternion look = Quaternion.LookRotation(new Vector3(0,33,0) - this.transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
        }

    }

    void Cohesion()
    {
        boidCount = 0;
        Vector3 boidPosSum = this.transform.position;

        for (int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, this.transform.position);

            if (dist <= detectDistCheck)
            {
                boidPosSum += allBoids[i].transform.position;
                boidCount++;
            }
        }

        if (boidCount > 1)
        {
            Vector3 avg = (boidPosSum / boidCount);
            
            avg = avg.normalized;

            Debug.Log("avg = " + avg);

            Vector3 turn = (avg - this.transform.position).normalized;

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
            float dist = Vector3.Distance(allBoids[i].transform.position, this.transform.position);

            if(dist <= avoidDistCheck)
            {
                turnAway = turnAway + (this.transform.position - allBoids[i].transform.position);
            }
        }

        turnAway = turnAway.normalized;
        vel = vel + avoidStr * turnAway / (avoidStr + 1);
        vel = vel.normalized;

        for (int i = 0; i < obstacle.Length; i++)
        {
            float dist = Vector3.Distance(obstacle[i].transform.position, this.transform.position);

            if (dist <= detectTree)
            {
                turnAway = turnAway + (this.transform.position - obstacle[i].transform.position);
            }
        }

        turnAway = turnAway.normalized;
        vel = vel + treeStr * turnAway / (treeStr + 1);
        vel = vel.normalized;
    }

    void Allign()
    {
        Vector3 dirSum = Vector3.zero;
        int count = 0;

        for(int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, this.transform.position);
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