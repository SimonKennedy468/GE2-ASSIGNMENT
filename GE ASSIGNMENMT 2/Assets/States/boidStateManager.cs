//State manager for boids
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidStateManager : MonoBehaviour
{
    boidBaseState currentState;

    public restingState resting = new restingState();
    public landingState landing = new landingState();
    public groupState group = new groupState();
    public deadState dead = new deadState();
    public aloneState alone = new aloneState();
    public returningState returning = new returningState();




    //bools to check current energy
    

    // Start is called before the first frame update
    void Start()
    {
        currentState = alone;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        
    }

    public void SwitchState(boidBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
    }
}
