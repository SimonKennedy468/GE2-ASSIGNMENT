using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killHunters : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] hunters;
    public void OnButtonPress()
    {
        hunters = GameObject.FindGameObjectsWithTag("hunter");
        for(int i = 0; i < hunters.Length; i++)
        {
            Destroy(hunters[i]);
        }
    }
}
