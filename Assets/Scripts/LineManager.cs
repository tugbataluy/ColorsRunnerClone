using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UI;
public class LineManager : MonoBehaviour
{
    
    // Start is called before the first frame update
   public TMP_Text score;
    public List<GameObject>players;
    public GameObject playerClone;
    public bool isCollided;
   public bool isFinished;
   GameObject follow;
    public GameObject spawnPoint;
    public Vector3 spawnPosition;
    public Animator anim;
    public GameObject Menu;
    void Start()
    {
        GameIsNotActive();
        players.Add(this.transform.GetChild(3).gameObject);
        //print(this.transform.GetChild(2).gameObject.name);
        spawnPosition=new Vector3(spawnPoint.transform.localPosition.x+1,spawnPoint.transform.localPosition.y,spawnPoint.transform.localPosition.z);
        //print(mainPlayer.color);
        follow=this.transform.GetChild(1).gameObject;
        score=this.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

    
    }

    // Update is called once per frame
    void Update()
    {
       
        follow.transform.localPosition=this.transform.GetChild(3).localPosition;
        
        score.text=players.Count.ToString();

        Menu.transform.GetChild(3).GetComponentInChildren<Text>().text=players.Count.ToString();
        this.transform.GetChild(0).localPosition=new Vector3 (this.transform.GetChild(3).localPosition.x,this.transform.GetChild(3).localPosition.y+3.5f,this.transform.GetChild(3).localPosition.z);
        if(players.Count==0){
            ShowFail();
        }
    }
    
   

    public void Creator(Material coloring){
      
        MakeThemBiggerThenSmaller();
        GameObject newOne= Instantiate(playerClone,Vector3.zero,playerClone.transform.rotation,this.transform);
        newOne.transform.localPosition=spawnPosition;
        newOne.GetComponentInChildren<SkinnedMeshRenderer>().material.color=coloring.color;
        //spawnPoint.transform.localPosition=new Vector3(this.transform.GetChild(this.transform.childCount-1).localPosition.x+1,this.transform.GetChild(this.transform.childCount-1).localPosition.y,this.transform.GetChild(this.transform.childCount-1).localPosition.z);
        spawnPosition.x+=1;
        players.Add(newOne);

    }
    public void Destroyer(GameObject go){
        players.Remove(go);
        anim=go.GetComponent<Animator>();
        anim.SetBool("IsDead_bool",true);
        
        go.transform.parent=null;
        Destroy(go,.5f);
        //spawnPoint.transform.localPosition=new Vector3 (spawnPoint.transform.localPosition.x+1,spawnPoint.transform.localPosition.y,spawnPoint.transform.localPosition.z);
        
    }

   public IEnumerator RemoveThem(GameObject go){
        yield return new WaitForSeconds(.3f);
          
            Destroyer(go);

    }

    public IEnumerator Align(GameObject go){
            Vector3 startPosition=go.transform.position;
            for(int i=0;i<players.Count;i++){
                players[i].transform.position=new Vector3(startPosition.x+(0.3f*i),0.5f,startPosition.z);
                players[i].GetComponentInChildren<Outline>().enabled=false;
                
                yield return new WaitForSeconds(.1f);
            }

    }
    public void Realign(){
        Vector3 spawner=new Vector3(0,0.5f,0);
       
        
        for(int i=0;i<players.Count;i++){
            players[i].transform.localPosition=spawner;
            players[i].GetComponentInChildren<Outline>().enabled=true;
            spawner.x+=1f;

        }
        GameIsActive();

    }
    public void Finish(){
        float scale= players.Count*0.1f;
        this.transform.GetChild(0).gameObject.SetActive(false);
        GameIsNotActive();
   
        for(int i=1;i<players.Count;i++){
            Destroy(players[i]);
        }
        players[0].transform.DOScale(new Vector3(scale,scale,scale)+players[0].transform.localScale,5f).OnComplete(ShowWin);
        
            
        

    }

    public void MakeSmooth(){
       
        for(int i=1;i<players.Count;i++){
            players[i].transform.DOMoveZ( players[i-1].transform.position.z,.7f);

            }
    }
    
    public void OriginLine(){
        for(int i=1;i<players.Count;i++){
            players[i].transform.DOMoveZ(players[0].transform.position.z,.1f);
        }
    }

    public void MakeThemBiggerThenSmaller(){
        foreach(GameObject go in players){
            float scale=1.5f;
            go.transform.DOScale(new Vector3(scale,scale,scale),.1f);
        }
    }

    public void GameIsActive() {
        this.GetComponent<PlayerMovement>().enabled = true;
    }

    public void GameIsNotActive() {
        this.GetComponent<PlayerMovement>().enabled = false;

    }

    public void ShowFail() {
        GameIsNotActive();
        Menu.transform.GetChild(5).gameObject.SetActive(true);

    }

    public void ShowWin() {
        GameIsNotActive();
        Menu.transform.GetChild(6).gameObject.SetActive(true);
    }
}
