using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject smt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(smt.GetComponent<PlaneManager>().isHere){
            
        }
    }
}
