using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text TapToPlay;

    [SerializeField] private GameObject Hand_Icon;

    void Start()
    {
        TapToPlay.transform.DOScale(1.1f,.5f).SetLoops(10000,LoopType.Yoyo).SetEase(Ease.InOutFlash);
        Hand_Icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-240f,-320f),1f).SetLoops(10000,LoopType.Yoyo).SetEase(Ease.InOutFlash);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


   public  void MakeItInvisible() {
        GameObject.FindGameObjectWithTag("Start").SetActive(false);   
   }

   public void Restart() {
        SceneManager.LoadScene("Scene1");

    }
        
    
}
