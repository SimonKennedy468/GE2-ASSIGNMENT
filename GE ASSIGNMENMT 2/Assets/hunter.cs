using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hunter : MonoBehaviour
{

    public float timePassed = 5f;

    public List<Vector3> potentialPoints = new List<Vector3>();
    public GameObject travelPoint;

    // Start is called before the first frame update
    void Start()
    {
        travelPoint = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 3f)
        {
            for (int i = 0; i < 5; i++)
            {
                int radius = 15;
                float angle = i * Mathf.PI * 2 / 5;
                float x = Mathf.Sin(angle) * radius;
                float z = Mathf.Cos(angle) * radius;

                potentialPoints.Add(new Vector3(x, -this.transform.position.y, z));

            }


            travelPoint.transform.Translate(potentialPoints[UnityEngine.Random.Range(0, 4)]);

 

            potentialPoints.Clear();
            timePassed = 0;

            
        }
        Vector3 targetDir = travelPoint.transform.position - this.transform.position;

        Vector3 newDir = Vector3.RotateTowards(this.transform.forward, targetDir, 3 * Time.deltaTime, 0.0f);
        this.transform.rotation = Quaternion.LookRotation(newDir);
        this.transform.Translate(Vector3.forward * 20 * Time.deltaTime);
    }
}
