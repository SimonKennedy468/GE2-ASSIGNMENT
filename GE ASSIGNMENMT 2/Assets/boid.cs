using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour
{
    //lists to store boids and obstacles
    public GameObject[] allBoids;
    public GameObject[] obstacle;

    //needs to be public so boids know wether to go as a flock or regroup
    public int boidCount;
    
    //turn strenghts
    public float centreStr = 0.5f;
    public float avoidStr = 0.025f;
    public float alignStr = 0.3f;

    public float treeStr = 3;

    //distance checks
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

        /*the boids are not moved by this script, there is already a moveBoid.cs script that 
         * pushes it forward along its y axis. This simply rotates it in the relevant direction 
         * based on context. The direction to move, vel, is calulated but adding the values 
         * calculated in the cohesion, avoid and align methods
         */

        //method gets average centre of masses 
        Cohesion();

        //move as flock
        if(boidCount > 1)
        {
            Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, vel);
            transform.rotation = toRotate;
        }
        //move individually 
        else
        {
            Quaternion look = Quaternion.LookRotation(new Vector3(0,20,0) - this.transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
        }

    }

    //This method calculates the agverage position of 
    //all the nearby boids. 
    void Cohesion()
    {
        
        boidCount = 0;
        Vector3 boidPosSum = this.transform.position;

        //check distance to all current boids, inefficient but works
        for (int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, this.transform.position);

            if (dist <= detectDistCheck)
            {
                //add up all positions and increase count of nearby boids
                boidPosSum += allBoids[i].transform.position;
                boidCount++;
            }
        }

        //nearby boid was found
        if (boidCount > 1)
        {
            /*This gets the average distance, then normalizes it.
             * based on the average, the direction can be calulated, normalized, 
             * calculated with the turn strength variables and added to 
             * the velocity var total. The avoid and allign methods are then run
             */
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

    //method to avoid other boids and obstacles 
    void Avoid()
    {
        //default value of zero, and does not need to turn
        Vector3 turnAway = Vector3.zero;

        //check distance of all boids realtive to self
        for(int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, this.transform.position);

            //calculate direction to turn in i.e sharper turn up if 2 are below
            if(dist <= avoidDistCheck)
            {
                turnAway += (this.transform.position - allBoids[i].transform.position);
            }
        }
        //normalize values 
        turnAway = turnAway.normalized;
        vel = vel + avoidStr * turnAway / (avoidStr + 1);
        vel = vel.normalized;

        //same, but for static obstacles like trees
        for (int i = 0; i < obstacle.Length; i++)
        {
            float dist = Vector3.Distance(obstacle[i].transform.position, this.transform.position);

            if (dist <= detectTree)
            {
                turnAway = turnAway + (this.transform.position - obstacle[i].transform.position);
            }
        }
        //normalize values
        turnAway = turnAway.normalized;
        vel = vel + treeStr * turnAway / (treeStr + 1);
        vel = vel.normalized;
    }


    //final boid rule, allign self with other nearby boids 
    void Allign()
    {
        //no nearby boids, dont need to rotate further, default at zero
        Vector3 dirSum = Vector3.zero;
        int count = 0;

        //check distance of each boid relative to self, and keep count
        for(int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, this.transform.position);
            if(dist <= alignmentDistCheck)
            {
                dirSum += allBoids[i].transform.up;
                count++;
            }
        }

        //get average direction, normalize and add to vel value
        Vector3 avg = dirSum / count;
        avg = avg.normalized;

        float deltaTimeStr = alignStr * Time.deltaTime;

        vel = vel + deltaTimeStr * avg / (deltaTimeStr + 1);
        vel = vel.normalized;        
    }
}