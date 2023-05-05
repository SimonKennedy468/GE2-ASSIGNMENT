using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restingState : boidBaseState
{
    public override void EnterState(boidStateManager boidState)
    {
        Object.Destroy(boidState.GetComponent<moveBoid>());
        boidState.GetComponent<energy>().resting = true;
        boidState.transform.LookAt(new Vector3(0, 30, 0));
    }
    public override void UpdateState(boidStateManager boidState)
    {
        if(boidState.GetComponent<energy>().boidEnergy >=20)
        {
            boidState.gameObject.AddComponent<moveBoid>();
            boidState.SwitchState(boidState.alone);
        }
    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }

}
