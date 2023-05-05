using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoid : MonoBehaviour
{

    public GameObject bird;
    public GameObject boidManager;
    // Start is called before the first frame update


    // Update is called once per frame
    public void OnButtonPress()
    {
        boidManager = GameObject.FindGameObjectWithTag("boidManager");
        GameObject go = GameObject.Instantiate(bird, new Vector3(Random.Range(-50, 5), 5 , Random.Range(-50, 5)), Quaternion.identity);
        boidManager.GetComponent<boidList>().allBoidsList.Add(go);
    }
}
