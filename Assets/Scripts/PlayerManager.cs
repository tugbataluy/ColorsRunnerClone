using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    LineManager lineManager;
    Material mainPlayer;

      private float maxBound=10f;
    private float minBound=-9f;
    bool isAI;
       void Start()
    {
        
        lineManager=this.transform.parent.GetComponent<LineManager>();
        mainPlayer=this.transform.GetComponentInChildren<SkinnedMeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject==this.transform.parent.GetChild(3).gameObject&& !isAI){
            this.gameObject.AddComponent<NavMeshAgent>();
            isAI=true;
        }
    }

    void OnTriggerEnter(Collider other){
        //renk degistirme
        if(other.CompareTag("ColorChanger")){
            ChangeColors(other.gameObject.transform.GetComponentInChildren<MeshRenderer>().material.color);
            lineManager.score.color=other.gameObject.transform.GetComponentInChildren<MeshRenderer>().material.color;
            this.transform.DOScale(this.transform.localScale*1.2f,.5f).OnComplete(()=>this.transform.DOScale(this.transform.localScale/1.2f,.5f));
        }

        // Aynı renkli olursa sıraya ekle
        if(other.CompareTag("Collectable")&& other.GetComponentInChildren<SkinnedMeshRenderer>().material.color==mainPlayer.color){
            Destroy(other.gameObject);
            lineManager.Creator(mainPlayer);

        }
        //Farklı renkliyse çarpışınca yok et
         if(this.gameObject==this.transform.parent.GetChild(3).gameObject&&other.CompareTag("Collectable")&& other.GetComponentInChildren<SkinnedMeshRenderer>().material.color!=mainPlayer.color){
            Destroy(other.gameObject);
            lineManager.Destroyer(this.gameObject);

        }

        //engele çarpınca yok ol, şart sürünün başındaki eleman olman
         if(other.gameObject.CompareTag("Obstacle") && this.gameObject==this.transform.parent.GetChild(3).gameObject){
            lineManager.Destroyer(this.gameObject);
            
        }

        //
         if(other.CompareTag("StickyPlatform") && other.GetComponent<MeshRenderer>().material.color!=mainPlayer.color){
                lineManager.isCollided=true;
                StartCoroutine(lineManager.RemoveThem(this.transform.parent.GetChild(3).gameObject));
           

        }
       
    
     if(other.CompareTag("Exit")&&this.gameObject==this.transform.parent.GetChild(this.transform.parent.childCount-1).gameObject){
                lineManager.isCollided=false;
                other.transform.parent.GetComponent<BoxCollider>().enabled=false;
           }
       
       if(other.CompareTag("Finish") && !lineManager.isFinished){
        lineManager.Finish();

       }

       
       


    }

    
                 
        
       
             
             
            
     void ChangeColors( Color c){
        mainPlayer.color=c;

    }
    IEnumerator Wait(){
        yield return new WaitForSeconds(2f);
        lineManager.Destroyer(this.gameObject);
    }

    IEnumerator EnableDisable(Collider co){
        co.isTrigger=false;
        yield return new WaitForSeconds(2f*lineManager.players.IndexOf(this.gameObject));
        co.isTrigger=true;
        lineManager.Destroyer(co.gameObject);
    }
}
