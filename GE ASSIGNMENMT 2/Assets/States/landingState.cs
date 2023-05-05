//State for landing the boid
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landingState : boidBaseState
{

    public List<Vector3> potentialPoints = new List<Vector3>();
    public GameObject landingPointScan;
    public GameObject landingPoint;
    public override void EnterState(boidStateManager boidState)
    {
        //generate 5 points, shoot a ray down. The hit point of the ray is the landing point
        landingPoint = new GameObject();
        for (int i = 0; i < 5; i++)
        {
            int radius = 25;
            float angle = i * Mathf.PI * 2 / 5;
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            potentialPoints.Add(new Vector3(x, boidState.transform.position.y, z));

        }

        landingPointScan = new GameObject();

        landingPointScan.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)] + boidState.transform.position);
        Ray landingRay = new Ray(landingPointScan.transform.position, -landingPointScan.transform.up);
        RaycastHit hit;
        if(Physics.Raycast(landingRay, out hit))
        {
            landingPoint.transform.position = hit.point;
        }

        potentialPoints.Clear();
    }
    public override void UpdateState(boidStateManager boidState)
    {
        //travel to landing point
        Vector3 targetDir = landingPoint.transform.position - boidState.transform.position;

        Vector3 landDir = Vector3.RotateTowards(boidState.transform.forward, targetDir, 3f * Time.deltaTime, 0.0f);
        boidState.transform.rotation = Quaternion.LookRotation(landDir);

        //check if landed
        if(Vector3.Distance(boidState.transform.position, landingPoint.transform.position) <= 2 )
        {
            Object.Destroy(landingPoint);
            boidState.SwitchState(boidState.resting);
        }
        //check if there was no tree
        if(landingPoint.transform.position.y <=1)
        {
            boidState.SwitchState(boidState.dead);
        }
    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }
}
