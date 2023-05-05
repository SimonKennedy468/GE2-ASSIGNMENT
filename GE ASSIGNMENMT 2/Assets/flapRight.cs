using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flapRight : MonoBehaviour
{
    public bool up = false;
    public bool down = true;

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
        else if (down == true)
        {
            transform.Rotate(0, -2.5f, 0);
            StartCoroutine(flapdown());
        }

    }


    IEnumerator flapUp()
    {

        yield return new WaitForSecondsRealtime(0.33f);
        up = false;
        down = true;
    }


    IEnumerator flapdown()
    {

        yield return new WaitForSecondsRealtime (0.33f);
        up = true;
        down = false;
    }
}
