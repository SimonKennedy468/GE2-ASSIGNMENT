using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy : MonoBehaviour
{
    

    public bool currEnergyHigh = false;
    public bool currEnergymedium = false;
    public bool currEnergylow = false;

    public bool resting = false;
    public float boidEnergy = 20;


    public Material highE;
    public Material mediumE;
    public Material lowE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(resting == false)
        {
            boidEnergy = boidEnergy - 1f * Time.deltaTime;
        }
        else if (resting == true)
        {
            boidEnergy = boidEnergy + 1f * Time.deltaTime;
        }

        if (boidEnergy > 10 && currEnergyHigh == false)
        {
            this.GetComponent<Renderer>().material = highE;
            currEnergylow = false;
            currEnergymedium = false;
            currEnergyHigh = true;

        }

        else if (boidEnergy < 10 && boidEnergy > 0 && currEnergymedium == false)
        {
            this.GetComponent<Renderer>().material = mediumE;
            currEnergyHigh = false;
            currEnergymedium = true;

        }

        else if (boidEnergy <= 0)
        {
            if (currEnergylow == false)
            {
                this.GetComponent<Renderer>().material = lowE;
                currEnergymedium = false;
                currEnergylow = true;

            }


        }
    }
}
