using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
    // Start is called before the first frame update
    LineManager lineManager;
    public Animator anim;
    public bool isHere;
    public GameObject nextTo;
    public GameObject drone;

    
    void Start()
    {
        lineManager=GameObject.Find("MainPlayer").GetComponent<LineManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lineManager.players.Count==0){
            lineManager.ShowFail();
        }
    }
    
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")&&!isHere){
            drone.SetActive(true);
            drone.transform.DOMoveZ(this.transform.position.z,3f).OnComplete(()=>Destroy(drone,.25f));
           
            if(this.GetComponent<MeshRenderer>().material.color!=other.GetComponentInChildren<SkinnedMeshRenderer>().material.color)
            {
             
                other.transform.parent.GetComponent<PlayerMovement>().enabled=false;
                StartCoroutine(lineManager.Align(this.gameObject));
                isHere=true;
                StartCoroutine(Wait());
                other.transform.parent.GetChild(0).gameObject.SetActive(false);
                    this.transform.DOScaleX(0,3f).OnComplete(MakeIt);
                    
            }
            else{
                
                nextTo.GetComponent<BoxCollider>().enabled=false;
                 other.transform.parent.GetComponent<PlayerMovement>().enabled=false;
                StartCoroutine(lineManager.Align(this.gameObject));
                isHere=true;
                
                 nextTo.transform.DOScaleX(0,3f).OnComplete(lineManager.Realign);
                 
                  
            }

   


}
}
IEnumerator Wait(){
    
    yield return new WaitForSeconds(lineManager.players.Count*.5f);
    
}
IEnumerator Wait2(GameObject go){

    yield return new WaitForSeconds(1);
    lineManager.players.Remove(go);
}
    void MakeIt(){
     for(int i=lineManager.players.Count-1;i>=0;i--){
                anim=lineManager.players[i].GetComponent<Animator>();
                anim.SetBool("IsDead_bool",true);
                GameObject togo=lineManager.players[i];
                Destroy(togo,1f);
                StartCoroutine(Wait2(togo));
                
            }
        
      
        
    }
}


