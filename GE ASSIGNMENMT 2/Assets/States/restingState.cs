//State for when boid is resting
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restingState : boidBaseState
{
    public float hitInspect;
    public override void EnterState(boidStateManager boidState)
    {
        Object.Destroy(boidState.GetComponent<moveBoid>());
        boidState.GetComponent<energy>().resting = true;
        boidState.transform.LookAt(new Vector3(0, 30, 0));
    }
    public override void UpdateState(boidStateManager boidState)
    {
        //check if rest is over
        if(boidState.GetComponent<energy>().boidEnergy >=20)
        {
            boidState.gameObject.AddComponent<moveBoid>();
            boidState.SwitchState(boidState.alone);
        }
        //check if tree is destroyed after landing, then switch states if necessary. shoot raycast below and see if it is less than 2, meaning the floor
        Ray landingRay = new Ray(boidState.transform.position, -Vector3.up);
        Debug.DrawRay(boidState.transform.position, -Vector3.up, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(landingRay, out hit))
        {
            hitInspect = hit.point.y;
            if (hit.point.y <= 2f)
            {
                boidState.gameObject.AddComponent<moveBoid>();

                boidState.SwitchState(boidState.dead);
            }
        }
    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }

}
