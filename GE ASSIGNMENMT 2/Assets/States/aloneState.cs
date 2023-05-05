using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aloneState : boidBaseState
{

    public List<Vector3> potentialPoints = new List<Vector3>();
    public GameObject travelPoint;
    public bool needPoint = true;
    public float timePassed = 3f;

    public GameObject[] allBoids;


    float seconds = 3f;

    public Transform self;

    public override void EnterState(boidStateManager boidState)
    {
        self = boidState.transform;
        allBoids = GameObject.FindGameObjectsWithTag("Bird");
        if(travelPoint == null)
        {
            travelPoint = new GameObject();
        }
        

    }

    public override void UpdateState(boidStateManager boidState)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 3f)
        {
            for (int i = 0; i < 5; i++)
            {
                int radius = 25;
                float angle = i * Mathf.PI * 2 / 5;
                float x = Mathf.Sin(angle) * radius;
                float z = Mathf.Cos(angle) * radius;

                potentialPoints.Add(new Vector3(x, UnityEngine.Random.Range(5, 15), z));

            }

            
            


            if (self.position.y >= 25)
            {
                travelPoint.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)] + self.position + new Vector3(0, -10, 0));

            }

            else
            {
                travelPoint.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)] + self.position);

            }
            potentialPoints.Clear();
            timePassed = 0;
        }
        else
        {
            for (int i = 0; i < allBoids.Length; i++)
            {
                if(boidState.transform.position != allBoids[i].transform.position)
                {
                    float dist = Vector3.Distance(boidState.transform.position, allBoids[i].transform.position);
                    if (dist <= 25)
                    {
                        boidState.SwitchState(boidState.group);
                    }
                }
                
            }
            if (travelPoint != null)
            {
                Vector3 targetDir = travelPoint.transform.position - boidState.transform.position;

                Vector3 newDir = Vector3.RotateTowards(boidState.transform.forward, targetDir, 3 * Time.deltaTime, 0.0f);
                Debug.DrawRay(boidState.transform.position, newDir, Color.red);


                if (boidState.transform.position.x >= 150 || boidState.transform.position.x <= -150 || boidState.transform.position.z >= 150 || boidState.transform.position.z <= -150 || boidState.transform.position.y >= 100)
                {
                    boidState.SwitchState(boidState.returning);

                }

                else
                {
                    boidState.transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
        }

        if(boidState.GetComponent<energy>().boidEnergy <= 10)
        {
            boidState.SwitchState(boidState.landing);
        }

    }
    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {
        Vector3 collisionDir = collision.transform.position - boidState.transform.position;
        Vector3 avoidDir = Vector3.RotateTowards(boidState.transform.forward, collisionDir, 1f * Time.deltaTime, -1.0f);
        boidState.transform.rotation = Quaternion.LookRotation(avoidDir);
        boidState.gameObject.GetComponent<Rigidbody>().AddForce(boidState.transform.up * -1f, ForceMode.Impulse);

    }

}