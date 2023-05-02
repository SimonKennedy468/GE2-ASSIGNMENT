using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBoid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move boid forward along its y axis
        transform.Translate(Vector3.up * 33 * Time.deltaTime);
    }

}
