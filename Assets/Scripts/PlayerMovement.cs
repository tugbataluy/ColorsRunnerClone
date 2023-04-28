using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    LineManager lm;
    private float maxBound=9f;
    private float minBound=-9f;
    // Start is called before the first frame update
    Touch t;
    void Start()
    {
      lm=GameObject.Find("MainPlayer").GetComponent<LineManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement=Input.GetAxis("Horizontal")*horizontalSpeed;
        this.transform.Translate(-verticalSpeed*Time.deltaTime,0,horizontalMovement*Time.deltaTime);
        //,horizontalMovement*Time.deltaTime

        if (Input.touchCount > 0)
            {
                t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Moved)
                {
                   
                    transform.position = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z + t.deltaPosition.x*0.025f);
                   
                    lm.MakeSmooth();
                    
                }
              
               
                   
              
               

               
                
                
            }
    
    
    else{
        lm.OriginLine();
        
        
       
    }
    
    
    }

    
    
}
