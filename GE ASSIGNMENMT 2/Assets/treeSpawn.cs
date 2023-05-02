using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int boidNum = 25;
    public GameObject tree;
    // Start is called before the first frame update
    void Awake()
    {
        /*Spawn multiple trees at random points, using random values for x and z. 
         */
        for (int i = 0; i < boidNum; i++)
        {
            Vector3 randPos = new Vector3(UnityEngine.Random.Range(-350, 350), UnityEngine.Random.Range(1, 1), UnityEngine.Random.Range(-350, 350));
            Instantiate(tree, randPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
