using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flapLeft : MonoBehaviour
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
            transform.Rotate(0, 2.5f, 0);
            StartCoroutine(flapUp());
        }
        else if(down == true)
        {
            transform.Rotate(0, -2.5f, 0);
            StartCoroutine(flapdown());
        }
        
    }


    IEnumerator flapUp()
    {
        
        yield return new WaitForSecondsRealtime(0.25f);
        up = false;
        down = true;
    }


    IEnumerator flapdown()
    {

        yield return new WaitForSecondsRealtime(0.25f);
        up = true;
        down = false;
    }
}
