using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepHigh : MonoBehaviour
{

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(GetComponent<boid>());
    }

    private void OnTriggerStay(Collider other)
    {
        Destroy(GetComponent<boid>());
        Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(wait());
        
    }


    IEnumerator wait()
    {
        Quaternion look = Quaternion.LookRotation(target.transform.position - this.transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 2);
        yield return new WaitForSeconds(0.5f);
        gameObject.AddComponent<boid>();
    }
}
