using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidList : MonoBehaviour
{
    public GameObject plane;
    public GameObject[] allBoids;
    public List<GameObject> allBoidsList = new List<GameObject>();

    public GameObject[] allTrees;
    public List<GameObject> allTreesList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.FindGameObjectWithTag("Ground");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(plane.GetComponent<spawnBoids>().boidSpawned == true)
        {
            allBoids = GameObject.FindGameObjectsWithTag("Bird");
            allBoidsList = new List<GameObject>(allBoids);
            plane.GetComponent<spawnBoids>().boidSpawned = false;
        }

        if(plane.GetComponent<treeSpawn>().treesSpawned == true)
        {
            allTrees = GameObject.FindGameObjectsWithTag("Tree");
            allTreesList = new List<GameObject>(allTrees);
        }
    }
}
