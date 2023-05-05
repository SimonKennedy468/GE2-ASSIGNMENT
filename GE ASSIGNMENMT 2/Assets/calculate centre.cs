using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculatecentre : MonoBehaviour
{
    public GameObject[] allBoids;
    public GameObject centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        allBoids = GameObject.FindGameObjectsWithTag("Bird");

        centerPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(centerPoint);

        Vector3 posSum = new Vector3(0, 0, 0);
        int count = 0;

        for (int i = 0; i < allBoids.Length; i++)
        { 
            float dist = Vector3.Distance(transform.position, allBoids[i].transform.position);
            if (dist <= 25)
            {
                count++;
                posSum += allBoids[i].transform.position;
            }
        }

        Vector3 avgPos = posSum / count;

        centerPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        centerPoint.transform.Translate(avgPos);
    }

}

