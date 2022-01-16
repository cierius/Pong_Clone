using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Blur_Camera : MonoBehaviour
{
    //[SerializeField]
    private RenderTexture blur;
    private Material blurMat;
    private GameObject plane;
    private Camera cam;

    void Start()
    {
        plane = GameObject.Find("Square");
        cam = GetComponent<Camera>();
    }
    
    public void PreRenderCall()
    {
        blur = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        blur.Create();
        
        
        //blur = cam.targetTexture;
        //RenderTexture.active = cam.targetTexture;
    }
    
    
    public void RenderCall()
    {
        Texture2D tex = new Texture2D(blur.width, blur.height);
        //print(tex.width + " " + tex.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();
        
        Sprite blurSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100f);

        plane.GetComponent<SpriteRenderer>().sprite = blurSprite;
        
        blur.Release();
    }
}
