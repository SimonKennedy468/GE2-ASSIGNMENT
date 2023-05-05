using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returningState : boidBaseState
{
    public override void EnterState(boidStateManager boidState)
    {

    }
    public override void UpdateState(boidStateManager boidState)
    {
        if (boidState.transform.position.x <= 25 && boidState.transform.position.x >= -25 && boidState.transform.position.z <= 25 && boidState.transform.position.z >= -25)
        {
            boidState.SwitchState(boidState.alone);
        }
        boidState.transform.LookAt(new Vector3(0, 25, 0));

        if (boidState.GetComponent<energy>().boidEnergy <= 10)
        {
            boidState.SwitchState(boidState.landing);
        }

        if(boidState.GetComponent<energy>().boidEnergy <= 10)
        {
            boidState.SwitchState(boidState.landing);
        }
    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }
}
