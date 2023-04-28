using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Rainbow : MonoBehaviour
{
    // Start is called before the first frame update
    SkinnedMeshRenderer skin;
   
    [SerializeField] [Range(0f,1f)] float lerpTime;
    [SerializeField] Color[] myColors;
    string[] Color_hex={
        "#9400D3","#4B0082","#0000FF","#00FF00","#FFFF00","	#FF7F00","	#FF0000"
    };
    int colorindex=0;
    float t=0f;
    void Start()
    {
        HexToColor();
        skin=this.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<myColors.Length;i++)
        {skin.material.DOColor(myColors[0],.1f);}
    }

    void HexToColor(){
       
        for(int i=0;i<Color_hex.Length;i++)
        {ColorUtility.TryParseHtmlString(Color_hex[i], out myColors[i] );
           
        }
    }
}
