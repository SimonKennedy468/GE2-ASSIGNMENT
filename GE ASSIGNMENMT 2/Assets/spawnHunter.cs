using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class spawnHunter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hunter;
    

    public void OnButtonPress()
    {
        GameObject.Instantiate(hunter);
    }
}
