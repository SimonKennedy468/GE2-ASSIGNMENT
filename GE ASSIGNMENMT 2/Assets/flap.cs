using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flap : MonoBehaviour
{
    public bool up = true;
    public bool down = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (up == true)
        {
            StartCoroutine(flapUp(1f));
        }
        else if(down == true)
        {
            StartCoroutine(flapdown(1f));
        }
        
    }


    IEnumerator flapUp(float timetoflap)
    {
        float currTime = 0f;

        while(currTime < timetoflap)
        {
            transform.Rotate(0, 2.5f, 0);
        }
        currTime += Time.deltaTime;

        if(currTime >= timetoflap)
        {
            up = false;
            down = true;
        }
        yield return null;
    }


    IEnumerator flapdown(float timetoflap)
    {

        float currTime = 0f;

        while (currTime < timetoflap)
        {
            transform.Rotate(0, -2.5f, 0);
        }
        currTime += Time.deltaTime;

        if (currTime >= timetoflap)
        {
            up = false;
            down = true;
        }
        
        yield return null;
    }
}
