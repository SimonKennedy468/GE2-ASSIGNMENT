using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int boidNum = 25;
    public GameObject tree;
    public bool treesSpawned = false;
    // Start is called before the first frame update
    void Awake()
    {
        /*Spawn multiple trees at random points, using random values for x and z. 
         */
        for (int i = 0; i < boidNum; i++)
        {
            Vector3 randPos = new Vector3(UnityEngine.Random.Range(-150, 150), UnityEngine.Random.Range(1, 1), UnityEngine.Random.Range(-150, 150));
            Instantiate(tree, randPos, Quaternion.identity);
        }
        treesSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
