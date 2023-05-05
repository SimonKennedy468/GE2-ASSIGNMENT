using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class boidBaseState
{
    public abstract void EnterState(boidStateManager boidState);
    public abstract void UpdateState(boidStateManager boidState);

    public abstract void OnColissionEnter(boidStateManager boidState, Collision collision);

}
