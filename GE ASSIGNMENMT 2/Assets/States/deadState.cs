using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadState : boidBaseState
{
    public GameObject boidManager;
    public float timePassed = 0f;
    public override void EnterState(boidStateManager boidState)
    {
        boidManager = GameObject.FindGameObjectWithTag("boidManager");
        Debug.Log("coudlnt find a tree, goodbye :(");
    }
    public override void UpdateState(boidStateManager boidState)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 10f)
        {
            boidManager.GetComponent<boidList>().allBoidsList.Remove(boidState.gameObject);
            boidManager.GetComponent<boidList>().allBoidsList.Remove(boidState.gameObject);
            Object.Destroy(boidState.gameObject);
        }

        Vector3 targetDir = new Vector3(500, 100, 500) - boidState.transform.position;

        Vector3 newDir = Vector3.RotateTowards(boidState.transform.forward, targetDir, 3 * Time.deltaTime, 0.0f);
        boidState.transform.rotation = Quaternion.LookRotation(newDir);

    }

    public override void OnColissionEnter(boidStateManager boidState, Collision collision)
    {

    }
}
