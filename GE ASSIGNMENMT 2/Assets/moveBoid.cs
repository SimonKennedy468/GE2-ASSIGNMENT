using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBoid : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directon = target.transform.position - transform.position;
        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, directon);
        transform.rotation = toRotate;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 3 * Time.deltaTime);
    }
}
