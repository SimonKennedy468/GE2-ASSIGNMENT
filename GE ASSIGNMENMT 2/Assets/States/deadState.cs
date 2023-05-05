using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadState : boidBaseState
{

    public float timePassed = 0f;
    public override void EnterState(boidStateManager boidState)
    {
        Debug.Log("bird has died :(");
        Object.Destroy(boidState.GetComponent<moveBoid>());
        boidState.GetComponent<Rigidbody>().isKinematic = false;
        boidState.GetComponent<Rigidbody>().useGravity = true;
    }
    public override void UpdateState(boidStateManager boidState)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 3f)
        {
            Object.Destroy(boidState.gameObject);
        }
    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }
}
