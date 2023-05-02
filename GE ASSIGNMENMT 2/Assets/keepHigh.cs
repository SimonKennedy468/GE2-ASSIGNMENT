using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepHigh : MonoBehaviour
{

    public GameObject target;
    public GameObject landing;

    public GameObject[] obstacle;

    public float energy = 20f;

    public Vector3 vel;
    public float treeStr = 1;
    public float detectTree = 10;

    public Material highE;
    public Material mediumE;
    public Material lowE;

    public bool currEnergyHigh = false;
    public bool currEnergymedium = false;
    public bool currEnergylow = false;

    public bool rested = true;


    public bool highFlying = false;


    // Start is called before the first frame update
    void Start()
    {
        obstacle = GameObject.FindGameObjectsWithTag("Tree");
    }

    // Update is called once per frame
    void Update()
    {
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
        if(highFlying == true && rested == true)
        {
            energy = energy - 1f * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        highFlying = false;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (rested == true)
        {
            Destroy(GetComponent<boid>());
            /*
            Vector3 turnAway = Vector3.zero;

            
            turnAway = turnAway.normalized;
            

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

            transform.rotation = Quaternion.FromToRotation(Vector3.up, vel);
            */
            Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);

            energy = energy - 1f * Time.deltaTime;
        }

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

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    IEnumerator wait()
    {
        Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
        yield return new WaitForSeconds(0.5f);
        gameObject.AddComponent<boid>();
    }

    
}
