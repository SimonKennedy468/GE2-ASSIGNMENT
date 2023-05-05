using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSingletree : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject tree;
    public GameObject boidManager;
    public void OnButtonPress()
    {
        boidManager = GameObject.FindGameObjectWithTag("boidManager");
        GameObject go = GameObject.Instantiate(tree, new Vector3(Random.Range(-150, 150), 1, Random.Range(-150, 150)), Quaternion.identity);
        boidManager.GetComponent<boidList>().allTreesList.Add(go);
    }
}
