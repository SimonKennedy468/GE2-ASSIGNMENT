using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landingState : boidBaseState
{

    public List<Vector3> potentialPoints = new List<Vector3>();
    public GameObject landingPoint;
    public override void EnterState(boidStateManager boidState)
    {
        for (int i = 0; i < 5; i++)
        {
            int radius = 25;
            float angle = i * Mathf.PI * 2 / 5;
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            potentialPoints.Add(new Vector3(x, 0, z));

        }

        landingPoint = new GameObject();

        landingPoint.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)] + boidState.transform.position - new Vector3(0, boidState.transform.position.y, 0));

        potentialPoints.Clear();
    }
    public override void UpdateState(boidStateManager boidState)
    {
        Vector3 targetDir = landingPoint.transform.position - boidState.transform.position;

        Vector3 landDir = Vector3.RotateTowards(boidState.transform.forward, targetDir, 3f * Time.deltaTime, 0.0f);
        boidState.transform.rotation = Quaternion.LookRotation(landDir);

        if(Vector3.Distance(boidState.transform.position, landingPoint.transform.position) <= 2 )
        {
            Object.Destroy(landingPoint);
            boidState.SwitchState(boidState.resting);
        }

        Debug.Log("Energy is + " + boidState.GetComponent<energy>().boidEnergy);
        if(boidState.GetComponent<energy>().boidEnergy <= 0)
        {
            boidState.SwitchState(boidState.dead);
        }
    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }
}
