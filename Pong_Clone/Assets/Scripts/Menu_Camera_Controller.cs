using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Camera_Controller : MonoBehaviour
{
    
    private Camera mainCam;
    private Camera blurCam;
    
    
    void Awake()
    {
        mainCam = GetComponent<Camera>();
        blurCam = GameObject.Find("Blur_Camera").GetComponent<Camera>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //sblurCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //blurCam.targetTexture = mainCam.activeTexture;
        /*blurCam.GetComponent<Blur_Camera>().PreRenderCall();
        blurCam.Render();
        blurCam.GetComponent<Blur_Camera>().RenderCall();*/
    }

    void OnPreRender()
    {
        blurCam.GetComponent<Blur_Camera>().PreRenderCall();
        blurCam.Render();
        blurCam.GetComponent<Blur_Camera>().RenderCall();
    }
}
