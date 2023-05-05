//hunter controolers
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hunter : MonoBehaviour
{


    public List<Vector3> potentialPoints = new List<Vector3>();
    public GameObject travelPoint;
    public GameObject boidManager;

    // Start is called before the first frame update
    void Start()
    {
        travelPoint = new GameObject();

        boidManager = GameObject.FindGameObjectWithTag("boidManager");
        //set random position
        transform.position = new Vector3(Random.Range(-150, 150), 0, Random.Range(-150, 150));
}

    // Update is called once per frame
    void Update()
    {
        //loop through all trees to find the closest, then move towards it 
        Vector3 closest = new Vector3(500, 500, 500);
        for(int i = 0; i < boidManager.GetComponent<boidList>().allTreesList.Count; i++)
        {
            if(Vector3.Distance(boidManager.GetComponent<boidList>().allTreesList[i].transform.position, this.transform.position) < Vector3.Distance(closest, this.transform.position))
            {
                closest = boidManager.GetComponent<boidList>().allTreesList[i].transform.position;
            }
        }

        Vector3 treeDir = closest - this.transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, treeDir, 3 * Time.deltaTime, 0.0f);
        this.transform.rotation = Quaternion.LookRotation(newDir);
        transform.Translate(Vector3.forward * 2.5f * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider collision)
    {
        //destroy a tree on colision
        Debug.Log("Collided, chopping tree");
        if (collision.gameObject.tag == "Tree")
        {
            boidManager.GetComponent<boidList>().allTreesList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
}
}
