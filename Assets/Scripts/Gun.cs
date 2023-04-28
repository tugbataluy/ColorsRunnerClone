using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem muzzleFlash;
    public bool isLeft;
    LineManager lm;
    void Start()
    {
        lm=GameObject.Find("MainPlayer").GetComponent<LineManager>();
    }

    // Update is called once per frame
    void Update()
    {
            if (lm.isCollided){
               //ShowInfo();
               if(isLeft)
               {
                this.transform.GetChild(0).DOLocalRotate(new Vector3(0,-35,0),.5f);
                }
               else
               {
                this.transform.GetChild(0).DOLocalRotate(new Vector3(0,35,0),.5f);
               }
               muzzleFlash.Play();
            }
    }

  
    
  
    

}
