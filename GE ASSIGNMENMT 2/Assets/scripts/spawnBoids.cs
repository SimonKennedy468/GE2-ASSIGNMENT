using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoids : MonoBehaviour
{

    public int boidNum = 10;
    public GameObject birdBoid;
    public bool boidSpawned = false;

    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < boidNum; i++)
        {
            Vector3 randPos = new Vector3(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(5, 5), UnityEngine.Random.Range(-50, 50));
            Instantiate(birdBoid, randPos, Quaternion.identity);
        }
        boidSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
