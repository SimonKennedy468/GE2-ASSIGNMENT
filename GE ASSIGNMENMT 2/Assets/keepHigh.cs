using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepHigh : MonoBehaviour
{

    public GameObject target;
    public GameObject landing;
    public float energy = 20f;


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
    }

    private void OnTriggerExit(Collider other)
    {
        if (rested == true)
        {
            StartCoroutine(wait());
        }
        highFlying = true;
    }


    IEnumerator wait()
    {
        Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
        yield return new WaitForSeconds(0.5f);
        gameObject.AddComponent<boid>();
    }

    
}
