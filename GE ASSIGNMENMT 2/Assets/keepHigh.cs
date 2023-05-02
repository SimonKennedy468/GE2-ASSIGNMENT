using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepHigh : MonoBehaviour
{
    //empty target gameobjects
    public GameObject target;
    public GameObject landing;

    //array to store obstacles
    public GameObject[] obstacle;

    //store variable to represent boid energy levels 
    public float energy = 20f;

    //values needed for force application
    public Vector3 vel;
    public float treeStr = 1;
    public float detectTree = 10;

    //materials to show boid energy level
    public Material highE;
    public Material mediumE;
    public Material lowE;

    //bools to check current energy
    public bool currEnergyHigh = false;
    public bool currEnergymedium = false;
    public bool currEnergylow = false;

    public bool rested = true;


    public bool highFlying = false;


    // Start is called before the first frame update
    void Start()
    {
        //get list of all obstacels
        obstacle = GameObject.FindGameObjectsWithTag("Tree");
    }

    // Update is called once per frame
    void Update()
    {
        /*Constantly check the current status of the boids energy levels. 
         * As they fall, the material is changed to reflect how tired the boid is
         */
        if (energy > 10 && currEnergyHigh == false)
        {
            this.GetComponent<Renderer>().material = highE;
            currEnergylow = false;
            currEnergymedium = false;
            currEnergyHigh = true;
            
        }

        else if (energy < 10 && energy > 0 && currEnergymedium == false)
        {
            this.GetComponent<Renderer>().material = mediumE;
            currEnergyHigh = false;
            currEnergymedium = true;
        }

        else if (energy <= 0)
        {

            transform.LookAt(landing.transform, Vector3.forward);

            rested = false;
            if(currEnergylow == false)
            {
                this.GetComponent<Renderer>().material = lowE;
                currEnergymedium = false;
                currEnergylow = true;
                Destroy(GetComponent<boid>());
            }

            
        }
        //if its flyign high and has already rested
        if(highFlying == true && rested == true)
        {
            energy = energy - 1f * Time.deltaTime;
        }
        
    }

    //flying low
    private void OnTriggerEnter(Collider other)
    {
        highFlying = false;
        
    }
    //boid curls back up and away from the ground, back into the flock
    private void OnTriggerStay(Collider other)
    {
        //just curling back up, looks towards a target in the sky and flys upwards towards it in a slerp
        if (rested == true)
        {
            //stop the boid behaviours so the rotation isnt "corrected" back into the average flock point
            Destroy(GetComponent<boid>());
            
            Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
            
            
        }

        //When the bird is coming in for a landing to rest
        else if(other.gameObject.CompareTag("Ground") && rested == false)
        {
            Destroy(GetComponent<moveBoid>());
            energy = energy + 5f * Time.deltaTime;
            transform.LookAt(target.transform.position);
            if (energy >= 20)
            {
                rested = true;
                
                gameObject.AddComponent<moveBoid>();
            }
        }

        //collided with tree, needs to move out of the way
        else
        {
            Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);

            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 0.1f, ForceMode.Impulse);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (rested == true)
        {
            StartCoroutine(wait());
        }
        highFlying = true;

        //reset the velocity to keep boid moving in correct direction
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    //add the boid script back after half a second to avoid jitter
    IEnumerator wait()
    {
        Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
        yield return new WaitForSeconds(0.5f);
        gameObject.AddComponent<boid>();
    }

    
}
