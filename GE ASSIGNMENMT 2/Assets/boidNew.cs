using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidNew : MonoBehaviour
{
    float speed = 30;

    public Vector3 dir;
    public GameObject[] allBoids;



    public float centreStr = 0.5f;
    public float avoidStr = 0.025f;
    public float alignStr = 0.3f;

    public float detectDistCheck = 20;
    public float avoidDistCheck = 3;
    public float alignmentDistCheck = 15;

    // Start is called before the first frame update
    void Start()
    {
        allBoids = GameObject.FindGameObjectsWithTag("Bird");
    }

    // Update is called once per frame
    void Update()
    {
        //Allign();
        MoveToCentre();
        Avoid();

        Quaternion toRotate = Quaternion.FromToRotation(Vector3.up, dir);
        transform.rotation = toRotate;

        

        //transform.Translate(dir * (speed * Time.deltaTime));
    }


    void MoveToCentre()
    {
        Vector3 posSum = transform.position;
        int count = 0;

        for(int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, transform.position);
            if(dist <= detectDistCheck)
            {
                posSum += allBoids[i].transform.position;
                count++;
            }
        }

        if(count == 0)
        {
            return;
        }

        Vector3 posAvg = posSum / count;
        
        posAvg = posAvg.normalized;
        Debug.DrawRay(this.transform.position, posAvg, Color.red);
        Vector3 faceDir = (posAvg - transform.position).normalized;

        float deltaTimeStr = centreStr * Time.deltaTime;
        dir = dir + deltaTimeStr * faceDir / (deltaTimeStr + 1);
        dir = dir.normalized;
    }

    void Avoid()
    {
        Vector3 faceAwayDir = Vector3.zero;

        for (int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, transform.position);

            if(dist <= avoidDistCheck)
            {
                faceAwayDir = faceAwayDir + (transform.position = allBoids[i].transform.position);
            }
        }

        faceAwayDir = faceAwayDir.normalized;

        dir = dir + avoidStr * faceAwayDir / (avoidStr + 1);
        dir = dir.normalized;
    }

    void Allign()
    {
        Vector3 dirSum = Vector3.zero;
        int count = 0;

        for (int i = 0; i < allBoids.Length; i++)
        {
            float dist = Vector3.Distance(allBoids[i].transform.position, transform.position);
            if(dist <= avoidDistCheck)
            {
                dirSum += allBoids[i].transform.up;
                count++;
            }
        }

        Vector3 dirAvg = dirSum / count;
        dirAvg = dirAvg.normalized;

        float deltaTimeStr = alignStr * Time.deltaTime;
        dir = dir + deltaTimeStr * dirAvg / (deltaTimeStr + 1);
        dir = dir.normalized;
    }
}
